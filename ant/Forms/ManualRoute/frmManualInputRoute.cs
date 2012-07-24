using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace matsps.Forms.ManualRoute
{
    public partial class frmManualInputRoute : Form
    {
        public frmManualInputRoute()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        /// <summary>
        /// Списко индексов в нужной последовательности
        /// </summary>
        public List<int> CitiesIndexes { set; get; }

        private void txbRoute_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData )
            {
                case Keys.Enter:
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    };break;
                case Keys.Escape:
                    {
                        this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    };break;
            }

        }

        private void frmManualInputRoute_FormClosing(object sender, FormClosingEventArgs e)
        {
            CitiesIndexes = new List<int>();
            if (this.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                string[] source = txbRoute.Text.Split('-');
                foreach (string item in source)
                {
                    CitiesIndexes.Add(int.Parse(item));
                }
            }
        }
    }
}
