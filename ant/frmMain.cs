using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ant
{
    public partial class frmMain : Form
    {
        public frmMain()                
        {
            InitializeComponent();
        }

        #region События главной формы

        /// <summary>
        /// Загрузка формы. Запуск алгоритма
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
            toolSTLInfo.Text = "";
            Process pr = new Process();
            pr.Start();

            // Лист результатов по времени
            List<string> listr = pr.ResultList;
            foreach(string str in listr)
            {
                rtxbOut.AppendText(str);
            }
            // Лист последовательности городов
            AntAlgData.AntAlgDataCitiesCollection CitiesInPath = pr.ResultPath;
            for (int i = 0; i < CitiesInPath.Count; i++)
            {
                rtxbCities.AppendText(CitiesInPath[i].Index + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
            }

            // Путь городов
            ucCP.SetCities(CitiesInPath);
            ucCP.PaintCities();

            toolSTLInfo.Text = "Время расчета: " + pr.ProcessTime.ToString();
        }
        #endregion

        #region События из меню
        /// <summary>
        /// Выход из программы
        /// </summary>
        private void toolStripMenuItemExit_Click(object sender, EventArgs e)    
        {
            this.Close();
        }

        /// <summary>
        /// Вызывает окно О программе
        /// </summary>
        private void toolStripMenuItemAbout_Click(object sender, EventArgs e)   
        {
            Forms.About.frmAbout ab = new ant.Forms.About.frmAbout();
            ab.ShowDialog(this);
        }
        #endregion 
    }
}
