using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Collections;
using System.Xml;
using PhysLib;
using System.Linq;

namespace PhysBox
{
    public partial class Toolbox : Form
    {
        private MainForm MyOwner { get { return Owner as MainForm; } }

        public Toolbox()
        {
            InitializeComponent();

            if (!Directory.Exists("objects")) Directory.CreateDirectory("objects");
            RefreshGeometries();
        }

        public void RefreshGeometries()
        {
            newobj_Geometry.Items.Clear();
            foreach (string name in Directory.GetFiles("objects"))
                newobj_Geometry.Items.Add(Path.GetFileNameWithoutExtension(name));
        }

        public void RefreshObjects()
        {
            if (MyOwner == null) return;
            list_allObjects.Items.Clear();
            foreach (GraphicObject obj in (from o in MyOwner.MyWorld.Objects select o.Model))
                list_allObjects.Items.Add(obj.Name);
        }

        private void createGeometry_Click(object sender, EventArgs e)
        {
            CreateObject obj = new CreateObject();
            obj.ShowDialog();

            RefreshGeometries();
        }

        private void newObj_AutoName_CheckedChanged(object sender, EventArgs e)
        {
            newObj_Name.Enabled = !newObj_AutoName.Checked;
        }

        public void Forbid()
        {
            tab_Toolbox.Enabled = false;
        }

        public void ActionDone()
        {
            tab_Toolbox.Enabled = true;
        }

        public void ChangeTool(int Index)
        {
            if (IsDisposed) return;

            if (Index == 1)
            {
                if (MyOwner.Selected != null)
                {
                    objProps.Text = String.Format("Vlastnosti objektu: {0}", (MyOwner.Selected.Model as GraphicObject).Name);
                    objProps.Enabled = true;
                    button_Analyze.Enabled = true;
                    button_SetProps.Enabled = true;
                }
            }
            tab_Toolbox.SelectedIndex = Index;
        }

        private void newObj_Insert_Click(object sender, EventArgs e)
        {
            if (newobj_Mass.Text.Length == 0 && !newObj_Static.Checked)
            {
                MessageBox.Show("Zadejte hmotnost tělesa","Chyba",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                return;
            }

            if (newobj_Geometry.SelectedIndex < 0)
            {
                MessageBox.Show("Vyberte geometrii pro těleso", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (newobj_Material.SelectedIndex < 0)
            {
                MessageBox.Show("Vyberte materiál pro těleso", "Chyba", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GraphicObject Placing = GraphicObject.LoadFromFile(@"objects\" + newobj_Geometry.SelectedItem.ToString() + ".xml");

            if (newObj_AutoName.Checked)
                Placing.Name = String.Format("object_#{0}", MyOwner.MyWorld.CountObjects);
            else Placing.Name = newObj_Name.Text;

            Material MyMat = Material.Concrete;
            switch (newobj_Material.SelectedIndex)
            {
                case 1: MyMat = Material.Wood; break;
                case 2: MyMat = Material.Concrete; break;
                case 3: MyMat = Material.Steel; break;
                case 4: MyMat = Material.Rubber; break;
                default: MyMat = Material.Concrete; break;
            }

            double MyMass = 0;
            if (!Double.TryParse(newobj_Mass.Text, out MyMass))
              MyMass = 1000;

            SimObject MyNewObject = new SimObject(Placing, MyMass/1000, MyMat);
            MyNewObject.Enabled = newObj_Enabled.Checked;
            MyNewObject.Static = newObj_Static.Checked;

            MyOwner.Focus();
            MyOwner.AddObject(MyNewObject);

            tab_Toolbox.Enabled = false;
        }

        private void button_applyEnv_Click(object sender, EventArgs e)
        {
            MyOwner.MyWorld.Resolution = Double.Parse(env_Resolution.Text);
            MyOwner.MyWorld.Aether = Double.Parse(env_Aether.Text);
            MyOwner.MyWorld.Gravity = MyOwner.MyWorld.Convert(new Vector(0, -Double.Parse(env_G.Text), 0), ConversionType.MetersToPixels);
            MyOwner.MyWorld.Solver.Enabled = check_Collisions.Checked;
            MyOwner.MyWorld.Delta = Double.Parse(env_StepSize.Text) / 1000;
        }

        private void tab_Toolbox_Selected(object sender, TabControlEventArgs e)
        {
            if (tab_Toolbox.SelectedIndex == 3)
            {
                env_Aether.Text = MyOwner.MyWorld.Aether.ToString();
                env_G.Text = (-MyOwner.MyWorld.Convert(MyOwner.MyWorld.Gravity, ConversionType.PixelsToMeters).Magnitude).ToString();
                env_Resolution.Text = MyOwner.MyWorld.Resolution.ToString();
                check_Collisions.Checked = MyOwner.MyWorld.Solver.Enabled;
                env_StepSize.Text = (1000 * MyOwner.MyWorld.Delta).ToString();
            }
            else if (tab_Toolbox.SelectedIndex == 2)
                RefreshObjects();
        }

        public void DoSelectByName(string ObjName)
        {
            var sq = from obj in MyOwner.MyWorld.Objects where ((GraphicObject)obj.Model).Name == ObjName select ((GraphicObject)obj.Model).WorldIndex;
            if (sq.Count() == 0) return;
            MyOwner.Selected = MyOwner.MyWorld.Objects[sq.First()];
        }

        private void button4_Click(object sender, EventArgs e)
        {
            DoSelectByName(list_allObjects.SelectedItem.ToString());
            MyOwner.přemístitSemToolStripMenuItem_Click(sender, e);
        }

        private void list_allObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            objs_DeleteObject.Enabled = objs_MoveSelected.Enabled = objs_SelectObject.Enabled = list_allObjects.SelectedIndex > -1;
        }

        private void objs_DeleteObject_Click(object sender, EventArgs e)
        {
            string ObjName = list_allObjects.SelectedItem.ToString();
            var sq = from obj in MyOwner.MyWorld.Objects where ((GraphicObject)obj.Model).Name == ObjName select ((GraphicObject)obj.Model).WorldIndex;
            if (sq.Count() == 0) return;
            if (MessageBox.Show(String.Format("Opravdu chcete těleso {0} odstranit?", ObjName), "Odstranit", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == System.Windows.Forms.DialogResult.Yes)
            {
                MyOwner.MyWorld.DeleteObject(sq.First());
                RefreshObjects();
            }
        }

        private void objs_SelectObject_Click(object sender, EventArgs e)
        {
            DoSelectByName(list_allObjects.SelectedItem.ToString());
        }

        private void button_ResetObj_Click(object sender, EventArgs e)
        {
            if (MyOwner.Selected != null)
                MyOwner.Selected.ResetAll();
        }

        private void newObj_Static_CheckedChanged(object sender, EventArgs e)
        {
            newobj_Mass.Enabled = !newObj_Static.Checked;            
        }

     }
}
