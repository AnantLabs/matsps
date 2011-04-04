namespace matsps.Forms.SelectAlgs
{
    partial class frmSelectAlgs
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
            this.btnStart = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.CalcStatus = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.AlgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CalcCount = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnStart.BackgroundImage = global::matsps.Properties.Resources.accepted_48;
            this.btnStart.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnStart.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnStart.Location = new System.Drawing.Point(12, 176);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(32, 32);
            this.btnStart.TabIndex = 1;
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.BackgroundImage = global::matsps.Properties.Resources.cancel_48;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(50, 176);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(32, 32);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(353, 158);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Выберите алгоритмы для расчета  :";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CalcStatus,
            this.AlgName,
            this.CalcCount});
            this.dataGridView.Location = new System.Drawing.Point(17, 28);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.Size = new System.Drawing.Size(326, 118);
            this.dataGridView.TabIndex = 0;
            // 
            // CalcStatus
            // 
            this.CalcStatus.FillWeight = 50F;
            this.CalcStatus.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.CalcStatus.Frozen = true;
            this.CalcStatus.HeaderText = "Расчет";
            this.CalcStatus.Name = "CalcStatus";
            this.CalcStatus.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CalcStatus.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
            this.CalcStatus.ToolTipText = "Производить ли расчет данным алгоритмом";
            this.CalcStatus.Width = 50;
            // 
            // AlgName
            // 
            this.AlgName.FillWeight = 120F;
            this.AlgName.Frozen = true;
            this.AlgName.HeaderText = "Алгоритм";
            this.AlgName.Name = "AlgName";
            this.AlgName.ReadOnly = true;
            this.AlgName.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.AlgName.ToolTipText = "Название алгоритма";
            this.AlgName.Width = 150;
            // 
            // CalcCount
            // 
            this.CalcCount.Frozen = true;
            this.CalcCount.HeaderText = "Кол-во расчетов";
            this.CalcCount.Name = "CalcCount";
            this.CalcCount.Resizable = System.Windows.Forms.DataGridViewTriState.False;
            this.CalcCount.ToolTipText = "Сколько раз произвести расчет";
            // 
            // frmSelectAlgs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(377, 211);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnStart);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(393, 249);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(393, 249);
            this.Name = "frmSelectAlgs";
            this.ShowIcon = false;
            this.Text = "Расчет";
            this.Load += new System.EventHandler(this.frmSelectAlgs_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataGridViewCheckBoxColumn CalcStatus;
        private System.Windows.Forms.DataGridViewTextBoxColumn AlgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CalcCount;
    }
}