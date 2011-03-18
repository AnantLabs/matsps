using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using ant.Forms;
using ant.CommonData;

namespace ant
{
    public partial class frmMain : Form
    {
        #region Конструкторы и Данные
        public frmMain()                    
        {
            InitializeComponent();

            // Начальная инициализация параметров расчета. Используются параметры по умолчанию.
            _paramAnt = new ant.AntAlgData.AntAlgDataParameters();

            // Отправляем ссылку на Лист Маршрутов контроллу прорисовки
            liRoute = new List<Route>();
            ucCP.ListRoute = liRoute;
        }

        /// <summary>
        /// Коллекция городов
        /// </summary>
        private DataCitiesCollection _cities;
        /// <summary>
        /// Параметры расчета
        /// </summary>
        private AntAlgData.AntAlgDataParameters     _paramAnt;

        /// <summary>
        /// Обрабочик алгоритма расчета по методу Муравьиной колонии
        /// </summary>
        private ProcessAnt                  _prAnt;
        /// <summary>
        /// Обрабочик алгоритма расчета по методу Ближайшего соседа
        /// </summary>
        private ProcessNearestNeighbour     _pnn;

        /// <summary>
        /// Лист расчитанных маршрутов. Все расчитанные маршруты за текущую сессию, помещаются сюда.
        /// </summary>
        private List<Route> liRoute;
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
            Forms.Parameters.frmSelectAlgs frmPar = new ant.Forms.Parameters.frmSelectAlgs(_paramAnt, _cities.Count);
            DialogResult res = frmPar.ShowDialog(this);

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
            // ИСХОДНЫЕ ДАННЫЕ
            // Создаем Города
            int iCitiesCount;
            try
            {
                iCitiesCount = Convert.ToInt32(tlStrpTxbCitiesCount.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка преобразования из текста в целое число: " + ex.Message);
                return;
            }
            _cities = new DataCitiesCollection(iCitiesCount);
            liRoute.Clear();
            _paramAnt.MaxCities = iCitiesCount;
            _paramAnt.MaxAnts = iCitiesCount;

            //Cities.InitPheromone = param.InitPheromone;
            _cities.MaxDistance = _paramAnt.MaxDistance;
            _cities.InitCitiesRandom();
            // Прорисовка городов
            ucCP.Cities = _cities;
            //ucCP.PaintCities();
            ucCP.RefreshRoutePaint();

            rtxbOut.Clear();
            rtxbCities.Clear();
            toolSTLInfo.Text = "Города созданы: " + _cities.Count;
        }
        /// <summary>
        /// Нажатие клавиши Enter в текстовом поле
        /// </summary>
        private void tlStrpTxbCitiesCount_KeyUp(object sender, KeyEventArgs e)      
        {
            if (e.KeyCode == Keys.Enter)
                tlStrpBtnCreateRandomCities_Click(this, new EventArgs());
        }

        /// <summary>
        /// Кнопка запуска расчетов
        /// </summary>
        private void tlStrpBtnStart_Click(object sender, EventArgs e)               
        {
            // Запускаем форму выбора алгоритма. Если она завершилась нажатием кнопки "ОК", то запускаем вбранные алгоритмы
            Forms.SelectAlgs.frmSelectAlgs sa = new ant.Forms.SelectAlgs.frmSelectAlgs();
            DialogResult res = sa.ShowDialog();

            switch (res)
            {
                case DialogResult.OK:
                    {
                        List<int> selectList = sa.getSelectList();//список выбранных алгоритмов                        
                        int iCount = selectList.Count; //количество выбранных алгоритмов

                        //Последовательный запуск выбранных алгоритмов

                        if (selectList[0] == 1)
                            AntAlgStart(); //запуск алгоритма муравьиной колонии

                        if (selectList[1] == 1)
                            NearestNeighbourStart(); //запуск алгоритма соседа

                    }
                    break;
            }
        }

        /// <summary>
        /// Начать расчет методом Муравьиной колонии
        /// </summary>
        private void AntAlgStart()                              
        {
            // АЛГОРИТМ
            if (_prAnt == null)
            {
                // Интерфейс
                toolSTLProgress.Visible = true;
                ToolStripProgress.Visible = true;
                toolSTLInfo.Text = DateTime.Now.ToLongTimeString(); //.ToString("{0:H:mm:ss zzz}");
                tlStrpTxbCitiesCount.Enabled = false;
                //tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                _prAnt = new ProcessAnt();
                _prAnt.eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                _prAnt.eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                _prAnt.Parameters = _paramAnt;
                _prAnt.Cities = _cities;
                _prAnt.Start();
            }
        }

        /// <summary>
        /// Начать расчёт методом Ближайшего соседа
        /// </summary>
        /// <param name="sender"></param>
        private void NearestNeighbourStart()                    
        {

            // АЛГОРИТМ
            if (_pnn == null)
            {
                // Интерфейс
                //ucCP.RouteLengthTextOut("");
                toolSTLProgress.Visible = true;
                ToolStripProgress.Visible = true;
                toolSTLInfo.Text = DateTime.Now.ToShortTimeString();
                tlStrpTxbCitiesCount.Enabled = false;
                //tsbNearestNeighbour.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                _pnn = new ProcessNearestNeighbour();
                _pnn.eventProgressChanged += new ProcessNearestNeighbour.ProgressChanged(AntAlgProgressChange);
                _pnn.eventFinally += new EventHandler<EventArgs>(PNNFinally);
                _pnn.Parameters = _paramAnt;
                _pnn.Cities = _cities;
                _pnn.Start();
            }
            //toolSTLInfo.Text = "";

            //ProcessNearestNeighbour pnn = new ProcessNearestNeighbour();
            //pnn.Parameters = _param;
            //pnn.Cities = _cities;
            //pnn.Start();


            //AntAlgData.AntAlgDataCitiesCollection CitiesInPath = pnn.ResultPath;
            //ucCP.SetCities(CitiesInPath);
            //ucCP.PaintCitiesAndRoute();



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

                    // Путь городов. Заносим лист.
                   //_prAnt.ResultPath.AlgorithmName = "Муравей";
                    _prAnt.ResultPath.Drawing.Color = Color.Purple; // цвет маршрута
                    liRoute.Add( _prAnt.ResultPath );
                    ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                    ucCP.RefreshRoutePaint(); // обновляем прорисовку                    

                    toolSTLInfo.Text = "Время расчета: " + _prAnt.ProcessTime.ToString();

                    // Готовность интерфейса
                    _prAnt = null;
                    tlStrpTxbCitiesCount.Enabled = true;
                    //tlStrpBtnAntAlgStart.Enabled = true;
                    tlStrpBtnCreateRandomCities.Enabled = true;
                    ToolStripProgress.Visible = false;
                    toolSTLProgress.Visible = false;
                }));
        }

        private void PNNFinally(object sender, EventArgs e)             
        {
            this.Invoke(new MethodInvoker(delegate()
            {
                try
                {
                    // РЕЗУЛЬТАТЫ
                    // Лист результатов по времени
                    List<string> listr = _pnn.ResultList;
                    foreach (string str in listr)
                    {
                        rtxbOut.AppendText(str);
                    }
                    rtxbOut.AppendText("\n--------------------------------------------\n");
                    // Лист последовательности городов
                    DataCitiesCollection CitiesInPath = _pnn.ResultPath.Cities;
                    rtxbCities.Clear();
                    for (int i = 0; i < CitiesInPath.Count; i++)
                    {
                        rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[i].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                    }

                    // Путь городов
                    //_pnn.ResultPath.AlgorithmName = "Ближайший сосед";
                    _pnn.ResultPath.Drawing.Color = Color.LightSeaGreen;
                    liRoute.Add(_pnn.ResultPath);
                    ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                    ucCP.RefreshRoutePaint(); // обновляем прорисовку

                    toolSTLInfo.Text = "Время расчета: " + _pnn.ProcessTime.ToString();

                    // Готовность интерфейса
                    _pnn = null;
                    tlStrpTxbCitiesCount.Enabled = true;
                    //tlStrpBtnAntAlgStart.Enabled = true;
                    tlStrpBtnCreateRandomCities.Enabled = true;
                    //tsbNearestNeighbour.Enabled = true;
                    //ToolStripProgress.Visible = false;
                    //toolSTLProgress.Visible = false;
                }
                catch (Exception ex)
                { MessageBox.Show(ex.Message + ex.StackTrace); }
            }));

        }
        #endregion
    }
}
