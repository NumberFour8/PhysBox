namespace PhysBox
{
    partial class CreateObject
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.drawPanel = new System.Windows.Forms.Panel();
            this.button_OK = new System.Windows.Forms.Button();
            this.colorDialog = new System.Windows.Forms.ColorDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.text_objName = new System.Windows.Forms.TextBox();
            this.button_selColor = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.text_objDrag = new System.Windows.Forms.MaskedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.text_objDepth = new System.Windows.Forms.MaskedTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.text_objTension = new System.Windows.Forms.MaskedTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label_COG = new System.Windows.Forms.Label();
            this.label_cCOG = new System.Windows.Forms.Label();
            this.button_selTexture = new System.Windows.Forms.Button();
            this.textureDialog = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.drawPanel);
            this.groupBox1.Location = new System.Drawing.Point(2, 2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 188);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Objekt (vybrat body)";
            // 
            // drawPanel
            // 
            this.drawPanel.Location = new System.Drawing.Point(0, 20);
            this.drawPanel.Name = "drawPanel";
            this.drawPanel.Size = new System.Drawing.Size(359, 162);
            this.drawPanel.TabIndex = 0;
            this.drawPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.drawPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // button_OK
            // 
            this.button_OK.Enabled = false;
            this.button_OK.Location = new System.Drawing.Point(286, 270);
            this.button_OK.Name = "button_OK";
            this.button_OK.Size = new System.Drawing.Size(75, 23);
            this.button_OK.TabIndex = 2;
            this.button_OK.Text = "Hotovo";
            this.button_OK.UseVisualStyleBackColor = true;
            this.button_OK.Click += new System.EventHandler(this.button_OK_Click);
            // 
            // colorDialog
            // 
            this.colorDialog.FullOpen = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(9, 198);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Název: ";
            // 
            // text_objName
            // 
            this.text_objName.Enabled = false;
            this.text_objName.Location = new System.Drawing.Point(59, 195);
            this.text_objName.Name = "text_objName";
            this.text_objName.Size = new System.Drawing.Size(129, 20);
            this.text_objName.TabIndex = 8;
            this.text_objName.TextChanged += new System.EventHandler(this.text_mass_TextChanged);
            // 
            // button_selColor
            // 
            this.button_selColor.Enabled = false;
            this.button_selColor.Location = new System.Drawing.Point(197, 194);
            this.button_selColor.Name = "button_selColor";
            this.button_selColor.Size = new System.Drawing.Size(78, 21);
            this.button_selColor.TabIndex = 7;
            this.button_selColor.Text = "Barva...";
            this.button_selColor.UseVisualStyleBackColor = true;
            this.button_selColor.Click += new System.EventHandler(this.button_selColor_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 226);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Koeficient odporu:";
            // 
            // text_objDrag
            // 
            this.text_objDrag.Location = new System.Drawing.Point(114, 224);
            this.text_objDrag.Mask = "0.00";
            this.text_objDrag.Name = "text_objDrag";
            this.text_objDrag.PromptChar = ' ';
            this.text_objDrag.Size = new System.Drawing.Size(50, 20);
            this.text_objDrag.TabIndex = 11;
            this.text_objDrag.Text = "123";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 250);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Hloubka útvaru:";
            // 
            // text_objDepth
            // 
            this.text_objDepth.Location = new System.Drawing.Point(114, 247);
            this.text_objDepth.Mask = "0000";
            this.text_objDepth.Name = "text_objDepth";
            this.text_objDepth.PromptChar = ' ';
            this.text_objDepth.Size = new System.Drawing.Size(50, 20);
            this.text_objDepth.TabIndex = 13;
            this.text_objDepth.Text = "10";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(170, 250);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(18, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "px";
            // 
            // text_objTension
            // 
            this.text_objTension.Location = new System.Drawing.Point(114, 273);
            this.text_objTension.Mask = "0.0";
            this.text_objTension.Name = "text_objTension";
            this.text_objTension.PromptChar = ' ';
            this.text_objTension.Size = new System.Drawing.Size(50, 20);
            this.text_objTension.TabIndex = 16;
            this.text_objTension.Text = "00";
            this.text_objTension.TypeValidationCompleted += new System.Windows.Forms.TypeValidationEventHandler(this.text_objTension_TypeValidationCompleted);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 275);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Zaoblení útvaru:";
            // 
            // label_COG
            // 
            this.label_COG.AutoSize = true;
            this.label_COG.Location = new System.Drawing.Point(194, 250);
            this.label_COG.Name = "label_COG";
            this.label_COG.Size = new System.Drawing.Size(81, 13);
            this.label_COG.TabIndex = 17;
            this.label_COG.Text = "Určené těžiště :";
            // 
            // label_cCOG
            // 
            this.label_cCOG.AutoSize = true;
            this.label_cCOG.Location = new System.Drawing.Point(194, 227);
            this.label_cCOG.Name = "label_cCOG";
            this.label_cCOG.Size = new System.Drawing.Size(96, 13);
            this.label_cCOG.TabIndex = 18;
            this.label_cCOG.Text = "Spočítané těžiště :";
            // 
            // button_selTexture
            // 
            this.button_selTexture.Enabled = false;
            this.button_selTexture.Location = new System.Drawing.Point(283, 194);
            this.button_selTexture.Name = "button_selTexture";
            this.button_selTexture.Size = new System.Drawing.Size(78, 21);
            this.button_selTexture.TabIndex = 19;
            this.button_selTexture.Text = "Textura...";
            this.button_selTexture.UseVisualStyleBackColor = true;
            this.button_selTexture.Click += new System.EventHandler(this.button1_Click);
            // 
            // textureDialog
            // 
            this.textureDialog.Title = "Vybrat texturu...";
            // 
            // CreateObject
            // 
            this.AcceptButton = this.button_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(364, 297);
            this.Controls.Add(this.button_selTexture);
            this.Controls.Add(this.label_cCOG);
            this.Controls.Add(this.label_COG);
            this.Controls.Add(this.text_objTension);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.text_objDepth);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.text_objDrag);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text_objName);
            this.Controls.Add(this.button_selColor);
            this.Controls.Add(this.button_OK);
            this.Controls.Add(this.groupBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateObject";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vytvořit objekt";
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button button_OK;
        private System.Windows.Forms.ColorDialog colorDialog;
        private System.Windows.Forms.Panel drawPanel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox text_objName;
        private System.Windows.Forms.Button button_selColor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MaskedTextBox text_objDrag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MaskedTextBox text_objDepth;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox text_objTension;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label_COG;
        private System.Windows.Forms.Label label_cCOG;
        private System.Windows.Forms.Button button_selTexture;
        private System.Windows.Forms.OpenFileDialog textureDialog;
    }
}