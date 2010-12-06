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
        private bool Moving = false,Rotating = false,AddForce = false,SetAxis = false,Scaling = false;

        private PointF? afOrigin;
        
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
                
            Tools.Location = new Point(Size.Width - Tools.Width - 20, mainMenu.Height + 30);
            Tools.Show(this);
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
                    Placing.Model.Position = new Vector(e.X, e.Y + CursorCorrection,0);
                    MyWorld.AddObject(Placing);
                    
                    Placing = null;
                    Moving = Rotating = false;
                    Cursor = Cursors.Default;
                    Tools.ActionDone();
                }
                if (Selected != null)
                {
                    if (Moving)
                    {
                        Selected.Model.Position = new Vector(e.X, e.Y + CursorCorrection, 0);
                        Cursor = Cursors.Default;
                        Moving = false;
                    }

                    if (SetAxis)
                    {
                        Selected.RotationPoint = new Vector(e.X, e.Y, 0);
                        Cursor = Cursors.Default;
                        SetAxis = false;
                    }

                    if (AddForce)
                    {
                        if (afOrigin == null)
                            afOrigin = Selected.Model.ProjectToObject(e.Location);
                        else
                        {
                            AddForce = false;
                            Cursor = Cursors.Default;
                            Selected.ApplyForce(new Vector((double)(afOrigin.Value.X - e.X), (double)(afOrigin.Value.Y - (e.Y + CursorCorrection))), new Vector((double)afOrigin.Value.X, (double)afOrigin.Value.Y));

                            afOrigin = null;
                            Tools.ActionDone();
                        }
                    }
                }
                    
            }
            else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (MyWorld.CountObjects == 0) return;
                Selected = MyWorld.NearestObject(new Vector(e.X, e.Y, 0));

                stat_SelObject.Text = manipulateObj_Name.Text = String.Format("Objekt: {0}", (Selected.Model as GraphicObject).Name);
                manipulateObj_Enabled.Checked = Selected.Enabled;
                manipulateObj_NoTranslations.Checked = Selected.NoTranslations;
                
                manipulateObj_Rotate.Checked = Rotating;
                manipulateObj_Scale.Checked = Scaling;
                manipulateObj_SetAxis.Checked = SetAxis;
                manipulateObj_ApplyForce.Checked = AddForce;
                manipulateObj_Translate.Checked = Moving;

                manipulateObj.Enabled = true;
                manipulateObj.Show(new Point(e.X, e.Y));

                Tools.ChangeTool(1);
            }
        }

        private void přemístitSemToolStripMenuItem_Click(object sender, EventArgs e)
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
            if (e.Button == System.Windows.Forms.MouseButtons.Left && (Rotating || Scaling))
            {
                Scaling = Rotating = false;
                Cursor = Cursors.Default;
            }
        }

        private void MainForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left && Rotating && Selected != null)
                Selected.Model.Orientation += 3;
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
                Tools.Forbid();
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
                Selected.Reset();
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
                Scaling = Moving = Rotating = SetAxis = AddForce = false;
                Placing = null;
                Cursor = Cursors.Default;
                Tools.ActionDone();
            }
        }

    }
}
