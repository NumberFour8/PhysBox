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
            this.tab_Toolbox.SuspendLayout();
            this.tool_newObj.SuspendLayout();
            this.newObj.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tab_Toolbox
            // 
            this.tab_Toolbox.Controls.Add(this.tool_newObj);
            this.tab_Toolbox.Controls.Add(this.tabPage2);
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
            this.newObj_Enabled.Location = new System.Drawing.Point(14, 181);
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
            this.newObj_AutoName.Location = new System.Drawing.Point(14, 157);
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
    }
}