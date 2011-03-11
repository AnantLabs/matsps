using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ant.CommonData;

namespace ant
{
    public partial class frmMain : Form
    {
        #region Конструкторы и Данные
        public frmMain()                
        {
            InitializeComponent();

            _paramAnt = new ant.AntAlgData.AntAlgDataParameters();
        }

        // Коллекция городов
        DataCitiesCollection _cities;
        // Параметры расчета
        AntAlgData.AntAlgDataParameters _paramAnt;

        /// <summary>
        /// Обрабочик алгоритма расчета по методу Муравьиной колонии
        /// </summary>
        private ProcessAnt _prAnt;
        #endregion

        #region События главной формы

        /// <summary>
        /// Загрузка формы. Запуск алгоритма
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)                           
        {
            toolSTLProgress.Visible = false;
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
        /// Вызываем форму Настроек
        /// </summary>
        private void toolStripMenuItemParameters_Click(object sender, EventArgs e)
        {
            // Запускаем форму настроек. Если она завершилась нажатием кнопки "ОК", применяем настройки
            Forms.Parameters.frmAlgSettings frmPar = new ant.Forms.Parameters.frmAlgSettings(_paramAnt, _cities.Count);
            DialogResult res = frmPar.ShowDialog( this );

            switch (res)
            {
                case DialogResult.OK:
                    {
                        _paramAnt = frmPar.GetParameters();
                    }
                    break;
            }
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
            ucCP.RouteLengthTextOut(-1);
            // ИСХОДНЫЕ ДАННЫЕ
            //_param = new ant.AntAlgData.AntAlgDataParameters();
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
            _cities = new DataCitiesCollection( iCitiesCount );
            _paramAnt.MaxCities = iCitiesCount;
            _paramAnt.MaxAnts = iCitiesCount;

            //Cities.InitPheromone = param.InitPheromone;
            _cities.MaxDistance = _paramAnt.MaxDistance;
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
            if (_prAnt == null)
            {
                // Интерфейс
                ucCP.RouteLengthTextOut(-1);
                toolSTLProgress.Visible = true;
                ToolStripProgress.Visible = true;
                toolSTLInfo.Text = DateTime.Now.ToLongTimeString(); //.ToString("{0:H:mm:ss zzz}");
                tlStrpTxbCitiesCount.Enabled = false;
                tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                _prAnt = new ProcessAnt();
                _prAnt.eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                _prAnt.eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                _prAnt.Parameters = _paramAnt;
                _prAnt.Cities = _cities;
                _prAnt.Start();
            }
        }
        #endregion

        #region События алгоритмов
        /// <summary>
        /// Изменение в Алгоритме муравья
        /// </summary>
        /// <param name="value"></param>
        private delegate void IncrementCallback(int val);
        private void AntAlgProgressChange(int value)
        {
            //prgbarProgress.Value = value;
            //toolSTProgress.Text = value + "%";
            try
            {
                this.Invoke(new MethodInvoker(delegate()
                {
                    toolSTLProgress.Text = "Процент вполнения: " + value.ToString() + "%";
                }));

                if (statusStrip1.InvokeRequired)
                {
                    var d = new IncrementCallback(AntAlgProgressChange);
                    Invoke(d, value);
                }
                else
                {
                    ToolStripProgress.Value = value;
                }
            }
            catch (ObjectDisposedException ex)
            {
            }
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
                    List<string> listr = _prAnt.ResultList;
                    foreach (string str in listr)
                    {
                        rtxbOut.AppendText(str);
                    }
                    rtxbOut.AppendText("--------------------------------------------\n");
                    // Лист последовательности городов
                    DataCitiesCollection CitiesInPath = _prAnt.ResultPath.Cities;
                    rtxbCities.Clear();
                    for (int i = 0; i < CitiesInPath.Count; i++)
                    {
                        rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[i].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                    }

                    // Путь городов
                    ucCP.SetCities(CitiesInPath);
                    ucCP.PaintCitiesAndRoute();

                    toolSTLInfo.Text = "Время расчета: " + _prAnt.ProcessTime.ToString();
                    
                    //Вывод длинны маршрута
                    ucCP.RouteLengthTextOut(_prAnt.ResultPath.length);
                    //
                   
                    // Готовность интерфейса
                    _prAnt = null;
                    tlStrpTxbCitiesCount.Enabled = true;
                    tlStrpBtnAntAlgStart.Enabled = true;
                    tlStrpBtnCreateRandomCities.Enabled = true;
                    ToolStripProgress.Visible = false;
                    toolSTLProgress.Visible = false;
                }));
        }
        #endregion

    }
}
