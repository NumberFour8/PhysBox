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
            this.menu_openToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_restartSim = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_Quit = new System.Windows.Forms.ToolStripMenuItem();
            this.analýzaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.konfiguraceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_enableWireframe = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_showVertices = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_drawInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.menu_showVersion = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.menu_pauseSim = new System.Windows.Forms.ToolStripMenuItem();
            this.nastavitNulovouHladinuToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stavovyRadek = new System.Windows.Forms.StatusStrip();
            this.status_SimStat = new System.Windows.Forms.ToolStripStatusLabel();
            this.stat_SelObject = new System.Windows.Forms.ToolStripStatusLabel();
            this.manipulateObj = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manipulateObj_Name = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.manipulateObj_Enabled = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.manipulateObj_Translate = new System.Windows.Forms.ToolStripMenuItem();
            this.manipulateObj_Rotate = new System.Windows.Forms.ToolStripMenuItem();
            this.manipulateObj_Scale = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.manipulateObj_SetAxis = new System.Windows.Forms.ToolStripMenuItem();
            this.manipulateObj_ApplyForce = new System.Windows.Forms.ToolStripMenuItem();
            this.manipulateObj_CancelForces = new System.Windows.Forms.ToolStripMenuItem();
            this.manipulateObj_NoTranslations = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.analyzovatToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.simTime = new System.Windows.Forms.Timer(this.components);
            this.menu_showBounds = new System.Windows.Forms.ToolStripMenuItem();
            this.mainMenu.SuspendLayout();
            this.stavovyRadek.SuspendLayout();
            this.manipulateObj.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainMenu
            // 
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ukončitToolStripMenuItem,
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
            this.menu_openToolbar,
            this.toolStripMenuItem3,
            this.menu_restartSim,
            this.menu_Quit});
            this.ukončitToolStripMenuItem.Name = "ukončitToolStripMenuItem";
            this.ukončitToolStripMenuItem.Size = new System.Drawing.Size(65, 20);
            this.ukončitToolStripMenuItem.Text = "Program";
            // 
            // menu_openToolbar
            // 
            this.menu_openToolbar.Name = "menu_openToolbar";
            this.menu_openToolbar.ShortcutKeyDisplayString = "";
            this.menu_openToolbar.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.menu_openToolbar.Size = new System.Drawing.Size(137, 22);
            this.menu_openToolbar.Text = "Nástroje";
            this.menu_openToolbar.Click += new System.EventHandler(this.nástrojeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(134, 6);
            // 
            // menu_restartSim
            // 
            this.menu_restartSim.Name = "menu_restartSim";
            this.menu_restartSim.Size = new System.Drawing.Size(137, 22);
            this.menu_restartSim.Text = "Restart";
            // 
            // menu_Quit
            // 
            this.menu_Quit.Name = "menu_Quit";
            this.menu_Quit.Size = new System.Drawing.Size(137, 22);
            this.menu_Quit.Text = "Ukončit";
            // 
            // analýzaToolStripMenuItem
            // 
            this.analýzaToolStripMenuItem.Name = "analýzaToolStripMenuItem";
            this.analýzaToolStripMenuItem.Size = new System.Drawing.Size(60, 20);
            this.analýzaToolStripMenuItem.Text = "Analýza";
            // 
            // konfiguraceToolStripMenuItem
            // 
            this.konfiguraceToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menu_enableWireframe,
            this.menu_showVertices,
            this.menu_drawInfo,
            this.menu_showBounds,
            this.menu_showVersion,
            this.toolStripMenuItem4,
            this.menu_pauseSim,
            this.nastavitNulovouHladinuToolStripMenuItem});
            this.konfiguraceToolStripMenuItem.Name = "konfiguraceToolStripMenuItem";
            this.konfiguraceToolStripMenuItem.Size = new System.Drawing.Size(83, 20);
            this.konfiguraceToolStripMenuItem.Text = "Konfigurace";
            // 
            // menu_enableWireframe
            // 
            this.menu_enableWireframe.Checked = true;
            this.menu_enableWireframe.CheckOnClick = true;
            this.menu_enableWireframe.CheckState = System.Windows.Forms.CheckState.Checked;
            this.menu_enableWireframe.Name = "menu_enableWireframe";
            this.menu_enableWireframe.Size = new System.Drawing.Size(232, 22);
            this.menu_enableWireframe.Text = "Drátový model";
            this.menu_enableWireframe.Click += new System.EventHandler(this.drátovýModelToolStripMenuItem_Click);
            // 
            // menu_showVertices
            // 
            this.menu_showVertices.CheckOnClick = true;
            this.menu_showVertices.Name = "menu_showVertices";
            this.menu_showVertices.Size = new System.Drawing.Size(232, 22);
            this.menu_showVertices.Text = "Zobrazit vertexy";
            // 
            // menu_drawInfo
            // 
            this.menu_drawInfo.CheckOnClick = true;
            this.menu_drawInfo.Name = "menu_drawInfo";
            this.menu_drawInfo.Size = new System.Drawing.Size(232, 22);
            this.menu_drawInfo.Text = "Zobrazit informace modelu";
            // 
            // menu_showVersion
            // 
            this.menu_showVersion.CheckOnClick = true;
            this.menu_showVersion.Name = "menu_showVersion";
            this.menu_showVersion.Size = new System.Drawing.Size(232, 22);
            this.menu_showVersion.Text = "Zobrazit verzi";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(229, 6);
            // 
            // menu_pauseSim
            // 
            this.menu_pauseSim.CheckOnClick = true;
            this.menu_pauseSim.Name = "menu_pauseSim";
            this.menu_pauseSim.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.menu_pauseSim.Size = new System.Drawing.Size(232, 22);
            this.menu_pauseSim.Text = "Pozastavit simulaci";
            this.menu_pauseSim.CheckedChanged += new System.EventHandler(this.pozastavitSimulaciToolStripMenuItem_CheckedChanged);
            // 
            // nastavitNulovouHladinuToolStripMenuItem
            // 
            this.nastavitNulovouHladinuToolStripMenuItem.Name = "nastavitNulovouHladinuToolStripMenuItem";
            this.nastavitNulovouHladinuToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.nastavitNulovouHladinuToolStripMenuItem.Size = new System.Drawing.Size(232, 22);
            this.nastavitNulovouHladinuToolStripMenuItem.Text = "Nastavit nulovou hladinu";
            this.nastavitNulovouHladinuToolStripMenuItem.Click += new System.EventHandler(this.nastavitNulovouHladinuToolStripMenuItem_Click);
            // 
            // stavovyRadek
            // 
            this.stavovyRadek.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.status_SimStat,
            this.stat_SelObject});
            this.stavovyRadek.Location = new System.Drawing.Point(0, 441);
            this.stavovyRadek.Name = "stavovyRadek";
            this.stavovyRadek.Size = new System.Drawing.Size(1019, 22);
            this.stavovyRadek.TabIndex = 3;
            this.stavovyRadek.Text = "statusStrip";
            // 
            // status_SimStat
            // 
            this.status_SimStat.Name = "status_SimStat";
            this.status_SimStat.Size = new System.Drawing.Size(79, 17);
            this.status_SimStat.Text = "Simulace běží";
            // 
            // stat_SelObject
            // 
            this.stat_SelObject.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.stat_SelObject.Name = "stat_SelObject";
            this.stat_SelObject.Size = new System.Drawing.Size(126, 17);
            this.stat_SelObject.Text = "Žádný vybraný objekt";
            // 
            // manipulateObj
            // 
            this.manipulateObj.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manipulateObj_Name,
            this.toolStripMenuItem1,
            this.manipulateObj_Enabled,
            this.toolStripMenuItem5,
            this.manipulateObj_Translate,
            this.manipulateObj_Rotate,
            this.manipulateObj_Scale,
            this.toolStripMenuItem6,
            this.manipulateObj_SetAxis,
            this.manipulateObj_ApplyForce,
            this.manipulateObj_CancelForces,
            this.manipulateObj_NoTranslations,
            this.toolStripMenuItem2,
            this.analyzovatToolStripMenuItem});
            this.manipulateObj.Name = "manipulateObj";
            this.manipulateObj.Size = new System.Drawing.Size(171, 248);
            // 
            // manipulateObj_Name
            // 
            this.manipulateObj_Name.Enabled = false;
            this.manipulateObj_Name.Font = new System.Drawing.Font("Segoe UI", 9F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))));
            this.manipulateObj_Name.Name = "manipulateObj_Name";
            this.manipulateObj_Name.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_Name.Text = "name";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(167, 6);
            // 
            // manipulateObj_Enabled
            // 
            this.manipulateObj_Enabled.CheckOnClick = true;
            this.manipulateObj_Enabled.Name = "manipulateObj_Enabled";
            this.manipulateObj_Enabled.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_Enabled.Text = "Simulovat";
            this.manipulateObj_Enabled.Click += new System.EventHandler(this.manipulateObj_Enabled_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(167, 6);
            // 
            // manipulateObj_Translate
            // 
            this.manipulateObj_Translate.Name = "manipulateObj_Translate";
            this.manipulateObj_Translate.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_Translate.Text = "Přemístit";
            this.manipulateObj_Translate.Click += new System.EventHandler(this.přemístitSemToolStripMenuItem_Click);
            // 
            // manipulateObj_Rotate
            // 
            this.manipulateObj_Rotate.Name = "manipulateObj_Rotate";
            this.manipulateObj_Rotate.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_Rotate.Text = "Otáčet";
            this.manipulateObj_Rotate.Click += new System.EventHandler(this.otáčetToolStripMenuItem_Click);
            // 
            // manipulateObj_Scale
            // 
            this.manipulateObj_Scale.Name = "manipulateObj_Scale";
            this.manipulateObj_Scale.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_Scale.Text = "Škálovat";
            this.manipulateObj_Scale.Click += new System.EventHandler(this.manipulateObj_Scale_Click);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(167, 6);
            // 
            // manipulateObj_SetAxis
            // 
            this.manipulateObj_SetAxis.Enabled = false;
            this.manipulateObj_SetAxis.Name = "manipulateObj_SetAxis";
            this.manipulateObj_SetAxis.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_SetAxis.Text = "Zvolit osu otáčení";
            this.manipulateObj_SetAxis.Click += new System.EventHandler(this.zvolitOsuOtáčeníToolStripMenuItem_Click);
            // 
            // manipulateObj_ApplyForce
            // 
            this.manipulateObj_ApplyForce.Name = "manipulateObj_ApplyForce";
            this.manipulateObj_ApplyForce.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_ApplyForce.Text = "Působit silou...";
            this.manipulateObj_ApplyForce.Click += new System.EventHandler(this.působitSilouToolStripMenuItem_Click);
            // 
            // manipulateObj_CancelForces
            // 
            this.manipulateObj_CancelForces.Name = "manipulateObj_CancelForces";
            this.manipulateObj_CancelForces.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_CancelForces.Text = "Zrušit všechny síly";
            this.manipulateObj_CancelForces.Click += new System.EventHandler(this.manipulateObj_CancelForces_Click);
            // 
            // manipulateObj_NoTranslations
            // 
            this.manipulateObj_NoTranslations.CheckOnClick = true;
            this.manipulateObj_NoTranslations.Name = "manipulateObj_NoTranslations";
            this.manipulateObj_NoTranslations.Size = new System.Drawing.Size(170, 22);
            this.manipulateObj_NoTranslations.Text = "Vypnout translace";
            this.manipulateObj_NoTranslations.Click += new System.EventHandler(this.manipulateObj_NoTranslations_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(167, 6);
            // 
            // analyzovatToolStripMenuItem
            // 
            this.analyzovatToolStripMenuItem.Name = "analyzovatToolStripMenuItem";
            this.analyzovatToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.analyzovatToolStripMenuItem.Text = "Analyzovat...";
            // 
            // simTime
            // 
            this.simTime.Interval = 1;
            this.simTime.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // menu_showBounds
            // 
            this.menu_showBounds.CheckOnClick = true;
            this.menu_showBounds.Name = "menu_showBounds";
            this.menu_showBounds.Size = new System.Drawing.Size(232, 22);
            this.menu_showBounds.Text = "Zobrazit hranice modelů";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(1019, 463);
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
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyUp);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseClick);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseUp);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.stavovyRadek.ResumeLayout(false);
            this.stavovyRadek.PerformLayout();
            this.manipulateObj.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStripMenuItem ukončitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip stavovyRadek;
        private System.Windows.Forms.ToolStripMenuItem menu_restartSim;
        private System.Windows.Forms.ToolStripMenuItem menu_Quit;
        private System.Windows.Forms.ToolStripMenuItem analýzaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem konfiguraceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_enableWireframe;
        private System.Windows.Forms.ToolStripMenuItem menu_showVersion;
        private System.Windows.Forms.Timer simTime;
        private System.Windows.Forms.ContextMenuStrip manipulateObj;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Name;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Enabled;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_CancelForces;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem analyzovatToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel status_SimStat;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_ApplyForce;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Translate;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Rotate;
        private System.Windows.Forms.ToolStripMenuItem menu_pauseSim;
        private System.Windows.Forms.ToolStripMenuItem menu_openToolbar;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripStatusLabel stat_SelObject;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_SetAxis;
        private System.Windows.Forms.ToolStripMenuItem menu_showVertices;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_Scale;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        private System.Windows.Forms.ToolStripMenuItem manipulateObj_NoTranslations;
        private System.Windows.Forms.ToolStripMenuItem menu_drawInfo;
        private System.Windows.Forms.ToolStripMenuItem nastavitNulovouHladinuToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menu_showBounds;
    }
}

