using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using PhysLib;

namespace PhysBox
{
    public partial class MainForm : Form
    {
        public Graphics Scene;

        public MainForm()
        {
            InitializeComponent();
            
            Scene = mainScene.CreateGraphics();
            InitScene();
        }

        public void InitScene()
        {
            Scene.FillRegion(Brushes.Yellow,new Region(new Rectangle(0,0,mainScene.Width,mainScene.Height)));
            Scene.DrawEllipse(Pens.Beige, 0, 0, 20, 20);
            Scene.Save();
        }

    }
}
