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
            this.components = new System.ComponentModel.Container();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.ukončitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ukončitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.objektyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vytvořitGeometriiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.analýzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kreslitVektoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.velikostiVektorůToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfiguraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.drátovýModelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.verzeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stavovyRadek = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.newobj_Saved = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.newObj = new System.Windows.Forms.GroupBox();
            this.newObj_Enabled = new System.Windows.Forms.CheckBox();
            this.newObj_AutoName = new System.Windows.Forms.CheckBox();
            this.newObj_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newobj_Material = new System.Windows.Forms.ComboBox();
            this.newobj_Mass = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newobj_Geometry = new System.Windows.Forms.ComboBox();
            this.simTime = new System.Windows.Forms.Timer(this.components);
            this.manipulateObj = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manipulateObj_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.manipulateObj_Enabled = new System.Windows.Forms.ToolStripMenuItem();
            this.vlastnostiObjektuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.zrušitSílyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.změnitVlastnostiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.analyzovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.newObj.SuspendLayout();
            this.manipulateObj.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ukončitToolStripMenuItem,
            this.objektyToolStripMenuItem,
            this.analýzaToolStripMenuItem,
            this.konfiguraceToolStripMenuItem});
            this.mainMenu.Location = new System.Drawing.Point(0, 0);
            this.mainMenu.Name = "mainMenu";
            this.mainMenu.Size = new System.Drawing.Size(1019, 24);
            this.mainMenu.TabIndex = 0;
            this.mainMenu.Text = "menuStrip";
            // 
            // ukončitToolStripMenuItem
            // 
            this.ukončitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.restartToolStripMenuItem,
            this.ukončitToolStripMenuItem1});
            this.ukončitToolStripMenuItem.Name = "ukončitToolStripMenuItem";
            this.ukončitToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ukončitToolStripMenuItem.Text = "Program";
            // 
            // restartToolStripMenuItem
            // 
            this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
            this.restartToolStripMenuItem.Size = new System.Drawing.Size(115, 22);
            this.restartToolStripMenuItem.Text = "Restart";
            // 
            // ukončitToolStripMenuItem1
            // 
            this.ukončitToolStripMenuItem1.Name = "ukončitToolStripMenuItem1";
            this.ukončitToolStripMenuItem1.Size = new System.Drawing.Size(115, 22);
            this.ukončitToolStripMenuItem1.Text = "Ukončit";
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
            // analýzaToolStripMenuItem
            // 
            this.analýzaToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kreslitVektoryToolStripMenuItem,
            this.velikostiVektorůToolStripMenuItem});
            this.analýzaToolStripMenuItem.Name = "analýzaToolStripMenuItem";
            this.analýzaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.analýzaToolStripMenuItem.Text = "Analýza";
            // 
            // kreslitVektoryToolStripMenuItem
            // 
            this.kreslitVektoryToolStripMenuItem.Checked = true;
            this.kreslitVektoryToolStripMenuItem.CheckOnClick = true;
            this.kreslitVektoryToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.kreslitVektoryToolStripMenuItem.Name = "kreslitVektoryToolStripMenuItem";
            this.kreslitVektoryToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.kreslitVektoryToolStripMenuItem.Text = "Kreslit vektory";
            this.kreslitVektoryToolStripMenuItem.CheckedChanged += new System.EventHandler(this.kreslitVektoryToolStripMenuItem_CheckedChanged);
            // 
            // velikostiVektorůToolStripMenuItem
            // 
            this.velikostiVektorůToolStripMenuItem.CheckOnClick = true;
            this.velikostiVektorůToolStripMenuItem.Name = "velikostiVektorůToolStripMenuItem";
            this.velikostiVektorůToolStripMenuItem.Size = new System.Drawing.Size(161, 22);
            this.velikostiVektorůToolStripMenuItem.Text = "Velikosti vektorů";
            // 
            // konfiguraceToolStripMenuItem
            // 
            this.konfiguraceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.drátovýModelToolStripMenuItem,
            this.verzeToolStripMenuItem});
            this.konfiguraceToolStripMenuItem.Name = "konfiguraceToolStripMenuItem";
            this.konfiguraceToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.konfiguraceToolStripMenuItem.Text = "Konfigurace";
            // 
            // drátovýModelToolStripMenuItem
            // 
            this.drátovýModelToolStripMenuItem.Checked = true;
            this.drátovýModelToolStripMenuItem.CheckOnClick = true;
            this.drátovýModelToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.drátovýModelToolStripMenuItem.Name = "drátovýModelToolStripMenuItem";
            this.drátovýModelToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.drátovýModelToolStripMenuItem.Text = "Drátový model";
            // 
            // verzeToolStripMenuItem
            // 
            this.verzeToolStripMenuItem.CheckOnClick = true;
            this.verzeToolStripMenuItem.Name = "verzeToolStripMenuItem";
            this.verzeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.verzeToolStripMenuItem.Text = "Verze";
            // 
            // stavovyRadek
            // 
            this.stavovyRadek.Location = new System.Drawing.Point(0, 441);
            this.stavovyRadek.Name = "stavovyRadek";
            this.stavovyRadek.Size = new System.Drawing.Size(1019, 22);
            this.stavovyRadek.TabIndex = 3;
            this.stavovyRadek.Text = "statusStrip";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.newobj_Saved);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.button1);
            this.groupBox1.Controls.Add(this.newObj);
            this.groupBox1.Location = new System.Drawing.Point(823, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(196, 411);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Nástroje";
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
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // newObj
            // 
            this.newObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newObj.Controls.Add(this.newObj_Enabled);
            this.newObj.Controls.Add(this.newObj_AutoName);
            this.newObj.Controls.Add(this.newObj_Name);
            this.newObj.Controls.Add(this.label3);
            this.newObj.Controls.Add(this.newobj_Material);
            this.newObj.Controls.Add(this.newobj_Mass);
            this.newObj.Controls.Add(this.label2);
            this.newObj.Controls.Add(this.newobj_Geometry);
            this.newObj.Location = new System.Drawing.Point(6, 19);
            this.newObj.Name = "newObj";
            this.newObj.Size = new System.Drawing.Size(183, 319);
            this.newObj.TabIndex = 0;
            this.newObj.TabStop = false;
            this.newObj.Text = "Nový objekt";
            // 
            // newObj_Enabled
            // 
            this.newObj_Enabled.AutoSize = true;
            this.newObj_Enabled.Checked = true;
            this.newObj_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.newObj_Enabled.Location = new System.Drawing.Point(14, 155);
            this.newObj_Enabled.Name = "newObj_Enabled";
            this.newObj_Enabled.Size = new System.Drawing.Size(72, 17);
            this.newObj_Enabled.TabIndex = 9;
            this.newObj_Enabled.Text = "Simulovat";
            this.newObj_Enabled.UseVisualStyleBackColor = true;
            // 
            // newObj_AutoName
            // 
            this.newObj_AutoName.AutoSize = true;
            this.newObj_AutoName.Checked = true;
            this.newObj_AutoName.CheckState = System.Windows.Forms.CheckState.Checked;
            this.newObj_AutoName.Location = new System.Drawing.Point(14, 131);
            this.newObj_AutoName.Name = "newObj_AutoName";
            this.newObj_AutoName.Size = new System.Drawing.Size(116, 17);
            this.newObj_AutoName.TabIndex = 8;
            this.newObj_AutoName.Text = "Automatický název";
            this.newObj_AutoName.UseVisualStyleBackColor = true;
            this.newObj_AutoName.CheckedChanged += new System.EventHandler(this.newObj_AutoName_CheckedChanged);
            // 
            // newObj_Name
            // 
            this.newObj_Name.Enabled = false;
            this.newObj_Name.Location = new System.Drawing.Point(90, 105);
            this.newObj_Name.Name = "newObj_Name";
            this.newObj_Name.Size = new System.Drawing.Size(87, 20);
            this.newObj_Name.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Název :";
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
            // simTime
            // 
            this.simTime.Enabled = true;
            this.simTime.Interval = 10;
            this.simTime.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // manipulateObj
            // 
            this.manipulateObj.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manipulateObj_Name,
            this.toolStripMenuItem1,
            this.manipulateObj_Enabled,
            this.vlastnostiObjektuToolStripMenuItem,
            this.zrušitSílyToolStripMenuItem,
            this.změnitVlastnostiToolStripMenuItem,
            this.toolStripMenuItem2,
            this.analyzovatToolStripMenuItem});
            this.manipulateObj.Name = "manipulateObj";
            this.manipulateObj.Size = new System.Drawing.Size(175, 170);
            // 
            // manipulateObj_Name
            // 
            this.manipulateObj_Name.Enabled = false;
            this.manipulateObj_Name.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.manipulateObj_Name.Name = "manipulateObj_Name";
            this.manipulateObj_Name.Size = new System.Drawing.Size(174, 22);
            this.manipulateObj_Name.Text = "name";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(171, 6);
            // 
            // manipulateObj_Enabled
            // 
            this.manipulateObj_Enabled.CheckOnClick = true;
            this.manipulateObj_Enabled.Name = "manipulateObj_Enabled";
            this.manipulateObj_Enabled.Size = new System.Drawing.Size(174, 22);
            this.manipulateObj_Enabled.Text = "Simulace";
            this.manipulateObj_Enabled.CheckedChanged += new System.EventHandler(this.manipulateObj_Enabled_CheckedChanged);
            // 
            // vlastnostiObjektuToolStripMenuItem
            // 
            this.vlastnostiObjektuToolStripMenuItem.Name = "vlastnostiObjektuToolStripMenuItem";
            this.vlastnostiObjektuToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.vlastnostiObjektuToolStripMenuItem.Text = "Aplikovat sílu";
            // 
            // zrušitSílyToolStripMenuItem
            // 
            this.zrušitSílyToolStripMenuItem.Name = "zrušitSílyToolStripMenuItem";
            this.zrušitSílyToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.zrušitSílyToolStripMenuItem.Text = "Zrušit síly";
            // 
            // změnitVlastnostiToolStripMenuItem
            // 
            this.změnitVlastnostiToolStripMenuItem.Name = "změnitVlastnostiToolStripMenuItem";
            this.změnitVlastnostiToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.změnitVlastnostiToolStripMenuItem.Text = "Změnit vlastnosti...";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(171, 6);
            // 
            // analyzovatToolStripMenuItem
            // 
            this.analyzovatToolStripMenuItem.Name = "analyzovatToolStripMenuItem";
            this.analyzovatToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.analyzovatToolStripMenuItem.Text = "Analyzovat...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1019, 463);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.stavovyRadek);
            this.Controls.Add(this.mainMenu);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MainMenuStrip = this.mainMenu;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PhysBox";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.newObj.ResumeLayout(false);
            this.newObj.PerformLayout();
            this.manipulateObj.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem ukončitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip stavovyRadek;
        private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ukončitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem objektyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vytvořitGeometriiToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox newobj_Saved;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox newObj;
        private System.Windows.Forms.ComboBox newobj_Material;
        private System.Windows.Forms.MaskedTextBox newobj_Mass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox newobj_Geometry;
        private System.Windows.Forms.ToolStripMenuItem analýzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfiguraceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem drátovýModelToolStripMenuItem;
        private System.Windows.Forms.CheckBox newObj_AutoName;
        private System.Windows.Forms.TextBox newObj_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ToolStripMenuItem kreslitVektoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem velikostiVektorůToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verzeToolStripMenuItem;
        private System.Windows.Forms.Timer simTime;
        private System.Windows.Forms.CheckBox newObj_Enabled;
        private System.Windows.Forms.ContextMenuStrip manipulateObj;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Enabled;
        private System.Windows.Forms.ToolStripMenuItem vlastnostiObjektuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem zrušitSílyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem změnitVlastnostiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem analyzovatToolStripMenuItem;
    }
}

