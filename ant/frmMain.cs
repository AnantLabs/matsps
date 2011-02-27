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
        #region Конструкторы и Данные
        public frmMain()                
        {
            InitializeComponent();
        }

        // Коллекция городов
        AntAlgData.AntAlgDataCitiesCollection _cities;
        // Параметры расчета
        AntAlgData.AntAlgDataParameters _param;
        #endregion

        #region События главной формы

        /// <summary>
        /// Загрузка формы. Запуск алгоритма
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)
        {
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

        #region События toolStrip
        /// <summary>
        /// Создать коллекцию городов
        /// </summary>
        private void tlStrpBtnCreateRandomCities_Click(object sender, EventArgs e)      
        {
            // ИСХОДНЫЕ ДАННЫЕ
            _param = new ant.AntAlgData.AntAlgDataParameters();
            // Создаем Города
            int iCitiesCount;
            try
            {
                 iCitiesCount = Convert.ToInt32( tlStrpTxbCitiesCount.Text );
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка преобразования из текста в целое число: " + ex.Message);
                return;
            }
            _cities = new ant.AntAlgData.AntAlgDataCitiesCollection( iCitiesCount );
            _param.MaxCities = iCitiesCount;

            //Cities.InitPheromone = param.InitPheromone;
            _cities.MaxDistance = _param.MaxDistance;
            _cities.InitCitiesRandom();
            // Прорисовка городов
            ucCP.SetCities(_cities);
            ucCP.PaintCities();

            rtxbOut.Clear();
            rtxbCities.Clear();
            toolSTLInfo.Text = "Города созданы: " + _cities.Count;
        }
        /// <summary>
        /// Нажатие клавиши Enter в текстовом поле
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tlStrpTxbCitiesCount_KeyUp(object sender, KeyEventArgs e)          
        {
            if (e.KeyCode == Keys.Enter)
                tlStrpBtnCreateRandomCities_Click(this, new EventArgs());
        }

        /// <summary>
        /// Начать расчет методом Муравьиной колонии
        /// </summary>
        private void tlStrpBtnAntAlgStart_Click(object sender, EventArgs e)             
        {
            // АЛГОРИТМ
            toolSTLInfo.Text = "";
            ProcessAnt pr = new ProcessAnt();
            pr.Parameters = _param;
            pr.Cities = _cities;
            pr.Start();



            // РЕЗУЛЬТАТЫ
            // Лист результатов по времени
            List<string> listr = pr.ResultList;
            foreach (string str in listr)
            {
                rtxbOut.AppendText(str);
            }
            rtxbOut.AppendText("--------------------------------------------\n");
            // Лист последовательности городов
            AntAlgData.AntAlgDataCitiesCollection CitiesInPath = pr.ResultPath;
            rtxbCities.Clear();
            for (int i = 0; i < CitiesInPath.Count; i++)
            {
                rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[i].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
            }

            // Путь городов
            ucCP.SetCities(CitiesInPath);
            ucCP.PaintCitiesAndRoute();

            toolSTLInfo.Text = "Время расчета: " + pr.ProcessTime.ToString();

        }
        #endregion
    }
}
