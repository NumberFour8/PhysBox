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
        
        public MainForm()
        {
            InitializeComponent();
            Actors = new ArrayList();

            if (!Directory.Exists("objects"))
                Directory.CreateDirectory("objects");
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

    }
}
