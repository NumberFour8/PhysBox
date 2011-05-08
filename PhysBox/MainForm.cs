using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Linq;

using PhysLib;

namespace PhysBox
{
    public partial class MainForm : Form
    {
        public World MyWorld;
        private SimObject Placing = null;
        private bool Moving = false,Rotating = false,AddForce = false,SetAxis = false,Scaling = false,SetLevel = false;

        private PointF? afOrigin;

        private double multiplier = 100;
        private Toolbox Tools;
        public SimObject Selected = null;
        private BufferedGraphics Buffer;
        private float ScaleRatio = 1;

        public MainForm()
        {
            InitializeComponent();
            Ctx = new BufferedGraphicsContext();
            if (!Directory.Exists("scenes")) Directory.CreateDirectory("scenes");
            RefreshScenes();
        }

        private void RefreshScenes()
        {
            menu_Scenes.DropDownItems.Clear();
            foreach (string name in from string n in Directory.GetFiles("scenes") where n.Contains(".sce") || n.Contains(".sc") select Path.GetFileName(n))
                menu_Scenes.DropDownItems.Add(new ToolStripMenuItem(name,null,new EventHandler(QuickLoadScene)));
        }

        private void QuickLoadScene(object Sender, EventArgs args)
        {
            LoadScene(@"scenes\" + (Sender as ToolStripMenuItem).Text);
        }

        private void ShowToolbox()
        {
            if (Tools == null || Tools.IsDisposed) Tools = new Toolbox();
                
            Tools.Location = new Point(Size.Width - Tools.Width - 20, mainMenu.Height + 30);
            if (!Tools.Visible) Tools.Show(this);
        }

        public void AddObject(SimObject O)
        {
            Placing = O;
            Cursor = Cursors.Cross;
            Moving = Rotating = AddForce = Scaling = SetAxis = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (MyWorld == null){
                MyWorld = new World(World.DisplayDefault, World.EarthG,1.5*Math.Sqrt(Math.Pow(Size.Height,2)+Math.Pow(Size.Width,2)),100);
                MyWorld.OnTick += new EventHandler(MyWorld_OnTick);

                Buffer = Ctx.Allocate(CreateGraphics(), new Rectangle(0, 0, Width, Height));
                simTime.Enabled = true;
                MinimumSize = MaximumSize = Size;     
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MyWorld.Tick();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            Vector ClickPos = (new Vector(e.X, e.Y, 0))*(double)ScaleRatio;
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {

                if (Placing != null)
                {                    
                    Placing.Model.Position = ClickPos;
                    (Placing.Model as GraphicObject).WorldIndex = MyWorld.AddObject(Placing);
                    
                    Placing = null;
                    Moving = Rotating = false;
                    Cursor = Cursors.Default;
                    menu_SaveScene.Enabled = MyWorld.CountObjects > 0;
                    if (Tools != null && !Tools.IsDisposed) Tools.ActionDone();
                }

                if (SetLevel)
                {
                    SetLevel = false;
                    MyWorld.Level = ClickPos;
                    if (Tools != null && !Tools.IsDisposed) Tools.ActionDone();
                }

                if (Selected != null)
                {
                    if (SetAxis)
                    {
                        Selected.RotationPoint = ClickPos;
                        Cursor = Cursors.Default;
                        SetAxis = false;
                    }

                    if (AddForce)
                    {
                        if (afOrigin == null)
                            afOrigin = (PointF)Selected.Model.ProjectToObject(ClickPos);
                        else
                        {
                            AddForce = false;
                            Cursor = Cursors.Default;
                            Selected.ApplyForce(((Vector)afOrigin.Value - ClickPos) * multiplier * Selected.Mass, (Vector)afOrigin.Value);

                            afOrigin = null;
                            if (Tools != null && !Tools.IsDisposed) Tools.ActionDone();
                        }
                    }
                }
                    
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (MyWorld.CountObjects == 0) return;
                if (SetLevel) return;
                SelectObject(MyWorld.NearestObject(ClickPos,!menu_AllowStatics.Checked));
                if (Selected == null) return;

                manipulateObj_Enabled.Checked = Selected.Enabled;
                manipulateObj_NoTranslations.Checked = Selected.NoTranslations;
                
                manipulateObj_Rotate.Checked = Rotating;
                manipulateObj_Scale.Checked = Scaling;
                manipulateObj_SetAxis.Checked = SetAxis;
                manipulateObj_ApplyForce.Checked = AddForce;
                manipulateObj_Translate.Checked = Moving;
                manipulateObj_IsStatic.Checked = Selected.Static;

                manipulateObj.Enabled = true;

                manipulateObj.Show(new Point(e.X, e.Y));
            }
        }

        public void SelectObject(SimObject Obj)
        {
            if (Obj == null)
            {
                DeselectObject();
                return;
            }

            Selected = Obj;
            stat_SelObject.Text = manipulateObj_Name.Text = String.Format("Objekt: {0}", (Selected.Model as GraphicObject).Name);
            if (Tools != null && !Tools.IsDisposed) Tools.ChangeTool(1);
        }

        public void DeselectObject()
        {
            Selected = null;
            stat_SelObject.Text = "Žádný vybraný objekt";
            if (Tools != null && !Tools.IsDisposed)  Tools.objProps.Enabled = false;
        }

        public void přemístitSemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Moving = true;
            Cursor = Cursors.Cross;
            Scaling = Rotating = SetAxis = AddForce = false;
        }

        private void otáčetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotating = true;
            Cursor = Cursors.SizeNWSE;
            Moving = Scaling = SetAxis = AddForce = false;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (Rotating || Scaling || Moving))
            {
                Scaling = Rotating = Moving = false;
                Cursor = Cursors.Default;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Rotating && Selected != null)
                Selected.Model.Orientation += 3;
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Moving && Selected != null)
              Selected.Model.Position = (new Vector(e.X, e.Y, 0)) * ScaleRatio;
            //if (e.Button == System.Windows.Forms.MouseButtons.Left && Scaling && Selected != null)
            //    Selected.Model.Scale += 2;
        }        

        private void nástrojeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowToolbox();
        }

        private void pozastavitSimulaciToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            MyWorld.Paused = menu_pauseSim.Checked;
            status_SimStat.Text = MyWorld.Paused ? "Simulace pozastavena" : "Simulace běží";
        }

        private void manipulateObj_Enabled_Click(object sender, EventArgs e)
        {
            if (Selected != null)
              Selected.Enabled = manipulateObj_Enabled.Checked;
        }

        private void působitSilouToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Selected != null)
            {
                AddForce = true;
                Moving = Rotating = Scaling = SetAxis = false;
                afOrigin = null;
                if (Tools != null && !Tools.IsDisposed) Tools.Forbid();
                Cursor = Cursors.Cross;
            }
        }

        private void zvolitOsuOtáčeníToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetAxis = true;
            Moving = Rotating = Scaling = AddForce =  false;
            Cursor = Cursors.Cross;
        }

        private void drátovýModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu_showVertices.Enabled = menu_enableWireframe.Checked;
            if (!menu_enableWireframe.Checked)
                menu_showVertices.Checked = false;
        }

        private void manipulateObj_CancelForces_Click(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected.ResetAll();
        }

        private void manipulateObj_Scale_Click(object sender, EventArgs e)
        {
            Scaling = true;
            Moving = Rotating = SetAxis = AddForce = false;
            Cursor = Cursors.SizeNWSE;
        }

        private void manipulateObj_NoTranslations_Click(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected.NoTranslations = manipulateObj_NoTranslations.Checked;
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Scaling = Moving = Rotating = SetAxis = AddForce = SetLevel = false;
                Placing = null;
                Cursor = Cursors.Default;
                if (Tools != null && !Tools.IsDisposed) Tools.ActionDone();
            }

            if (e.Control && e.KeyCode == Keys.D && Selected != null)
                DeselectObject();

            if (Placing != null)
            {
                if (e.KeyCode == Keys.Up)
                    Placing.Model.Orientation += 5;
                else if (e.KeyCode == Keys.Down)
                    Placing.Model.Orientation -= 5;
            }
        }

        private void nastavitNulovouHladinuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLevel = true;
            if (Tools != null && !Tools.IsDisposed) Tools.Forbid();
        }

        private void menu_restartSim_Click(object sender, EventArgs e)
        {
            DeselectObject();
            MyWorld.ClearFields();
            MyWorld.ClearObjects();
        }

        private void statickýToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected.Static = manipulateObj_IsStatic.Checked;
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (MyWorld != null) 
              MyWorld.Paused = WindowState != FormWindowState.Maximized;
        }

        private void menu_ZoomOut_Click(object sender, EventArgs e)
        {
            ScaleRatio *= 2;
            Buffer.Graphics.ScaleTransform(0.5f, 0.5f);
            MyWorld.MaximumRadius *= 2;

            menu_ZoomOut.Enabled = ScaleRatio <= 4;
            menu_ZoomIn.Enabled = ScaleRatio >= 0.25;
        }

        private void menu_ZoomIn_Click(object sender, EventArgs e)
        {
            ScaleRatio *= 0.5f;
            Buffer.Graphics.ScaleTransform(2, 2);
            MyWorld.MaximumRadius *= 0.5;

            menu_ZoomIn.Enabled = ScaleRatio >= 0.25;
            menu_ZoomOut.Enabled = ScaleRatio <= 4;
        }

        private void uložitScénuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            menu_pauseSim.Checked = true;
            if (saveScene.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            FileStream Str = new FileStream(saveScene.FileName, FileMode.Create);
            GZipStream Compress = new GZipStream(Str, CompressionMode.Compress);

            XmlTextWriter Wri = new XmlTextWriter(Compress, System.Text.Encoding.UTF8);
            Wri.Formatting = Formatting.Indented;
            
            Wri.WriteStartDocument();
            Wri.WriteStartElement("Scene");
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            foreach (SimObject obj in MyWorld.Objects)
            {
                Wri.WriteStartElement("Object");
                using (MemoryStream S = new MemoryStream()){
                    fmt.Serialize(S, obj);
                    byte[] bfr = new byte[S.Length];
                    S.Seek(0, SeekOrigin.Begin);
                    S.Read(bfr, 0, (int)S.Length);
                    Wri.WriteAttributeString("Size", S.Length.ToString());
                    Wri.WriteBase64(bfr, 0,(int)S.Length);
                }
                Wri.WriteEndElement();
            }
            Wri.WriteEndElement();
            Wri.WriteEndDocument();
            Wri.Close();
            Str.Close();
        }

        private void LoadScene(string Location)
        {
            DeselectObject();
            MyWorld.ClearFields();
            MyWorld.ClearObjects();
            menu_pauseSim.Checked = true;

            FileStream Str = new FileStream(Location, FileMode.Open);
            Stream Rd = Str;
            if (Path.GetExtension(Location) == ".sce") Rd = new GZipStream(Str, CompressionMode.Decompress,false);

            XmlTextReader Rdr = new XmlTextReader(Rd);
            System.Runtime.Serialization.Formatters.Binary.BinaryFormatter fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            while (Rdr.Read())
            {
                if (Rdr.Name == "Object" && Rdr.NodeType == XmlNodeType.Element)
                {
                    int length = int.Parse(Rdr.GetAttribute("Size"));
                    byte[] data = new byte[length];
                    using (MemoryStream S = new MemoryStream())
                    {
                        Rdr.ReadElementContentAsBase64(data, 0, length);
                        S.Write(data, 0, length);
                        S.Seek(0, SeekOrigin.Begin);
                        MyWorld.AddObject((SimObject)fmt.Deserialize(S));
                    }
                }
                else continue;
            }
            Rdr.Close();
            Rd.Close();
        }

        private void menu_LoadScene_Click(object sender, EventArgs e)
        {
            if (openScene.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            LoadScene(openScene.FileName);
        }

        private void menu_DeleteOutOfBounds_CheckedChanged(object sender, EventArgs e)
        {
            MyWorld.DeleteOutOfBounds = menu_DeleteOutOfBounds.Checked;
        }

        private void menu_AllowStatics_Click(object sender, EventArgs e)
        {
            DeselectObject();
        }

        private void menu_Quit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }       
    }
}
