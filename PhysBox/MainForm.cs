using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

using PhysLib;

namespace PhysBox
{
    public partial class MainForm : Form
    {
        private World MyWorld;
        private GraphicObject Placing = null;
        private SimObject Selected = null;
        private bool Moving,Rotating = false;

        public MainForm()
        {
            InitializeComponent();
            if (!Directory.Exists("objects")) Directory.CreateDirectory("objects");
            Ctx = new BufferedGraphicsContext();
            
            RefreshGeometries();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Placing = ReadObjectFromFile(newobj_Geometry.SelectedItem.ToString());
            Cursor = Cursors.Cross;
            
            newObj.Enabled = button1.Enabled = false;
            Moving = Rotating = false;
        }

        private void newObj_AutoName_CheckedChanged(object sender, EventArgs e)
        {
            newObj_Name.Enabled = !newObj_AutoName.Checked;
        }

        private void kreslitVektoryToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            velikostiVektorůToolStripMenuItem.Enabled = kreslitVektoryToolStripMenuItem.Checked;
            velikostiVektorůToolStripMenuItem.Checked = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (MyWorld == null)
            {
                MyWorld = new World(World.DisplayDefault, World.EarthG,1.5*Math.Sqrt(Math.Pow(Height,2)+Math.Pow(Width,2)));
                MyWorld.OnTick += new EventHandler(MyWorld_OnTick);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            MyWorld.Tick();
        }

        private void MainForm_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (Placing != null)
                {
                    Placing.Position[0] = (double)(e.X);
                    Placing.Position[1] = (double)(e.Y);

                    if (newObj_AutoName.Checked)
                        Placing.Name = String.Format("object_#{0}", MyWorld.CountObjects + 1);
                    else Placing.Name = newObj_Name.Text;

                    SimObject MyNewObject = new SimObject(Placing, double.Parse(newobj_Mass.Text) / 1000);
                    MyNewObject.Enabled = newObj_Enabled.Checked;

                    MyWorld.AddObject(MyNewObject);

                    newObj.Enabled = button1.Enabled = true;

                    Placing = null;
                    Moving = Rotating = false;
                    Cursor = Cursors.Default;
                }

                if (Moving && Selected != null)
                {
                    Selected.Model.Position = new Vector(e.X, e.Y, 0);
                    Cursor = Cursors.Default;
                }
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Selected = MyWorld.NearestObject(new Vector(e.X, e.Y, 0));
                status_SelObject.Text = manipulateObj_Name.Text = String.Format("Objekt: {0}", (Selected.Model as GraphicObject).Name);
                manipulateObj_Enabled.Checked = Selected.Enabled;
                manipulateObj.Enabled = true;
                manipulateObj.Show(new Point(e.X, e.Y));
            }
            Moving = false;
        }

        private void manipulateObj_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected.Enabled = manipulateObj_Enabled.Checked;
        }

        private void status_SelObject_DropDownOpening(object sender, EventArgs e)
        {
            manipulateObj_Name.Text = "Žádný objekt";
            manipulateObj.Enabled = Selected != null;
            Rotating = Moving = false;
            if (Selected != null)
            {
                manipulateObj_Name.Text = String.Format("Objekt: {0}", (Selected.Model as GraphicObject).Name);
                manipulateObj_Enabled.Checked = Selected.Enabled;
            }
        }

        private void přemístitSemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Moving = true;
            Cursor = Cursors.Cross;
        }

        private void otáčetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Rotating = true;
            Cursor = Cursors.SizeNWSE;
        }

        private void MainForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Rotating)
            {
                Rotating = false;
                Cursor = Cursors.Default;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Rotating && Selected != null)
                Selected.Model.Orientation[0] += 3;
        }

    }
}
