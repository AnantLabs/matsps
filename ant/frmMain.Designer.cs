namespace matsps
{
    partial class frmMain
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.rtxbOut = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolSTLInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemParameters = new System.Windows.Forms.ToolStripMenuItem();
            this.тестToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.splContMain = new System.Windows.Forms.SplitContainer();
            this.tbCtrlInfo = new System.Windows.Forms.TabControl();
            this.tbPgTimePath = new System.Windows.Forms.TabPage();
            this.tbPgCitiesRoute = new System.Windows.Forms.TabPage();
            this.rtxbCities = new System.Windows.Forms.RichTextBox();
            this.ucCP = new matsps.UserControls.ucCitiesPainter();
            this.tlStrpMainContainer = new System.Windows.Forms.ToolStripContainer();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.tlStrpBtnLoadCities = new System.Windows.Forms.ToolStripButton();
            this.tlStrpBtnSaveCitiesCities = new System.Windows.Forms.ToolStripSplitButton();
            this.tlStrpBtnSaveModeCoords = new System.Windows.Forms.ToolStripMenuItem();
            this.tlStrpBtnSaveModeDistance = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tlStrpTxbCitiesCount = new System.Windows.Forms.ToolStripTextBox();
            this.tlStrpBtnCreateRandomCities = new System.Windows.Forms.ToolStripButton();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.tlStrpBtnSettings = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tlStrpBtnStart = new System.Windows.Forms.ToolStripButton();
            this.tlStripBtnReset = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tlStripBtnManualRoute = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.splContMain.Panel1.SuspendLayout();
            this.splContMain.Panel2.SuspendLayout();
            this.splContMain.SuspendLayout();
            this.tbCtrlInfo.SuspendLayout();
            this.tbPgTimePath.SuspendLayout();
            this.tbPgCitiesRoute.SuspendLayout();
            this.tlStrpMainContainer.ContentPanel.SuspendLayout();
            this.tlStrpMainContainer.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxbOut
            // 
            this.rtxbOut.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbOut.Location = new System.Drawing.Point(3, 3);
            this.rtxbOut.Name = "rtxbOut";
            this.rtxbOut.Size = new System.Drawing.Size(160, 334);
            this.rtxbOut.TabIndex = 0;
            this.rtxbOut.Text = "";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolSTLInfo});
            this.statusStrip1.Location = new System.Drawing.Point(0, 415);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(709, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolSTLInfo
            // 
            this.toolSTLInfo.Name = "toolSTLInfo";
            this.toolSTLInfo.Size = new System.Drawing.Size(28, 17);
            this.toolSTLInfo.Text = "Info";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
            this.toolStripMenuItemSettings,
            this.тестToolStripMenuItem,
            this.toolStripMenuItemHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(709, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItemFile
            // 
            this.toolStripMenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemExit});
            this.toolStripMenuItemFile.Name = "toolStripMenuItemFile";
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(48, 20);
            this.toolStripMenuItemFile.Text = "Файл";
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(108, 22);
            this.toolStripMenuItemExit.Text = "Выход";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // toolStripMenuItemSettings
            // 
            this.toolStripMenuItemSettings.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemParameters});
            this.toolStripMenuItemSettings.Name = "toolStripMenuItemSettings";
            this.toolStripMenuItemSettings.Size = new System.Drawing.Size(79, 20);
            this.toolStripMenuItemSettings.Text = "Настройки";
            // 
            // toolStripMenuItemParameters
            // 
            this.toolStripMenuItemParameters.Name = "toolStripMenuItemParameters";
            this.toolStripMenuItemParameters.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItemParameters.Text = "Параметры";
            this.toolStripMenuItemParameters.ToolTipText = "Параметры алгоритмов";
            this.toolStripMenuItemParameters.Click += new System.EventHandler(this.toolStripMenuItemParameters_Click);
            // 
            // тестToolStripMenuItem
            // 
            this.тестToolStripMenuItem.Name = "тестToolStripMenuItem";
            this.тестToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.тестToolStripMenuItem.Text = "Тест";
            this.тестToolStripMenuItem.Click += new System.EventHandler(this.тестToolStripMenuItem_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(65, 20);
            this.toolStripMenuItemHelp.Text = "Справка";
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(149, 22);
            this.toolStripMenuItemAbout.Text = "О программе";
            this.toolStripMenuItemAbout.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // splContMain
            // 
            this.splContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splContMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splContMain.Location = new System.Drawing.Point(0, 0);
            this.splContMain.Name = "splContMain";
            // 
            // splContMain.Panel1
            // 
            this.splContMain.Panel1.Controls.Add(this.tbCtrlInfo);
            // 
            // splContMain.Panel2
            // 
            this.splContMain.Panel2.Controls.Add(this.ucCP);
            this.splContMain.Size = new System.Drawing.Size(709, 366);
            this.splContMain.SplitterDistance = 174;
            this.splContMain.TabIndex = 4;
            // 
            // tbCtrlInfo
            // 
            this.tbCtrlInfo.Controls.Add(this.tbPgTimePath);
            this.tbCtrlInfo.Controls.Add(this.tbPgCitiesRoute);
            this.tbCtrlInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbCtrlInfo.Location = new System.Drawing.Point(0, 0);
            this.tbCtrlInfo.Name = "tbCtrlInfo";
            this.tbCtrlInfo.SelectedIndex = 0;
            this.tbCtrlInfo.Size = new System.Drawing.Size(174, 366);
            this.tbCtrlInfo.TabIndex = 0;
            this.tbCtrlInfo.Visible = false;
            // 
            // tbPgTimePath
            // 
            this.tbPgTimePath.Controls.Add(this.rtxbOut);
            this.tbPgTimePath.Location = new System.Drawing.Point(4, 22);
            this.tbPgTimePath.Name = "tbPgTimePath";
            this.tbPgTimePath.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgTimePath.Size = new System.Drawing.Size(166, 340);
            this.tbPgTimePath.TabIndex = 0;
            this.tbPgTimePath.Text = "Данные расчета";
            this.tbPgTimePath.UseVisualStyleBackColor = true;
            // 
            // tbPgCitiesRoute
            // 
            this.tbPgCitiesRoute.Controls.Add(this.rtxbCities);
            this.tbPgCitiesRoute.Location = new System.Drawing.Point(4, 22);
            this.tbPgCitiesRoute.Name = "tbPgCitiesRoute";
            this.tbPgCitiesRoute.Padding = new System.Windows.Forms.Padding(3);
            this.tbPgCitiesRoute.Size = new System.Drawing.Size(166, 340);
            this.tbPgCitiesRoute.TabIndex = 1;
            this.tbPgCitiesRoute.Text = "Путь";
            this.tbPgCitiesRoute.UseVisualStyleBackColor = true;
            // 
            // rtxbCities
            // 
            this.rtxbCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbCities.Location = new System.Drawing.Point(3, 3);
            this.rtxbCities.Name = "rtxbCities";
            this.rtxbCities.Size = new System.Drawing.Size(160, 334);
            this.rtxbCities.TabIndex = 1;
            this.rtxbCities.Text = "";
            // 
            // ucCP
            // 
            this.ucCP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCP.Location = new System.Drawing.Point(0, 0);
            this.ucCP.Name = "ucCP";
            this.ucCP.Size = new System.Drawing.Size(531, 366);
            this.ucCP.TabIndex = 0;
            // 
            // tlStrpMainContainer
            // 
            // 
            // tlStrpMainContainer.BottomToolStripPanel
            // 
            this.tlStrpMainContainer.BottomToolStripPanel.Enabled = false;
            this.tlStrpMainContainer.BottomToolStripPanelVisible = false;
            // 
            // tlStrpMainContainer.ContentPanel
            // 
            this.tlStrpMainContainer.ContentPanel.Controls.Add(this.splContMain);
            this.tlStrpMainContainer.ContentPanel.Size = new System.Drawing.Size(709, 366);
            this.tlStrpMainContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // tlStrpMainContainer.LeftToolStripPanel
            // 
            this.tlStrpMainContainer.LeftToolStripPanel.Enabled = false;
            this.tlStrpMainContainer.LeftToolStripPanelVisible = false;
            this.tlStrpMainContainer.Location = new System.Drawing.Point(0, 24);
            this.tlStrpMainContainer.Name = "tlStrpMainContainer";
            // 
            // tlStrpMainContainer.RightToolStripPanel
            // 
            this.tlStrpMainContainer.RightToolStripPanel.Enabled = false;
            this.tlStrpMainContainer.RightToolStripPanelVisible = false;
            this.tlStrpMainContainer.Size = new System.Drawing.Size(709, 391);
            this.tlStrpMainContainer.TabIndex = 5;
            this.tlStrpMainContainer.Text = "toolStripContainer1";
            // 
            // tlStrpMainContainer.TopToolStripPanel
            // 
            this.tlStrpMainContainer.TopToolStripPanel.MaximumSize = new System.Drawing.Size(180, 25);
            this.tlStrpMainContainer.TopToolStripPanel.MinimumSize = new System.Drawing.Size(180, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpBtnLoadCities,
            this.tlStrpBtnSaveCitiesCities,
            this.toolStripSeparator1,
            this.tlStrpTxbCitiesCount,
            this.tlStrpBtnCreateRandomCities});
            this.toolStrip1.Location = new System.Drawing.Point(0, 21);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Padding = new System.Windows.Forms.Padding(0);
            this.toolStrip1.Size = new System.Drawing.Size(194, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // tlStrpBtnLoadCities
            // 
            this.tlStrpBtnLoadCities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStrpBtnLoadCities.Image = global::matsps.Properties.Resources.folder_48;
            this.tlStrpBtnLoadCities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnLoadCities.Name = "tlStrpBtnLoadCities";
            this.tlStrpBtnLoadCities.Size = new System.Drawing.Size(23, 22);
            this.tlStrpBtnLoadCities.Text = "toolStripButton1";
            this.tlStrpBtnLoadCities.ToolTipText = "Загрузить города из файла";
            this.tlStrpBtnLoadCities.Click += new System.EventHandler(this.tlStrpBtnLoadCities_Click);
            // 
            // tlStrpBtnSaveCitiesCities
            // 
            this.tlStrpBtnSaveCitiesCities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStrpBtnSaveCitiesCities.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpBtnSaveModeCoords,
            this.tlStrpBtnSaveModeDistance});
            this.tlStrpBtnSaveCitiesCities.Image = global::matsps.Properties.Resources.floppy_disk_48;
            this.tlStrpBtnSaveCitiesCities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnSaveCitiesCities.Name = "tlStrpBtnSaveCitiesCities";
            this.tlStrpBtnSaveCitiesCities.Size = new System.Drawing.Size(32, 22);
            this.tlStrpBtnSaveCitiesCities.Text = "Сохранить коллекцию городов в файл";
            this.tlStrpBtnSaveCitiesCities.ButtonClick += new System.EventHandler(this.tlStrpBtnSaveCitiesCities_ButtonClick);
            // 
            // tlStrpBtnSaveModeCoords
            // 
            this.tlStrpBtnSaveModeCoords.Checked = true;
            this.tlStrpBtnSaveModeCoords.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tlStrpBtnSaveModeCoords.Name = "tlStrpBtnSaveModeCoords";
            this.tlStrpBtnSaveModeCoords.Size = new System.Drawing.Size(142, 22);
            this.tlStrpBtnSaveModeCoords.Text = "Координаты";
            this.tlStrpBtnSaveModeCoords.Click += new System.EventHandler(this.tlStrpBtnSaveModeCoords_Click);
            // 
            // tlStrpBtnSaveModeDistance
            // 
            this.tlStrpBtnSaveModeDistance.Name = "tlStrpBtnSaveModeDistance";
            this.tlStrpBtnSaveModeDistance.Size = new System.Drawing.Size(142, 22);
            this.tlStrpBtnSaveModeDistance.Text = "Расстояния";
            this.tlStrpBtnSaveModeDistance.Click += new System.EventHandler(this.tlStrpBtnSaveModeDistance_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // tlStrpTxbCitiesCount
            // 
            this.tlStrpTxbCitiesCount.BackColor = System.Drawing.Color.Gainsboro;
            this.tlStrpTxbCitiesCount.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tlStrpTxbCitiesCount.ForeColor = System.Drawing.SystemColors.WindowText;
            this.tlStrpTxbCitiesCount.Name = "tlStrpTxbCitiesCount";
            this.tlStrpTxbCitiesCount.Size = new System.Drawing.Size(25, 25);
            this.tlStrpTxbCitiesCount.ToolTipText = "Количество городов";
            this.tlStrpTxbCitiesCount.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tlStrpTxbCitiesCount_KeyDown);
            this.tlStrpTxbCitiesCount.KeyUp += new System.Windows.Forms.KeyEventHandler(this.tlStrpTxbCitiesCount_KeyUp);
            // 
            // tlStrpBtnCreateRandomCities
            // 
            this.tlStrpBtnCreateRandomCities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrpBtnCreateRandomCities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnCreateRandomCities.Name = "tlStrpBtnCreateRandomCities";
            this.tlStrpBtnCreateRandomCities.Size = new System.Drawing.Size(95, 22);
            this.tlStrpBtnCreateRandomCities.Text = "Создать города";
            this.tlStrpBtnCreateRandomCities.ToolTipText = "Создать заданное количество городов со случаными координатами";
            this.tlStrpBtnCreateRandomCities.Click += new System.EventHandler(this.tlStrpBtnCreateRandomCities_Click);
            // 
            // toolStrip2
            // 
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.None;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tlStrpBtnSettings,
            this.toolStripSeparator2,
            this.tlStrpBtnStart,
            this.tlStripBtnReset,
            this.toolStripSeparator3,
            this.tlStripBtnManualRoute});
            this.toolStrip2.Location = new System.Drawing.Point(181, 21);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(221, 25);
            this.toolStrip2.TabIndex = 4;
            // 
            // tlStrpBtnSettings
            // 
            this.tlStrpBtnSettings.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tlStrpBtnSettings.Image = global::matsps.Properties.Resources.spanner_48;
            this.tlStrpBtnSettings.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnSettings.Name = "tlStrpBtnSettings";
            this.tlStrpBtnSettings.Size = new System.Drawing.Size(23, 22);
            this.tlStrpBtnSettings.Text = "toolStripButton1";
            this.tlStrpBtnSettings.ToolTipText = "Параметры алгоритмов";
            this.tlStrpBtnSettings.Click += new System.EventHandler(this.toolStripMenuItemParameters_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // tlStrpBtnStart
            // 
            this.tlStrpBtnStart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStrpBtnStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStrpBtnStart.Name = "tlStrpBtnStart";
            this.tlStrpBtnStart.Size = new System.Drawing.Size(48, 22);
            this.tlStrpBtnStart.Text = "Расчет";
            this.tlStrpBtnStart.ToolTipText = "Выбор алгоритмов для расчета";
            this.tlStrpBtnStart.Click += new System.EventHandler(this.tlStrpBtnStart_Click);
            // 
            // tlStripBtnReset
            // 
            this.tlStripBtnReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStripBtnReset.Image = ((System.Drawing.Image)(resources.GetObject("tlStripBtnReset.Image")));
            this.tlStripBtnReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStripBtnReset.Name = "tlStripBtnReset";
            this.tlStripBtnReset.Size = new System.Drawing.Size(46, 22);
            this.tlStripBtnReset.Text = "Сброс";
            this.tlStripBtnReset.ToolTipText = "Сброс текущего расчета";
            this.tlStripBtnReset.Click += new System.EventHandler(this.tlStripBtnReset_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // tlStripBtnManualRoute
            // 
            this.tlStripBtnManualRoute.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tlStripBtnManualRoute.Image = ((System.Drawing.Image)(resources.GetObject("tlStripBtnManualRoute.Image")));
            this.tlStripBtnManualRoute.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tlStripBtnManualRoute.Name = "tlStripBtnManualRoute";
            this.tlStripBtnManualRoute.Size = new System.Drawing.Size(80, 22);
            this.tlStripBtnManualRoute.Text = "Ручной ввод";
            this.tlStripBtnManualRoute.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.tlStripBtnManualRoute.Click += new System.EventHandler(this.tlStripBtnManualRoute_Click);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 437);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.tlStrpMainContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(717, 464);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Задача коммивояжера";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splContMain.Panel1.ResumeLayout(false);
            this.splContMain.Panel2.ResumeLayout(false);
            this.splContMain.ResumeLayout(false);
            this.tbCtrlInfo.ResumeLayout(false);
            this.tbPgTimePath.ResumeLayout(false);
            this.tbPgCitiesRoute.ResumeLayout(false);
            this.tlStrpMainContainer.ContentPanel.ResumeLayout(false);
            this.tlStrpMainContainer.ResumeLayout(false);
            this.tlStrpMainContainer.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxbOut;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolSTLInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.SplitContainer splContMain;
        private matsps.UserControls.ucCitiesPainter ucCP;
        private System.Windows.Forms.RichTextBox rtxbCities;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemSettings;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemParameters;
        private System.Windows.Forms.TabControl tbCtrlInfo;
        private System.Windows.Forms.TabPage tbPgTimePath;
        private System.Windows.Forms.TabPage tbPgCitiesRoute;
        private System.Windows.Forms.ToolStripContainer tlStrpMainContainer;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox tlStrpTxbCitiesCount;
        private System.Windows.Forms.ToolStripButton tlStrpBtnCreateRandomCities;
        private System.Windows.Forms.ToolStripButton tlStrpBtnLoadCities;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton tlStrpBtnStart;
        private System.Windows.Forms.ToolStripButton tlStrpBtnSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSplitButton tlStrpBtnSaveCitiesCities;
        private System.Windows.Forms.ToolStripMenuItem tlStrpBtnSaveModeCoords;
        private System.Windows.Forms.ToolStripMenuItem tlStrpBtnSaveModeDistance;
        private System.Windows.Forms.ToolStripButton tlStripBtnReset;
        private System.Windows.Forms.ToolStripMenuItem тестToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton tlStripBtnManualRoute;
    }
}

