namespace ant
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
            this.rtxbOut = new System.Windows.Forms.RichTextBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolSTLInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.splContMain = new System.Windows.Forms.SplitContainer();
            this.splContLeft = new System.Windows.Forms.SplitContainer();
            this.rtxbCities = new System.Windows.Forms.RichTextBox();
            this.ucCP = new ant.UserControls.ucCitiesPainter();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.splContMain.Panel1.SuspendLayout();
            this.splContMain.Panel2.SuspendLayout();
            this.splContMain.SuspendLayout();
            this.splContLeft.Panel1.SuspendLayout();
            this.splContLeft.Panel2.SuspendLayout();
            this.splContLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // rtxbOut
            // 
            this.rtxbOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.rtxbOut.Location = new System.Drawing.Point(3, 3);
            this.rtxbOut.Name = "rtxbOut";
            this.rtxbOut.Size = new System.Drawing.Size(166, 174);
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
            this.toolSTLInfo.Size = new System.Drawing.Size(109, 17);
            this.toolSTLInfo.Text = "toolStripStatusLabel1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.MenuBar;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemFile,
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
            this.toolStripMenuItemFile.Size = new System.Drawing.Size(45, 20);
            this.toolStripMenuItemFile.Text = "Файл";
            // 
            // toolStripMenuItemExit
            // 
            this.toolStripMenuItemExit.Name = "toolStripMenuItemExit";
            this.toolStripMenuItemExit.Size = new System.Drawing.Size(107, 22);
            this.toolStripMenuItemExit.Text = "Выход";
            this.toolStripMenuItemExit.Click += new System.EventHandler(this.toolStripMenuItemExit_Click);
            // 
            // toolStripMenuItemHelp
            // 
            this.toolStripMenuItemHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemAbout});
            this.toolStripMenuItemHelp.Name = "toolStripMenuItemHelp";
            this.toolStripMenuItemHelp.Size = new System.Drawing.Size(62, 20);
            this.toolStripMenuItemHelp.Text = "Справка";
            // 
            // toolStripMenuItemAbout
            // 
            this.toolStripMenuItemAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripMenuItemAbout.Name = "toolStripMenuItemAbout";
            this.toolStripMenuItemAbout.Size = new System.Drawing.Size(138, 22);
            this.toolStripMenuItemAbout.Text = "О программе";
            this.toolStripMenuItemAbout.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            this.toolStripMenuItemAbout.Click += new System.EventHandler(this.toolStripMenuItemAbout_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(709, 25);
            this.toolStrip1.TabIndex = 3;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // splContMain
            // 
            this.splContMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splContMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splContMain.Location = new System.Drawing.Point(0, 52);
            this.splContMain.Name = "splContMain";
            // 
            // splContMain.Panel1
            // 
            this.splContMain.Panel1.Controls.Add(this.splContLeft);
            // 
            // splContMain.Panel2
            // 
            this.splContMain.Panel2.Controls.Add(this.ucCP);
            this.splContMain.Size = new System.Drawing.Size(709, 360);
            this.splContMain.SplitterDistance = 172;
            this.splContMain.TabIndex = 4;
            // 
            // splContLeft
            // 
            this.splContLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splContLeft.Location = new System.Drawing.Point(0, 0);
            this.splContLeft.Name = "splContLeft";
            this.splContLeft.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splContLeft.Panel1
            // 
            this.splContLeft.Panel1.Controls.Add(this.rtxbOut);
            // 
            // splContLeft.Panel2
            // 
            this.splContLeft.Panel2.Controls.Add(this.rtxbCities);
            this.splContLeft.Size = new System.Drawing.Size(172, 360);
            this.splContLeft.SplitterDistance = 180;
            this.splContLeft.TabIndex = 1;
            // 
            // rtxbCities
            // 
            this.rtxbCities.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxbCities.Location = new System.Drawing.Point(0, 0);
            this.rtxbCities.Name = "rtxbCities";
            this.rtxbCities.Size = new System.Drawing.Size(172, 176);
            this.rtxbCities.TabIndex = 1;
            this.rtxbCities.Text = "";
            // 
            // ucCP
            // 
            this.ucCP.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucCP.Location = new System.Drawing.Point(0, 0);
            this.ucCP.Name = "ucCP";
            this.ucCP.Size = new System.Drawing.Size(533, 360);
            this.ucCP.TabIndex = 0;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(709, 437);
            this.Controls.Add(this.splContMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(717, 464);
            this.Name = "frmMain";
            this.ShowIcon = false;
            this.Text = "Алгоритм муравья";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splContMain.Panel1.ResumeLayout(false);
            this.splContMain.Panel2.ResumeLayout(false);
            this.splContMain.ResumeLayout(false);
            this.splContLeft.Panel1.ResumeLayout(false);
            this.splContLeft.Panel2.ResumeLayout(false);
            this.splContLeft.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxbOut;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolSTLInfo;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHelp;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAbout;
        private System.Windows.Forms.SplitContainer splContMain;
        private ant.UserControls.ucCitiesPainter ucCP;
        private System.Windows.Forms.SplitContainer splContLeft;
        private System.Windows.Forms.RichTextBox rtxbCities;
    }
}

