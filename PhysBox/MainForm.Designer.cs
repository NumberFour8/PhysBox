namespace PhysBox
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.ukončitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukončitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.objektyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vytvořitGeometriiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.newobj_Saved = new System.Windows.Forms.ComboBox();
            this.newobj_Geometry = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newobj_Mass = new System.Windows.Forms.MaskedTextBox();
            this.newobj_Material = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ukončitToolStripMenuItem,
            this.objektyToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1019, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip";
            // 
            // ukončitToolStripMenuItem
            // 
            this.ukončitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.ukončitToolStripMenuItem1});
            this.ukončitToolStripMenuItem.Name = "ukončitToolStripMenuItem";
            this.ukončitToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.ukončitToolStripMenuItem.Text = "PhysBox";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // ukončitToolStripMenuItem1
            // 
            this.ukončitToolStripMenuItem1.Name = "ukončitToolStripMenuItem1";
            this.ukončitToolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.ukončitToolStripMenuItem1.Text = "Ukončit";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 441);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1019, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip";
            // 
            // objektyToolStripMenuItem
            // 
            this.objektyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vytvořitGeometriiToolStripMenuItem});
            this.objektyToolStripMenuItem.Name = "objektyToolStripMenuItem";
            this.objektyToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.objektyToolStripMenuItem.Text = "Objekty";
            // 
            // vytvořitGeometriiToolStripMenuItem
            // 
            this.vytvořitGeometriiToolStripMenuItem.Name = "vytvořitGeometriiToolStripMenuItem";
            this.vytvořitGeometriiToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.vytvořitGeometriiToolStripMenuItem.Text = "Vytvořit geometrii...";
            this.vytvořitGeometriiToolStripMenuItem.Click += new System.EventHandler(this.vytvořitGeometriiToolStripMenuItem_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.newobj_Saved);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Location = new System.Drawing.Point(823, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 411);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nástroje";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.newobj_Material);
            this.groupBox2.Controls.Add(this.newobj_Mass);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.newobj_Geometry);
            this.groupBox2.Location = new System.Drawing.Point(6, 19);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(183, 319);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Nový objekt";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(109, 382);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Vložit";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 341);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "nebo předdefinovaný model";
            // 
            // newobj_Saved
            // 
            this.newobj_Saved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newobj_Saved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newobj_Saved.FormattingEnabled = true;
            this.newobj_Saved.Location = new System.Drawing.Point(6, 355);
            this.newobj_Saved.Name = "newobj_Saved";
            this.newobj_Saved.Size = new System.Drawing.Size(178, 21);
            this.newobj_Saved.TabIndex = 3;
            // 
            // newobj_Geometry
            // 
            this.newobj_Geometry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newobj_Geometry.FormattingEnabled = true;
            this.newobj_Geometry.Location = new System.Drawing.Point(11, 19);
            this.newobj_Geometry.Name = "newobj_Geometry";
            this.newobj_Geometry.Size = new System.Drawing.Size(166, 21);
            this.newobj_Geometry.TabIndex = 1;
            this.newobj_Geometry.Text = "<vyber geometrii>";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 78);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Hmotnost (g) :";
            // 
            // newobj_Mass
            // 
            this.newobj_Mass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newobj_Mass.BeepOnError = true;
            this.newobj_Mass.Location = new System.Drawing.Point(90, 75);
            this.newobj_Mass.Mask = "0000000";
            this.newobj_Mass.Name = "newobj_Mass";
            this.newobj_Mass.PromptChar = ' ';
            this.newobj_Mass.Size = new System.Drawing.Size(87, 20);
            this.newobj_Mass.TabIndex = 4;
            // 
            // newobj_Material
            // 
            this.newobj_Material.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newobj_Material.FormattingEnabled = true;
            this.newobj_Material.Location = new System.Drawing.Point(11, 48);
            this.newobj_Material.Name = "newobj_Material";
            this.newobj_Material.Size = new System.Drawing.Size(166, 21);
            this.newobj_Material.TabIndex = 5;
            this.newobj_Material.Text = "<vyber materiál>";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1019, 463);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PhysBox";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem ukončitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ukončitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem objektyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vytvořitGeometriiToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox newobj_Saved;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox newobj_Material;
        private System.Windows.Forms.MaskedTextBox newobj_Mass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox newobj_Geometry;
    }
}

