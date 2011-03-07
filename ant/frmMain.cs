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

        /// <summary>
        /// Обрабочик алгоритма расчета по методу Муравьиной колонии
        /// </summary>
        private ProcessAnt _pr;
        #endregion

        #region События главной формы

        /// <summary>
        /// Загрузка формы. Запуск алгоритма
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)                           
        {
            tlStrpTxbCitiesCount.Text = "50"; // по умолчанию создаем 50 городов
            tlStrpTxbCitiesCount.Focus();
            tlStrpBtnCreateRandomCities_Click(this, new EventArgs());
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
            if (_pr == null)
            {
                // Интерфейс
                lblProgressInfo.Visible = true;
                toolSTLInfo.Text = DateTime.Now.ToShortTimeString();
                tlStrpTxbCitiesCount.Enabled = false;
                tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                _pr = new ProcessAnt();
                _pr.eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                _pr.eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                _pr.Parameters = _param;
                _pr.Cities = _cities;
                _pr.Start();
            }
        }
        #endregion

        #region События алгоритмов
        /// <summary>
        /// Изменение в Алгоритме муравья
        /// </summary>
        /// <param name="value"></param>
        private void AntAlgProgressChange(int value)                                    
        {
            //prgbarProgress.Value = value;
            //toolSTProgress.Text = value + "%";
            try
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    lblProgressInfo.Text = "Процент вполнения: " + value.ToString() + "%";
                }));
            }
            catch (ObjectDisposedException ex)
            {
            }
            //label1
        }

        /// <summary>
        /// Событие завершение расчета по Алгоритму Муравья
        /// </summary>
        private void AntAlgFinally(object sender, EventArgs e)                          
        {
            this.Invoke(new MethodInvoker(delegate()
                {
                    // РЕЗУЛЬТАТЫ
                    // Лист результатов по времени
                    List<string> listr = _pr.ResultList;
                    foreach (string str in listr)
                    {
                        rtxbOut.AppendText(str);
                    }
                    rtxbOut.AppendText("--------------------------------------------\n");
                    // Лист последовательности городов
                    AntAlgData.AntAlgDataCitiesCollection CitiesInPath = _pr.ResultPath;
                    rtxbCities.Clear();
                    for (int i = 0; i < CitiesInPath.Count; i++)
                    {
                        rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[i].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                    }

                    // Путь городов
                    ucCP.SetCities(CitiesInPath);
                    ucCP.PaintCitiesAndRoute();

                    toolSTLInfo.Text = "Время расчета: " + _pr.ProcessTime.ToString();

                    // Готовность интерфейса
                    lblProgressInfo.Visible = false;
                    _pr = null;
                    tlStrpTxbCitiesCount.Enabled = true;
                    tlStrpBtnAntAlgStart.Enabled = true;
                    tlStrpBtnCreateRandomCities.Enabled = true;
                }));
        }
        #endregion
    }
}
