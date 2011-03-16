namespace ant.UserControls
{
    partial class ucCitiesPainter
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

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Обязательный метод для поддержки конструктора - не изменяйте 
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.pbCanvas = new System.Windows.Forms.PictureBox();
            this.splContMain = new System.Windows.Forms.SplitContainer();
            this.chbShowCitiesLabelNumber = new System.Windows.Forms.CheckBox();
            this.chbGrayLines = new System.Windows.Forms.CheckBox();
            this.dgvRouteList = new System.Windows.Forms.DataGridView();
            this.dgvRLcolVisible = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.dgvRLcolNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRLcolAlgorithmName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dgvRLcolLength = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.btnShowHide = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).BeginInit();
            this.splContMain.Panel1.SuspendLayout();
            this.splContMain.Panel2.SuspendLayout();
            this.splContMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRouteList)).BeginInit();
            this.SuspendLayout();
            // 
            // pbCanvas
            // 
            this.pbCanvas.BackColor = System.Drawing.Color.White;
            this.pbCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbCanvas.Location = new System.Drawing.Point(0, 0);
            this.pbCanvas.Name = "pbCanvas";
            this.pbCanvas.Size = new System.Drawing.Size(515, 260);
            this.pbCanvas.TabIndex = 1;
            this.pbCanvas.TabStop = false;
            // 
            // splContMain
            // 
            this.splContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splContMain.Location = new System.Drawing.Point(0, 0);
            this.splContMain.Name = "splContMain";
            this.splContMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splContMain.Panel1
            // 
            this.splContMain.Panel1.Controls.Add(this.pbCanvas);
            // 
            // splContMain.Panel2
            // 
            this.splContMain.Panel2.Controls.Add(this.chbShowCitiesLabelNumber);
            this.splContMain.Panel2.Controls.Add(this.chbGrayLines);
            this.splContMain.Panel2.Controls.Add(this.dgvRouteList);
            this.splContMain.Panel2.Controls.Add(this.btnShowHide);
            this.splContMain.Size = new System.Drawing.Size(515, 355);
            this.splContMain.SplitterDistance = 260;
            this.splContMain.TabIndex = 4;
            this.splContMain.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splContMain_SplitterMoved);
            // 
            // chbShowCitiesLabelNumber
            // 
            this.chbShowCitiesLabelNumber.AutoSize = true;
            this.chbShowCitiesLabelNumber.Location = new System.Drawing.Point(290, 29);
            this.chbShowCitiesLabelNumber.Name = "chbShowCitiesLabelNumber";
            this.chbShowCitiesLabelNumber.Size = new System.Drawing.Size(174, 17);
            this.chbShowCitiesLabelNumber.TabIndex = 2;
            this.chbShowCitiesLabelNumber.Text = "Показывать номера городов";
            this.chbShowCitiesLabelNumber.UseVisualStyleBackColor = true;
            this.chbShowCitiesLabelNumber.CheckedChanged += new System.EventHandler(this.ucCitiesPainter_Resize);
            // 
            // chbGrayLines
            // 
            this.chbGrayLines.AutoSize = true;
            this.chbGrayLines.Location = new System.Drawing.Point(3, 29);
            this.chbGrayLines.Name = "chbGrayLines";
            this.chbGrayLines.Size = new System.Drawing.Size(280, 17);
            this.chbGrayLines.TabIndex = 1;
            this.chbGrayLines.Text = "Показывать серые линии между всеми городами";
            this.chbGrayLines.UseVisualStyleBackColor = true;
            this.chbGrayLines.CheckedChanged += new System.EventHandler(this.ucCitiesPainter_Resize);
            // 
            // dgvRouteList
            // 
            this.dgvRouteList.AllowUserToAddRows = false;
            this.dgvRouteList.AllowUserToDeleteRows = false;
            this.dgvRouteList.AllowUserToResizeRows = false;
            this.dgvRouteList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvRouteList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRouteList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dgvRLcolVisible,
            this.dgvRLcolNumber,
            this.dgvRLcolAlgorithmName,
            this.dgvRLcolLength});
            this.dgvRouteList.Location = new System.Drawing.Point(3, 52);
            this.dgvRouteList.Name = "dgvRouteList";
            this.dgvRouteList.RowHeadersVisible = false;
            this.dgvRouteList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRouteList.Size = new System.Drawing.Size(509, 36);
            this.dgvRouteList.TabIndex = 0;
            this.dgvRouteList.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dgvRouteList_MouseUp);
            // 
            // dgvRLcolVisible
            // 
            this.dgvRLcolVisible.HeaderText = "Видимость";
            this.dgvRLcolVisible.Name = "dgvRLcolVisible";
            this.dgvRLcolVisible.Width = 80;
            // 
            // dgvRLcolNumber
            // 
            this.dgvRLcolNumber.HeaderText = "Номер";
            this.dgvRLcolNumber.Name = "dgvRLcolNumber";
            this.dgvRLcolNumber.ReadOnly = true;
            this.dgvRLcolNumber.Width = 50;
            // 
            // dgvRLcolAlgorithmName
            // 
            this.dgvRLcolAlgorithmName.HeaderText = "Алгоритм";
            this.dgvRLcolAlgorithmName.Name = "dgvRLcolAlgorithmName";
            this.dgvRLcolAlgorithmName.ReadOnly = true;
            this.dgvRLcolAlgorithmName.Width = 200;
            // 
            // dgvRLcolLength
            // 
            this.dgvRLcolLength.HeaderText = "Длина маршрута";
            this.dgvRLcolLength.Name = "dgvRLcolLength";
            this.dgvRLcolLength.ReadOnly = true;
            this.dgvRLcolLength.Width = 130;
            // 
            // btnShowHide
            // 
            this.btnShowHide.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnShowHide.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnShowHide.Location = new System.Drawing.Point(0, 2);
            this.btnShowHide.Name = "btnShowHide";
            this.btnShowHide.Size = new System.Drawing.Size(516, 21);
            this.btnShowHide.TabIndex = 0;
            this.btnShowHide.Text = "скрыть/показать панель свойств";
            this.btnShowHide.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnShowHide.UseVisualStyleBackColor = false;
            this.btnShowHide.Click += new System.EventHandler(this.btnShowHide_Click);
            // 
            // ucCitiesPainter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splContMain);
            this.Name = "ucCitiesPainter";
            this.Size = new System.Drawing.Size(515, 355);
            this.Resize += new System.EventHandler(this.ucCitiesPainter_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.pbCanvas)).EndInit();
            this.splContMain.Panel1.ResumeLayout(false);
            this.splContMain.Panel2.ResumeLayout(false);
            this.splContMain.Panel2.PerformLayout();
            this.splContMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRouteList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbCanvas;
        private System.Windows.Forms.SplitContainer splContMain;
        private System.Windows.Forms.Button btnShowHide;
        private System.Windows.Forms.DataGridView dgvRouteList;
        private System.Windows.Forms.CheckBox chbGrayLines;
        private System.Windows.Forms.CheckBox chbShowCitiesLabelNumber;
        private System.Windows.Forms.DataGridViewCheckBoxColumn dgvRLcolVisible;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRLcolNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRLcolAlgorithmName;
        private System.Windows.Forms.DataGridViewTextBoxColumn dgvRLcolLength;
    }
}
