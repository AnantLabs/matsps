namespace matsps.Forms.ManualRoute
{
    partial class frmManualInputRoute
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
            this.txbRoute = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txbRoute
            // 
            this.txbRoute.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txbRoute.Location = new System.Drawing.Point(3, 3);
            this.txbRoute.Name = "txbRoute";
            this.txbRoute.Size = new System.Drawing.Size(613, 20);
            this.txbRoute.TabIndex = 0;
            this.txbRoute.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txbRoute_KeyDown);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(622, 1);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(102, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "Подтвердить";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // frmManualInputRoute
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(736, 25);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txbRoute);
            this.MaximumSize = new System.Drawing.Size(752, 63);
            this.MinimumSize = new System.Drawing.Size(252, 63);
            this.Name = "frmManualInputRoute";
            this.Text = "Введите номера городов в нужной последовательности через знак \"-\"";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmManualInputRoute_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txbRoute;
        private System.Windows.Forms.Button btnOk;
    }
}