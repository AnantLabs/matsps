using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;

using matsps.Forms;
using matsps.CommonData;
using matsps.BranchAndBound.BnBAlgLogic;       // пространство имен метода Ветвей и Границ
using matsps.Parameters;                       // параметры алгоритмов

namespace matsps
{
    /// <summary>
    /// Содержит параметры запуска алгоритма
    /// </summary>
    public class algStartParam
    {
        public algStartParam(bool selected, int instCount)
        {
            this.selected = selected;
            this.instCount = instCount;
        }
        #region Свойства
        /// <summary>
        /// Выбран/не выбран
        /// </summary>
        public bool selected
        {
            get;
            set;
        }
        /// <summary>
        /// Ко-во экземаляров для запуска
        /// </summary>
        public int instCount
        {
            get;
            set;
        }

        #endregion
    }
    public partial class frmMain : Form
    {
        #region Конструкторы и Данные
        public frmMain()                    
        {
            InitializeComponent();

            // Начальная инициализация параметров расчета. Используются параметры по умолчанию.
            _paramAnt = new AntParameters();
            _paramBnB = new BnBParameters();

            // Отправляем ссылку на Лист Маршрутов контроллу прорисовки
            liRoute = new List<Route>();
            ucCP.ListRoute = liRoute;

            //инициализация списка экземпляров алгоритма муравья
            _prAntList = new List<ProcessAnt>();
        }

        /// <summary>
        /// Коллекция городов
        /// </summary>
        private CitiesCollection _cities;
        /// <summary>
        /// Параметры расчета алгоритма муравьиной колонии
        /// </summary>
        private matsps.Parameters.AntParameters   _paramAnt;
        /// <summary>
        /// Параметры расчета алгоритма Ветвей и границ
        /// </summary>
        private matsps.Parameters.BnBParameters  _paramBnB;

        private List<ProcessAnt> _prAntList;
        ///// <summary>
        ///// Обрабочик алгоритма расчета по методу Муравьиной колонии
        ///// </summary>
        //private ProcessAnt _prAnt;
        /// <summary>
        /// Обрабочик алгоритма расчета по методу Ближайшего соседа
        /// </summary>
        private ProcessNearestNeighbour         _pnn;
        /// <summary>       
        /// /// Обрабочик алгоритма расчета по методу Ветвей и границ
        /// </summary>
        private ProcessBranchAndBound           _prBnB;

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
            Forms.Parameters.frmSelectAlgs frmPar = new matsps.Forms.Parameters.frmSelectAlgs(_paramAnt, _cities.Count);
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
            Forms.About.frmAbout ab = new matsps.Forms.About.frmAbout();
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
            _cities = new CitiesCollection(iCitiesCount);
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
            Forms.SelectAlgs.frmSelectAlgs sa = new matsps.Forms.SelectAlgs.frmSelectAlgs();
            DialogResult res = sa.ShowDialog();

            switch (res)
            {
                case DialogResult.OK:
                    {
                        List<algStartParam> selectList = sa.getSelectList();//список выбранных алгоритмов                        
                        //int iCount = selectList.Count; //количество выбранных алгоритмов

                        //Последовательный запуск выбранных алгоритмов

                        if (selectList[0].selected)
                        {
                            //количество запусков
                            int iCount = selectList[0].instCount;
                            for (int i = 0; i < iCount; i++)
                            {
                                //запуск алгоритма муравьиной колонии
                                AntAlgStart();
                            }
                        }
                        if (selectList[1].selected)
                        {
                            //количество запусков
                            int iCount = selectList[1].instCount;
                            for (int i = 0; i < iCount; i++)
                            {
                                //запуск алгоритма соседа
                                NearestNeighbourStart();  
                            }
                        }
                        if (selectList[2].selected)
                        {
                            //количество запусков
                            int iCount = selectList[2].instCount;
                            for (int i = 0; i < iCount; i++)
                            {
                                //запуск алгоритма соседа
                                BranchAndBoundStart();   
                            }
                        }
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
            //if (_prAnt == null)
            //{
                // Интерфейс
                toolSTLProgress.Visible = true;
                ToolStripProgress.Visible = true;
                toolSTLInfo.Text = DateTime.Now.ToLongTimeString(); //.ToString("{0:H:mm:ss zzz}");
                tlStrpTxbCitiesCount.Enabled = false;
                //tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                //_prAnt = new ProcessAnt();
                //_prAnt.eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                //_prAnt.eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                //_prAnt.Parameters = _paramAnt;
                //_prAnt.Cities = _cities;
                //_prAnt.Start();
                
                //Добавляем экземпляр алгоритма в список
                _prAntList.Add(new ProcessAnt());
                //Подписываем последний в списке экземпляр алгоритма на необходимые события
                _prAntList[_prAntList.Count-1].eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                _prAntList[_prAntList.Count - 1].eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                //Загружаем параметры расчета алгоритма в последний в списке экземпляр
                _prAntList[_prAntList.Count - 1].Parameters = _paramAnt;
                //Загружаем коллекия городов в последний в списке экземпляр алгоритма
                _prAntList[_prAntList.Count - 1].Cities = _cities;
                //Запускаем последний в списке экземпляр алгоритма
                _prAntList[_prAntList.Count - 1].Start();
            //}
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


        /// <summary>
        /// Начать расчет методом Ветвей и границ
        /// </summary>
        private void BranchAndBoundStart()
        {
            // АЛГОРИТМ
            if (_prBnB == null)
            {
                // Интерфейс
                toolSTLProgress.Visible = true;
                ToolStripProgress.Visible = true;
                toolSTLInfo.Text = DateTime.Now.ToLongTimeString(); //.ToString("{0:H:mm:ss zzz}");
                tlStrpTxbCitiesCount.Enabled = false;
                //tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                _prBnB = new ProcessBranchAndBound();
                //_prAnt.eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                //_prAnt.eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                _prBnB.Parameters = _paramBnB;
                _prBnB.Cities = _cities;
                //_prAnt.Start();
                _prBnB.Start();
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
                    //toolSTLProgress.Text = "Процент вполнения: " + value.ToString() + "%";
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
                    //List<string> listr = _prAnt.ResultInfo;
                    List<string> listr = _prAntList[_prAntList.Count-1].ResultInfo;
                    foreach (string str in listr)
                    {
                        rtxbOut.AppendText(str);
                    }
                    rtxbOut.AppendText("--------------------------------------------\n");
                    // Лист последовательности городов
                    //CitiesCollection CitiesInPath = _prAnt.ResultPath.Cities;
                    CitiesCollection CitiesInPath = _prAntList[_prAntList.Count-1].ResultPath.Cities;
                    rtxbCities.Clear();
                    for (int i = 0; i < CitiesInPath.Count; i++)
                    {
                        rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[i].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                    }

                    // Путь городов. Заносим лист.
                   //_prAnt.ResultPath.AlgorithmName = "Муравей";
                    _prAntList[_prAntList.Count-1].ResultPath.Drawing.Color = Color.Purple; // цвет маршрута
                    //_prAnt.ResultPath.Drawing.Color = Color.Purple; // цвет маршрута
                    liRoute.Add(_prAntList[_prAntList.Count-1].ResultPath);
                    ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                    ucCP.RefreshRoutePaint(); // обновляем прорисовку                    

                    toolSTLInfo.Text = "Время расчета: " + _prAntList[_prAntList.Count-1].ProcessTime.ToString();

                    // Готовность интерфейса
                    _prAntList[_prAntList.Count-1] = null;
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
                    CitiesCollection CitiesInPath = _pnn.ResultPath.Cities;
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

        private void tlStrpBtnSaveCities_Click(object sender, EventArgs e)
        {
            // Создаем новый файловый диалог
            SaveFileDialog DialogSave = new SaveFileDialog();
            // Задаем расширение файла по умолчангию
            DialogSave.DefaultExt = "txt";
            //Формируем название файла
            DialogSave.FileName = "matsps_" + String.Format("{0:0000}", _cities.Count) + "_" + String.Format("{0:yyyy.MM.dd-HH.mm.ss}", DateTime.Now);
            // Задаема доступные расширения файлов
            DialogSave.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            // Разрешаем автоматическое добавление расширения файла
            DialogSave.AddExtension = true;
            // Разрешаем восстановление текущей папки перед закрытием
            DialogSave.RestoreDirectory = true;
            // Заголовог диалога
            DialogSave.Title = "Вы хотите сохранить файл?";
            // Дириктория по умолчанию
            DialogSave.InitialDirectory = @"C:/";

            // Показать диалоговое окно
            if (DialogSave.ShowDialog() == DialogResult.OK)
            {
                int Count = _cities.Count;  //количество городов
                string text="";             //обнуляем строку
                for (int i = 0; i < Count; i++)
                {
                    string posX = _cities.Cities[i].X.ToString(); //координата X
                    string posY = _cities.Cities[i].Y.ToString(); //координата Y
                    text += posX + "\t" + posY + "\r\n";
                }

                string sFileName = DialogSave.FileName; //Получаем имя файла
                StreamWriter sw = null;
                try
                {
                    FileStream fs = File.Create(sFileName); //Создаем файл с заданным именем                               
                    sw = new StreamWriter(fs);
                    sw.WriteLine(text);   //записываем текст в файл
                }
                catch (IOException fe)
                {
                    string sDir = Directory.GetCurrentDirectory();
                    string s = Path.Combine(sDir, sFileName);
                    MessageBox.Show("Ошибка в " + s + ".  " + fe.Message);
                }
                    sw.Close();  //закрываем файл
            }

        }

        private void tlStrpBtnLoadCities_Click(object sender, EventArgs e)
        {           
            // Создаем новый файловый диалог
            OpenFileDialog DialogOpen = new OpenFileDialog();

            if ( DialogOpen.ShowDialog() == DialogResult.OK)
            {
                _cities.RemoveAll();
                string sFileName = DialogOpen.FileName; //Получаем имя файла
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(sFileName);
                    String line;
                    string[] param; //массив координат записи (x,y)
                    int i=0;

                    while ((line = sr.ReadLine()) != "")
                    {
                        param = Regex.Split(line, "\t");
                        int PosX = Convert.ToInt32(param[0]);
                        int PosY = Convert.ToInt32(param[1]);
                        _cities.Add(new City(PosX,PosY));
                        _cities[i].Index = i;
                        i++;
                    }
                }
                catch (IOException fe)
                {
                    string sDir = Directory.GetCurrentDirectory();
                    string s = Path.Combine(sDir, sFileName);
                    MessageBox.Show("Ошибка в " + s + ".  " + fe.Message);
                }
                // Прорисовка городов
                ucCP.Cities = _cities;
                ucCP.ClearDgvRouteList(); //Очищает лист маршрутов и DataGridView
                ucCP.RefreshRoutePaint(); //Перерисовывает маршруты
            }
        }
    }
}
