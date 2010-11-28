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

        public MainForm()
        {
            InitializeComponent();
            if (!Directory.Exists("objects")) Directory.CreateDirectory("objects");            

        }

        private void RefreshGeometries()
        {
            foreach (string name in Directory.GetFiles("objects"))
                newobj_Geometry.Items.Add(Path.GetFileNameWithoutExtension(name));
        }

        private void vytvořitGeometriiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateObject obj = new CreateObject();
            obj.ShowDialog();
            
            RefreshGeometries();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            newobj_Mass.Clear();
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
                MyWorld = new World(new Vector(Size.Width, Size.Height), World.B, World.EarthG);
                MyWorld.OnTick += new EventHandler(MyWorld_OnTick);
            }
        }

    }
}
