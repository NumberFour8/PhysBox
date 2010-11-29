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
            Cursor = Cursors.Hand;
            
            newObj.Enabled = button1.Enabled = false;
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
            if (Placing != null && e.Button == System.Windows.Forms.MouseButtons.Left)
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
                Cursor = Cursors.Default;
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                Selected = MyWorld.NearestObject(new Vector(e.X, e.Y, 0));
                manipulateObj_Name.Text = String.Format("Objekt: {0}",(Selected.Model as GraphicObject).Name);
                manipulateObj_Enabled.Checked = Selected.Enabled;
                manipulateObj.Show(new Point(e.X, e.Y));
            }
        }

        private void manipulateObj_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (Selected != null)
                Selected.Enabled = manipulateObj_Enabled.Checked;
        }

    }
}
