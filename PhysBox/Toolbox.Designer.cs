namespace PhysBox
{
    partial class Toolbox
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
            this.tab_Toolbox = new System.Windows.Forms.TabControl();
            this.tool_newObj = new System.Windows.Forms.TabPage();
            this.newobj_Saved = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.newObj_Insert = new System.Windows.Forms.Button();
            this.newObj = new System.Windows.Forms.GroupBox();
            this.createGeometry = new System.Windows.Forms.Button();
            this.newObj_Enabled = new System.Windows.Forms.CheckBox();
            this.newObj_AutoName = new System.Windows.Forms.CheckBox();
            this.newObj_Name = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.newobj_Material = new System.Windows.Forms.ComboBox();
            this.newobj_Mass = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.newobj_Geometry = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.objProps = new System.Windows.Forms.GroupBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.env_G = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.env_Resolution = new System.Windows.Forms.MaskedTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.env_Aether = new System.Windows.Forms.MaskedTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.env_StepSize = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.button_applyEnv = new System.Windows.Forms.Button();
            this.button_resetEnv = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.list_allObjects = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.tab_Toolbox.SuspendLayout();
            this.tool_newObj.SuspendLayout();
            this.newObj.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_Toolbox
            // 
            this.tab_Toolbox.Controls.Add(this.tool_newObj);
            this.tab_Toolbox.Controls.Add(this.tabPage2);
            this.tab_Toolbox.Controls.Add(this.tabPage1);
            this.tab_Toolbox.Controls.Add(this.tabPage3);
            this.tab_Toolbox.Location = new System.Drawing.Point(1, 3);
            this.tab_Toolbox.Name = "tab_Toolbox";
            this.tab_Toolbox.SelectedIndex = 0;
            this.tab_Toolbox.Size = new System.Drawing.Size(233, 367);
            this.tab_Toolbox.TabIndex = 0;
            // 
            // tool_newObj
            // 
            this.tool_newObj.Controls.Add(this.newobj_Saved);
            this.tool_newObj.Controls.Add(this.label1);
            this.tool_newObj.Controls.Add(this.newObj_Insert);
            this.tool_newObj.Controls.Add(this.newObj);
            this.tool_newObj.Location = new System.Drawing.Point(4, 22);
            this.tool_newObj.Name = "tool_newObj";
            this.tool_newObj.Padding = new System.Windows.Forms.Padding(3);
            this.tool_newObj.Size = new System.Drawing.Size(225, 341);
            this.tool_newObj.TabIndex = 0;
            this.tool_newObj.Text = "Nový objekt";
            this.tool_newObj.UseVisualStyleBackColor = true;
            // 
            // newobj_Saved
            // 
            this.newobj_Saved.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newobj_Saved.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newobj_Saved.FormattingEnabled = true;
            this.newobj_Saved.Location = new System.Drawing.Point(7, 314);
            this.newobj_Saved.Name = "newobj_Saved";
            this.newobj_Saved.Size = new System.Drawing.Size(133, 21);
            this.newobj_Saved.TabIndex = 7;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 300);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "nebo předdefinovaný model";
            // 
            // newObj_Insert
            // 
            this.newObj_Insert.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newObj_Insert.Location = new System.Drawing.Point(146, 312);
            this.newObj_Insert.Name = "newObj_Insert";
            this.newObj_Insert.Size = new System.Drawing.Size(73, 23);
            this.newObj_Insert.TabIndex = 5;
            this.newObj_Insert.Text = "Vložit";
            this.newObj_Insert.UseVisualStyleBackColor = true;
            this.newObj_Insert.Click += new System.EventHandler(this.newObj_Insert_Click);
            // 
            // newObj
            // 
            this.newObj.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newObj.Controls.Add(this.createGeometry);
            this.newObj.Controls.Add(this.newObj_Enabled);
            this.newObj.Controls.Add(this.newObj_AutoName);
            this.newObj.Controls.Add(this.newObj_Name);
            this.newObj.Controls.Add(this.label3);
            this.newObj.Controls.Add(this.newobj_Material);
            this.newObj.Controls.Add(this.newobj_Mass);
            this.newObj.Controls.Add(this.label2);
            this.newObj.Controls.Add(this.newobj_Geometry);
            this.newObj.Location = new System.Drawing.Point(7, 6);
            this.newObj.Name = "newObj";
            this.newObj.Size = new System.Drawing.Size(212, 291);
            this.newObj.TabIndex = 4;
            this.newObj.TabStop = false;
            this.newObj.Text = "Parametry";
            // 
            // createGeometry
            // 
            this.createGeometry.Location = new System.Drawing.Point(90, 46);
            this.createGeometry.Name = "createGeometry";
            this.createGeometry.Size = new System.Drawing.Size(116, 22);
            this.createGeometry.TabIndex = 10;
            this.createGeometry.Text = "Vytvořit geometrii";
            this.createGeometry.UseVisualStyleBackColor = true;
            this.createGeometry.Click += new System.EventHandler(this.createGeometry_Click);
            // 
            // newObj_Enabled
            // 
            this.newObj_Enabled.AutoSize = true;
            this.newObj_Enabled.Checked = true;
            this.newObj_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.newObj_Enabled.Location = new System.Drawing.Point(14, 229);
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
            this.newObj_AutoName.Location = new System.Drawing.Point(14, 206);
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
            this.newObj_Name.Location = new System.Drawing.Point(90, 131);
            this.newObj_Name.Name = "newObj_Name";
            this.newObj_Name.Size = new System.Drawing.Size(116, 20);
            this.newObj_Name.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 134);
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
            this.newobj_Material.Location = new System.Drawing.Point(11, 74);
            this.newobj_Material.Name = "newobj_Material";
            this.newobj_Material.Size = new System.Drawing.Size(195, 21);
            this.newobj_Material.TabIndex = 5;
            this.newobj_Material.Text = "<vyber materiál>";
            // 
            // newobj_Mass
            // 
            this.newobj_Mass.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newobj_Mass.BeepOnError = true;
            this.newobj_Mass.Location = new System.Drawing.Point(90, 101);
            this.newobj_Mass.Mask = "0000000";
            this.newobj_Mass.Name = "newobj_Mass";
            this.newobj_Mass.PromptChar = ' ';
            this.newobj_Mass.Size = new System.Drawing.Size(116, 20);
            this.newobj_Mass.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 104);
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
            this.newobj_Geometry.Size = new System.Drawing.Size(195, 21);
            this.newobj_Geometry.TabIndex = 1;
            this.newobj_Geometry.Text = "<vyber geometrii>";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.objProps);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(225, 341);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Vlastnosti";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // objProps
            // 
            this.objProps.Location = new System.Drawing.Point(3, 6);
            this.objProps.Name = "objProps";
            this.objProps.Size = new System.Drawing.Size(219, 332);
            this.objProps.TabIndex = 0;
            this.objProps.TabStop = false;
            this.objProps.Text = "Žádný objekt";
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(225, 341);
            this.tabPage1.TabIndex = 2;
            this.tabPage1.Text = "Tělesa";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.groupBox1);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(225, 341);
            this.tabPage3.TabIndex = 3;
            this.tabPage3.Text = "Prostředí";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.button_resetEnv);
            this.groupBox1.Controls.Add(this.button_applyEnv);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.env_StepSize);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.env_Aether);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.env_Resolution);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.env_G);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 171);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametry simulace";
            // 
            // env_G
            // 
            this.env_G.Location = new System.Drawing.Point(122, 27);
            this.env_G.Name = "env_G";
            this.env_G.Size = new System.Drawing.Size(51, 20);
            this.env_G.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 30);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(109, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Gravitační zrychlení :";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Rozlišení :";
            // 
            // env_Resolution
            // 
            this.env_Resolution.Location = new System.Drawing.Point(122, 53);
            this.env_Resolution.Name = "env_Resolution";
            this.env_Resolution.Size = new System.Drawing.Size(51, 20);
            this.env_Resolution.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(175, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(31, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "px/m";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(176, 30);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(37, 13);
            this.label7.TabIndex = 5;
            this.label7.Text = "m/s^2";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 13);
            this.label8.TabIndex = 6;
            this.label8.Text = "Hustota prostředí :";
            // 
            // env_Aether
            // 
            this.env_Aether.Location = new System.Drawing.Point(122, 79);
            this.env_Aether.Name = "env_Aether";
            this.env_Aether.Size = new System.Drawing.Size(51, 20);
            this.env_Aether.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(175, 82);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(44, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "kg/m^3";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 106);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(113, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Doba kroku simulace :";
            // 
            // env_StepSize
            // 
            this.env_StepSize.Location = new System.Drawing.Point(122, 103);
            this.env_StepSize.Name = "env_StepSize";
            this.env_StepSize.Size = new System.Drawing.Size(51, 20);
            this.env_StepSize.TabIndex = 10;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(176, 106);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(20, 13);
            this.label11.TabIndex = 11;
            this.label11.Text = "ms";
            // 
            // button_applyEnv
            // 
            this.button_applyEnv.Location = new System.Drawing.Point(135, 144);
            this.button_applyEnv.Name = "button_applyEnv";
            this.button_applyEnv.Size = new System.Drawing.Size(75, 21);
            this.button_applyEnv.TabIndex = 12;
            this.button_applyEnv.Text = "Použít";
            this.button_applyEnv.UseVisualStyleBackColor = true;
            // 
            // button_resetEnv
            // 
            this.button_resetEnv.Location = new System.Drawing.Point(6, 144);
            this.button_resetEnv.Name = "button_resetEnv";
            this.button_resetEnv.Size = new System.Drawing.Size(75, 21);
            this.button_resetEnv.TabIndex = 13;
            this.button_resetEnv.Text = "Původní";
            this.button_resetEnv.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button4);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.list_allObjects);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 328);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Všechna tělesa v prostředí";
            // 
            // list_allObjects
            // 
            this.list_allObjects.FormattingEnabled = true;
            this.list_allObjects.Location = new System.Drawing.Point(4, 19);
            this.list_allObjects.Name = "list_allObjects";
            this.list_allObjects.Size = new System.Drawing.Size(209, 225);
            this.list_allObjects.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 276);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 20);
            this.button1.TabIndex = 1;
            this.button1.Text = "Smazat tělesa mimo poloměr";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 250);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(97, 20);
            this.button2.TabIndex = 2;
            this.button2.Text = "Smazat";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(116, 250);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(97, 20);
            this.button3.TabIndex = 3;
            this.button3.Text = "Vybrat";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(27, 302);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(168, 20);
            this.button4.TabIndex = 4;
            this.button4.Text = "Přesunout tělesa mimo poloměr";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // Toolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(234, 371);
            this.Controls.Add(this.tab_Toolbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "Toolbox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Nástroje";
            this.tab_Toolbox.ResumeLayout(false);
            this.tool_newObj.ResumeLayout(false);
            this.tool_newObj.PerformLayout();
            this.newObj.ResumeLayout(false);
            this.newObj.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_Toolbox;
        private System.Windows.Forms.TabPage tool_newObj;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox newobj_Saved;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox newObj;
        private System.Windows.Forms.CheckBox newObj_Enabled;
        private System.Windows.Forms.CheckBox newObj_AutoName;
        private System.Windows.Forms.TextBox newObj_Name;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox newobj_Material;
        private System.Windows.Forms.MaskedTextBox newobj_Mass;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox newobj_Geometry;
        private System.Windows.Forms.Button createGeometry;
        public System.Windows.Forms.Button newObj_Insert;
        private System.Windows.Forms.GroupBox objProps;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.MaskedTextBox env_Aether;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.MaskedTextBox env_Resolution;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox env_G;
        private System.Windows.Forms.Button button_resetEnv;
        private System.Windows.Forms.Button button_applyEnv;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.MaskedTextBox env_StepSize;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox list_allObjects;
    }
}