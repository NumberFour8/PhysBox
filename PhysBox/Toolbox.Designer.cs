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
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label11;
            System.Windows.Forms.Label label10;
            System.Windows.Forms.Label label9;
            System.Windows.Forms.Label label8;
            System.Windows.Forms.Label label7;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label propMass;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label12;
            System.Windows.Forms.Label label13;
            this.tab_Toolbox = new System.Windows.Forms.TabControl();
            this.tool_newObj = new System.Windows.Forms.TabPage();
            this.newObj_Insert = new System.Windows.Forms.Button();
            this.newObj = new System.Windows.Forms.GroupBox();
            this.newObj_Mass = new System.Windows.Forms.TextBox();
            this.newObj_Static = new System.Windows.Forms.CheckBox();
            this.createGeometry = new System.Windows.Forms.Button();
            this.newObj_Enabled = new System.Windows.Forms.CheckBox();
            this.newObj_AutoName = new System.Windows.Forms.CheckBox();
            this.newObj_Name = new System.Windows.Forms.TextBox();
            this.newObj_Material = new System.Windows.Forms.ComboBox();
            this.newObj_Geometry = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.objProps = new System.Windows.Forms.GroupBox();
            this.propParams = new System.Windows.Forms.GroupBox();
            this.prop_InitialEnergy = new System.Windows.Forms.Label();
            this.prop_Material = new System.Windows.Forms.Label();
            this.prop_MomInertia = new System.Windows.Forms.Label();
            this.prop_Mass = new System.Windows.Forms.Label();
            this.propValues = new System.Windows.Forms.GroupBox();
            this.prop_rotationalEnergy = new System.Windows.Forms.Label();
            this.radio_rotationalEnergy = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.prop_potentialEnergy = new System.Windows.Forms.Label();
            this.radio_KineticEnergy = new System.Windows.Forms.RadioButton();
            this.radio_AngularVelocity = new System.Windows.Forms.RadioButton();
            this.radio_LinearVelocity = new System.Windows.Forms.RadioButton();
            this.prop_kineticEnergy = new System.Windows.Forms.Label();
            this.prop_angularVelocity = new System.Windows.Forms.Label();
            this.prop_Velocity = new System.Windows.Forms.Label();
            this.button_Analyze = new System.Windows.Forms.Button();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.objs_MoveSelected = new System.Windows.Forms.Button();
            this.objs_SelectObject = new System.Windows.Forms.Button();
            this.objs_DeleteObject = new System.Windows.Forms.Button();
            this.list_allObjects = new System.Windows.Forms.ListBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.check_Collisions = new System.Windows.Forms.CheckBox();
            this.button_applyEnv = new System.Windows.Forms.Button();
            this.env_StepSize = new System.Windows.Forms.MaskedTextBox();
            this.env_Aether = new System.Windows.Forms.MaskedTextBox();
            this.env_Resolution = new System.Windows.Forms.MaskedTextBox();
            this.env_G = new System.Windows.Forms.MaskedTextBox();
            label3 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label11 = new System.Windows.Forms.Label();
            label10 = new System.Windows.Forms.Label();
            label9 = new System.Windows.Forms.Label();
            label8 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            propMass = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label12 = new System.Windows.Forms.Label();
            label13 = new System.Windows.Forms.Label();
            this.tab_Toolbox.SuspendLayout();
            this.tool_newObj.SuspendLayout();
            this.newObj.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.objProps.SuspendLayout();
            this.propParams.SuspendLayout();
            this.propValues.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label3
            // 
            label3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(11, 134);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(44, 13);
            label3.TabIndex = 6;
            label3.Text = "Název :";
            // 
            // label2
            // 
            label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(11, 104);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(73, 13);
            label2.TabIndex = 3;
            label2.Text = "Hmotnost (g) :";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new System.Drawing.Point(176, 106);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(20, 13);
            label11.TabIndex = 11;
            label11.Text = "ms";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(6, 106);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(113, 13);
            label10.TabIndex = 9;
            label10.Text = "Doba kroku simulace :";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(175, 82);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(44, 13);
            label9.TabIndex = 8;
            label9.Text = "kg/m^3";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new System.Drawing.Point(6, 82);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(96, 13);
            label8.TabIndex = 6;
            label8.Text = "Hustota prostředí :";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(176, 30);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(37, 13);
            label7.TabIndex = 5;
            label7.Text = "m/s^2";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(175, 56);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(31, 13);
            label6.TabIndex = 4;
            label6.Text = "px/m";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(6, 56);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(57, 13);
            label5.TabIndex = 2;
            label5.Text = "Rozlišení :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(6, 30);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(109, 13);
            label4.TabIndex = 1;
            label4.Text = "Gravitační zrychlení :";
            // 
            // propMass
            // 
            propMass.AutoSize = true;
            propMass.Location = new System.Drawing.Point(6, 32);
            propMass.Name = "propMass";
            propMass.Size = new System.Drawing.Size(58, 13);
            propMass.TabIndex = 0;
            propMass.Text = "Hmotnost :";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(6, 77);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(53, 13);
            label1.TabIndex = 1;
            label1.Text = "Materiál : ";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new System.Drawing.Point(6, 54);
            label12.Name = "label12";
            label12.Size = new System.Drawing.Size(111, 13);
            label12.TabIndex = 2;
            label12.Text = "Moment setrvačnosti :";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new System.Drawing.Point(6, 99);
            label13.Name = "label13";
            label13.Size = new System.Drawing.Size(104, 13);
            label13.TabIndex = 3;
            label13.Text = "Počáteční energie : ";
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
            this.tab_Toolbox.Selected += new System.Windows.Forms.TabControlEventHandler(this.tab_Toolbox_Selected);
            // 
            // tool_newObj
            // 
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
            this.newObj.Controls.Add(this.newObj_Mass);
            this.newObj.Controls.Add(this.newObj_Static);
            this.newObj.Controls.Add(this.createGeometry);
            this.newObj.Controls.Add(this.newObj_Enabled);
            this.newObj.Controls.Add(this.newObj_AutoName);
            this.newObj.Controls.Add(this.newObj_Name);
            this.newObj.Controls.Add(label3);
            this.newObj.Controls.Add(this.newObj_Material);
            this.newObj.Controls.Add(label2);
            this.newObj.Controls.Add(this.newObj_Geometry);
            this.newObj.Location = new System.Drawing.Point(7, 6);
            this.newObj.Name = "newObj";
            this.newObj.Size = new System.Drawing.Size(212, 291);
            this.newObj.TabIndex = 4;
            this.newObj.TabStop = false;
            this.newObj.Text = "Parametry";
            // 
            // newObj_Mass
            // 
            this.newObj_Mass.Location = new System.Drawing.Point(90, 101);
            this.newObj_Mass.MaxLength = 10;
            this.newObj_Mass.Name = "newObj_Mass";
            this.newObj_Mass.Size = new System.Drawing.Size(116, 20);
            this.newObj_Mass.TabIndex = 12;
            // 
            // newObj_Static
            // 
            this.newObj_Static.AutoSize = true;
            this.newObj_Static.Location = new System.Drawing.Point(14, 252);
            this.newObj_Static.Name = "newObj_Static";
            this.newObj_Static.Size = new System.Drawing.Size(65, 17);
            this.newObj_Static.TabIndex = 11;
            this.newObj_Static.Text = "Statické";
            this.newObj_Static.UseVisualStyleBackColor = true;
            this.newObj_Static.CheckedChanged += new System.EventHandler(this.newObj_Static_CheckedChanged);
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
            // newObj_Material
            // 
            this.newObj_Material.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newObj_Material.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newObj_Material.FormattingEnabled = true;
            this.newObj_Material.Items.AddRange(new object[] {
            "Dřevo",
            "Beton",
            "Ocel",
            "Sklo",
            "Guma"});
            this.newObj_Material.Location = new System.Drawing.Point(11, 74);
            this.newObj_Material.Name = "newObj_Material";
            this.newObj_Material.Size = new System.Drawing.Size(195, 21);
            this.newObj_Material.TabIndex = 5;
            // 
            // newObj_Geometry
            // 
            this.newObj_Geometry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.newObj_Geometry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.newObj_Geometry.FormattingEnabled = true;
            this.newObj_Geometry.Location = new System.Drawing.Point(11, 19);
            this.newObj_Geometry.Name = "newObj_Geometry";
            this.newObj_Geometry.Size = new System.Drawing.Size(195, 21);
            this.newObj_Geometry.TabIndex = 1;
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
            this.objProps.Controls.Add(this.propParams);
            this.objProps.Controls.Add(this.propValues);
            this.objProps.Location = new System.Drawing.Point(3, 6);
            this.objProps.Name = "objProps";
            this.objProps.Size = new System.Drawing.Size(219, 332);
            this.objProps.TabIndex = 0;
            this.objProps.TabStop = false;
            this.objProps.Text = "Žádný objekt";
            // 
            // propParams
            // 
            this.propParams.Controls.Add(this.prop_InitialEnergy);
            this.propParams.Controls.Add(this.prop_Material);
            this.propParams.Controls.Add(this.prop_MomInertia);
            this.propParams.Controls.Add(this.prop_Mass);
            this.propParams.Controls.Add(label13);
            this.propParams.Controls.Add(label12);
            this.propParams.Controls.Add(label1);
            this.propParams.Controls.Add(propMass);
            this.propParams.Location = new System.Drawing.Point(6, 196);
            this.propParams.Name = "propParams";
            this.propParams.Size = new System.Drawing.Size(207, 130);
            this.propParams.TabIndex = 1;
            this.propParams.TabStop = false;
            this.propParams.Text = "Parametry";
            // 
            // prop_InitialEnergy
            // 
            this.prop_InitialEnergy.AutoSize = true;
            this.prop_InitialEnergy.Location = new System.Drawing.Point(126, 99);
            this.prop_InitialEnergy.Name = "prop_InitialEnergy";
            this.prop_InitialEnergy.Size = new System.Drawing.Size(21, 13);
            this.prop_InitialEnergy.TabIndex = 7;
            this.prop_InitialEnergy.Text = "0 J";
            // 
            // prop_Material
            // 
            this.prop_Material.AutoSize = true;
            this.prop_Material.Location = new System.Drawing.Point(126, 77);
            this.prop_Material.Name = "prop_Material";
            this.prop_Material.Size = new System.Drawing.Size(35, 13);
            this.prop_Material.TabIndex = 6;
            this.prop_Material.Text = "žádný";
            // 
            // prop_MomInertia
            // 
            this.prop_MomInertia.AutoSize = true;
            this.prop_MomInertia.Location = new System.Drawing.Point(126, 54);
            this.prop_MomInertia.Name = "prop_MomInertia";
            this.prop_MomInertia.Size = new System.Drawing.Size(57, 13);
            this.prop_MomInertia.TabIndex = 5;
            this.prop_MomInertia.Text = "0 kg . m^2";
            // 
            // prop_Mass
            // 
            this.prop_Mass.AutoSize = true;
            this.prop_Mass.Location = new System.Drawing.Point(126, 32);
            this.prop_Mass.Name = "prop_Mass";
            this.prop_Mass.Size = new System.Drawing.Size(28, 13);
            this.prop_Mass.TabIndex = 4;
            this.prop_Mass.Text = "0 kg";
            // 
            // propValues
            // 
            this.propValues.Controls.Add(this.prop_rotationalEnergy);
            this.propValues.Controls.Add(this.radio_rotationalEnergy);
            this.propValues.Controls.Add(this.radioButton1);
            this.propValues.Controls.Add(this.prop_potentialEnergy);
            this.propValues.Controls.Add(this.radio_KineticEnergy);
            this.propValues.Controls.Add(this.radio_AngularVelocity);
            this.propValues.Controls.Add(this.radio_LinearVelocity);
            this.propValues.Controls.Add(this.prop_kineticEnergy);
            this.propValues.Controls.Add(this.prop_angularVelocity);
            this.propValues.Controls.Add(this.prop_Velocity);
            this.propValues.Controls.Add(this.button_Analyze);
            this.propValues.Location = new System.Drawing.Point(6, 19);
            this.propValues.Name = "propValues";
            this.propValues.Size = new System.Drawing.Size(207, 171);
            this.propValues.TabIndex = 0;
            this.propValues.TabStop = false;
            this.propValues.Text = "Hodnoty";
            // 
            // prop_rotationalEnergy
            // 
            this.prop_rotationalEnergy.AutoSize = true;
            this.prop_rotationalEnergy.Location = new System.Drawing.Point(126, 112);
            this.prop_rotationalEnergy.Name = "prop_rotationalEnergy";
            this.prop_rotationalEnergy.Size = new System.Drawing.Size(21, 13);
            this.prop_rotationalEnergy.TabIndex = 28;
            this.prop_rotationalEnergy.Text = "0 J";
            // 
            // radio_rotationalEnergy
            // 
            this.radio_rotationalEnergy.AutoSize = true;
            this.radio_rotationalEnergy.Location = new System.Drawing.Point(6, 108);
            this.radio_rotationalEnergy.Name = "radio_rotationalEnergy";
            this.radio_rotationalEnergy.Size = new System.Drawing.Size(106, 17);
            this.radio_rotationalEnergy.TabIndex = 27;
            this.radio_rotationalEnergy.Text = "Otáčivá energie :";
            this.radio_rotationalEnergy.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(6, 85);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(123, 17);
            this.radioButton1.TabIndex = 26;
            this.radioButton1.Text = "Potenciální energie :";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // prop_potentialEnergy
            // 
            this.prop_potentialEnergy.AutoSize = true;
            this.prop_potentialEnergy.Location = new System.Drawing.Point(126, 87);
            this.prop_potentialEnergy.Name = "prop_potentialEnergy";
            this.prop_potentialEnergy.Size = new System.Drawing.Size(21, 13);
            this.prop_potentialEnergy.TabIndex = 25;
            this.prop_potentialEnergy.Text = "0 J";
            // 
            // radio_KineticEnergy
            // 
            this.radio_KineticEnergy.AutoSize = true;
            this.radio_KineticEnergy.Location = new System.Drawing.Point(6, 62);
            this.radio_KineticEnergy.Name = "radio_KineticEnergy";
            this.radio_KineticEnergy.Size = new System.Drawing.Size(113, 17);
            this.radio_KineticEnergy.TabIndex = 23;
            this.radio_KineticEnergy.Text = "Kinetická energie :";
            this.radio_KineticEnergy.UseVisualStyleBackColor = true;
            // 
            // radio_AngularVelocity
            // 
            this.radio_AngularVelocity.AutoSize = true;
            this.radio_AngularVelocity.Location = new System.Drawing.Point(6, 39);
            this.radio_AngularVelocity.Name = "radio_AngularVelocity";
            this.radio_AngularVelocity.Size = new System.Drawing.Size(104, 17);
            this.radio_AngularVelocity.TabIndex = 20;
            this.radio_AngularVelocity.Text = "Úhlová rychlost :";
            this.radio_AngularVelocity.UseVisualStyleBackColor = true;
            // 
            // radio_LinearVelocity
            // 
            this.radio_LinearVelocity.AutoSize = true;
            this.radio_LinearVelocity.Checked = true;
            this.radio_LinearVelocity.Location = new System.Drawing.Point(6, 16);
            this.radio_LinearVelocity.Name = "radio_LinearVelocity";
            this.radio_LinearVelocity.Size = new System.Drawing.Size(72, 17);
            this.radio_LinearVelocity.TabIndex = 19;
            this.radio_LinearVelocity.TabStop = true;
            this.radio_LinearVelocity.Text = "Rychlost :";
            this.radio_LinearVelocity.UseVisualStyleBackColor = true;
            // 
            // prop_kineticEnergy
            // 
            this.prop_kineticEnergy.AutoSize = true;
            this.prop_kineticEnergy.Location = new System.Drawing.Point(126, 64);
            this.prop_kineticEnergy.Name = "prop_kineticEnergy";
            this.prop_kineticEnergy.Size = new System.Drawing.Size(21, 13);
            this.prop_kineticEnergy.TabIndex = 17;
            this.prop_kineticEnergy.Text = "0 J";
            // 
            // prop_angularVelocity
            // 
            this.prop_angularVelocity.AutoSize = true;
            this.prop_angularVelocity.Location = new System.Drawing.Point(126, 41);
            this.prop_angularVelocity.Name = "prop_angularVelocity";
            this.prop_angularVelocity.Size = new System.Drawing.Size(41, 13);
            this.prop_angularVelocity.TabIndex = 14;
            this.prop_angularVelocity.Text = "0 rad/s";
            // 
            // prop_Velocity
            // 
            this.prop_Velocity.AutoSize = true;
            this.prop_Velocity.Location = new System.Drawing.Point(126, 20);
            this.prop_Velocity.Name = "prop_Velocity";
            this.prop_Velocity.Size = new System.Drawing.Size(34, 13);
            this.prop_Velocity.TabIndex = 13;
            this.prop_Velocity.Text = "0 m/s";
            // 
            // button_Analyze
            // 
            this.button_Analyze.Enabled = false;
            this.button_Analyze.Location = new System.Drawing.Point(126, 145);
            this.button_Analyze.Name = "button_Analyze";
            this.button_Analyze.Size = new System.Drawing.Size(75, 20);
            this.button_Analyze.TabIndex = 12;
            this.button_Analyze.Text = "Analyzovat";
            this.button_Analyze.UseVisualStyleBackColor = true;
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.objs_MoveSelected);
            this.groupBox2.Controls.Add(this.objs_SelectObject);
            this.groupBox2.Controls.Add(this.objs_DeleteObject);
            this.groupBox2.Controls.Add(this.list_allObjects);
            this.groupBox2.Location = new System.Drawing.Point(3, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(219, 328);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Všechna tělesa v prostředí";
            // 
            // objs_MoveSelected
            // 
            this.objs_MoveSelected.Location = new System.Drawing.Point(27, 276);
            this.objs_MoveSelected.Name = "objs_MoveSelected";
            this.objs_MoveSelected.Size = new System.Drawing.Size(168, 20);
            this.objs_MoveSelected.TabIndex = 4;
            this.objs_MoveSelected.Text = "Přesunout vybrané";
            this.objs_MoveSelected.UseVisualStyleBackColor = true;
            this.objs_MoveSelected.Click += new System.EventHandler(this.button4_Click);
            // 
            // objs_SelectObject
            // 
            this.objs_SelectObject.Location = new System.Drawing.Point(116, 250);
            this.objs_SelectObject.Name = "objs_SelectObject";
            this.objs_SelectObject.Size = new System.Drawing.Size(97, 20);
            this.objs_SelectObject.TabIndex = 3;
            this.objs_SelectObject.Text = "Vybrat";
            this.objs_SelectObject.UseVisualStyleBackColor = true;
            this.objs_SelectObject.Click += new System.EventHandler(this.objs_SelectObject_Click);
            // 
            // objs_DeleteObject
            // 
            this.objs_DeleteObject.Location = new System.Drawing.Point(6, 250);
            this.objs_DeleteObject.Name = "objs_DeleteObject";
            this.objs_DeleteObject.Size = new System.Drawing.Size(97, 20);
            this.objs_DeleteObject.TabIndex = 2;
            this.objs_DeleteObject.Text = "Smazat";
            this.objs_DeleteObject.UseVisualStyleBackColor = true;
            this.objs_DeleteObject.Click += new System.EventHandler(this.objs_DeleteObject_Click);
            // 
            // list_allObjects
            // 
            this.list_allObjects.FormattingEnabled = true;
            this.list_allObjects.Location = new System.Drawing.Point(4, 19);
            this.list_allObjects.Name = "list_allObjects";
            this.list_allObjects.Size = new System.Drawing.Size(209, 225);
            this.list_allObjects.TabIndex = 0;
            this.list_allObjects.SelectedIndexChanged += new System.EventHandler(this.list_allObjects_SelectedIndexChanged);
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
            this.groupBox1.Controls.Add(this.check_Collisions);
            this.groupBox1.Controls.Add(this.button_applyEnv);
            this.groupBox1.Controls.Add(label11);
            this.groupBox1.Controls.Add(this.env_StepSize);
            this.groupBox1.Controls.Add(label10);
            this.groupBox1.Controls.Add(label9);
            this.groupBox1.Controls.Add(this.env_Aether);
            this.groupBox1.Controls.Add(label8);
            this.groupBox1.Controls.Add(label7);
            this.groupBox1.Controls.Add(label6);
            this.groupBox1.Controls.Add(this.env_Resolution);
            this.groupBox1.Controls.Add(label5);
            this.groupBox1.Controls.Add(label4);
            this.groupBox1.Controls.Add(this.env_G);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(216, 237);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Parametry simulace";
            // 
            // check_Collisions
            // 
            this.check_Collisions.AutoSize = true;
            this.check_Collisions.Location = new System.Drawing.Point(9, 159);
            this.check_Collisions.Name = "check_Collisions";
            this.check_Collisions.Size = new System.Drawing.Size(96, 17);
            this.check_Collisions.TabIndex = 14;
            this.check_Collisions.Text = "Zapnout kolize";
            this.check_Collisions.UseVisualStyleBackColor = true;
            // 
            // button_applyEnv
            // 
            this.button_applyEnv.Location = new System.Drawing.Point(138, 210);
            this.button_applyEnv.Name = "button_applyEnv";
            this.button_applyEnv.Size = new System.Drawing.Size(75, 21);
            this.button_applyEnv.TabIndex = 12;
            this.button_applyEnv.Text = "Použít";
            this.button_applyEnv.UseVisualStyleBackColor = true;
            this.button_applyEnv.Click += new System.EventHandler(this.button_applyEnv_Click);
            // 
            // env_StepSize
            // 
            this.env_StepSize.Location = new System.Drawing.Point(122, 103);
            this.env_StepSize.Mask = "0999";
            this.env_StepSize.Name = "env_StepSize";
            this.env_StepSize.PromptChar = ' ';
            this.env_StepSize.Size = new System.Drawing.Size(51, 20);
            this.env_StepSize.TabIndex = 10;
            // 
            // env_Aether
            // 
            this.env_Aether.Location = new System.Drawing.Point(122, 79);
            this.env_Aether.Mask = "990.999";
            this.env_Aether.Name = "env_Aether";
            this.env_Aether.PromptChar = ' ';
            this.env_Aether.Size = new System.Drawing.Size(51, 20);
            this.env_Aether.TabIndex = 7;
            // 
            // env_Resolution
            // 
            this.env_Resolution.Location = new System.Drawing.Point(122, 53);
            this.env_Resolution.Mask = "099";
            this.env_Resolution.Name = "env_Resolution";
            this.env_Resolution.PromptChar = ' ';
            this.env_Resolution.Size = new System.Drawing.Size(51, 20);
            this.env_Resolution.TabIndex = 3;
            // 
            // env_G
            // 
            this.env_G.Location = new System.Drawing.Point(122, 27);
            this.env_G.Name = "env_G";
            this.env_G.PromptChar = ' ';
            this.env_G.Size = new System.Drawing.Size(51, 20);
            this.env_G.TabIndex = 0;
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
            this.newObj.ResumeLayout(false);
            this.newObj.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.objProps.ResumeLayout(false);
            this.propParams.ResumeLayout(false);
            this.propParams.PerformLayout();
            this.propValues.ResumeLayout(false);
            this.propValues.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tab_Toolbox;
        private System.Windows.Forms.TabPage tool_newObj;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox newObj;
        private System.Windows.Forms.CheckBox newObj_Enabled;
        private System.Windows.Forms.CheckBox newObj_AutoName;
        private System.Windows.Forms.TextBox newObj_Name;
        private System.Windows.Forms.ComboBox newObj_Material;
        private System.Windows.Forms.ComboBox newObj_Geometry;
        private System.Windows.Forms.Button createGeometry;
        public System.Windows.Forms.Button newObj_Insert;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.MaskedTextBox env_Aether;
        private System.Windows.Forms.MaskedTextBox env_Resolution;
        private System.Windows.Forms.MaskedTextBox env_G;
        private System.Windows.Forms.Button button_applyEnv;
        private System.Windows.Forms.MaskedTextBox env_StepSize;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button objs_MoveSelected;
        private System.Windows.Forms.Button objs_SelectObject;
        private System.Windows.Forms.Button objs_DeleteObject;
        private System.Windows.Forms.ListBox list_allObjects;
        private System.Windows.Forms.GroupBox propParams;
        private System.Windows.Forms.GroupBox propValues;
        private System.Windows.Forms.Button button_Analyze;
        private System.Windows.Forms.CheckBox check_Collisions;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radio_KineticEnergy;
        private System.Windows.Forms.RadioButton radio_AngularVelocity;
        private System.Windows.Forms.RadioButton radio_LinearVelocity;
        public System.Windows.Forms.Label prop_kineticEnergy;
        public System.Windows.Forms.Label prop_angularVelocity;
        public System.Windows.Forms.Label prop_Velocity;
        public System.Windows.Forms.Label prop_potentialEnergy;
        public System.Windows.Forms.Label prop_rotationalEnergy;
        private System.Windows.Forms.RadioButton radio_rotationalEnergy;
        private System.Windows.Forms.CheckBox newObj_Static;
        private System.Windows.Forms.TextBox newObj_Mass;
        private System.Windows.Forms.Label prop_InitialEnergy;
        private System.Windows.Forms.Label prop_Material;
        private System.Windows.Forms.Label prop_MomInertia;
        private System.Windows.Forms.Label prop_Mass;
        public System.Windows.Forms.GroupBox objProps;
    }
}