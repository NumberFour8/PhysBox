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
        private SimObject Placing = null;
        private bool Moving = false,Rotating = false,AddForce = false;

        private Point? afOrigin;
        
        private Toolbox Tools;
        public SimObject Selected = null;
        
        public MainForm()
        {
            InitializeComponent();
            Ctx = new BufferedGraphicsContext();
        }

        private void ShowToolbox()
        {
            if (Tools == null || Tools.IsDisposed) Tools = new Toolbox();
                
            Tools.Location = new Point(Size.Width - Tools.Width - 5, mainMenu.Height + 30);
            Tools.Show(this);
        }

        public void AddObject(SimObject O)
        {
            Placing = O;
            Cursor = Cursors.Cross;
            Moving = Rotating = false;
        }


        private void kreslitVektoryToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            velikostiVektorůToolStripMenuItem.Enabled = kreslitVektoryToolStripMenuItem.Checked;
            velikostiVektorůToolStripMenuItem.Checked = false;
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (MyWorld == null){
                MyWorld = new World(World.DisplayDefault, World.EarthG,1.5*Math.Sqrt(Math.Pow(Size.Height,2)+Math.Pow(Size.Width,2)));
                MyWorld.OnTick += new EventHandler(MyWorld_OnTick);
                simTime.Enabled = true;
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
                    Placing.Model.Position[0] = (double)(e.X);
                    Placing.Model.Position[1] = (double)(e.Y);

                    MyWorld.AddObject(Placing);
                    
                    Placing = null;
                    Moving = Rotating = false;
                    Cursor = Cursors.Default;
                    Tools.ActionDone();
                }

                if (Moving && Selected != null)
                {
                    Selected.Model.Position = new Vector(e.X, e.Y, 0);
                    Cursor = Cursors.Default;
                }

                if (AddForce && Selected != null){
                    if (afOrigin == null)
                        afOrigin = GetPositionOnObject(new Point((int)Selected.Model.Position[0], (int)Selected.Model.Position[1]), Selected.Model.ObjectGeometry, e.Location);
                    else
                    {
                        AddForce = false;
                        Cursor = Cursors.Default;
                        Selected.ApplyForce(new Vector((double)(afOrigin.Value.X - e.X), (double)(afOrigin.Value.Y - e.Y)), new Vector((double)afOrigin.Value.X, (double)afOrigin.Value.Y));

                        afOrigin = null;
                        Tools.ActionDone();
                    }
                }
                    
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (MyWorld.CountObjects == 0) return;
                Selected = MyWorld.NearestObject(new Vector(e.X, e.Y, 0));

                stat_SelObject.Text = manipulateObj_Name.Text = String.Format("Objekt: {0}", (Selected.Model as GraphicObject).Name);
                manipulateObj_Enabled.Checked = Selected.Enabled;
                manipulateObj.Enabled = true;
                manipulateObj.Show(new Point(e.X, e.Y));

                Tools.ChangeTool(1);
            }
            Moving = false;
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

        private void nástrojeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowToolbox();
        }

        private void pozastavitSimulaciToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            MyWorld.Paused = pozastavitSimulaciToolStripMenuItem.Checked;
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
                afOrigin = null;
                Tools.Forbid();

                Cursor = Cursors.Cross;
                Cursor.Position = new Point((int)(Selected.Model.Position[0] + Selected.Model.ObjectGeometry[0].X + 1),(int)(Selected.Model.Position[1] + Selected.Model.ObjectGeometry[0].Y + 1));
            }
            
        }

    }
}
