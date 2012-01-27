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
using matsps.GeneticAlgorithm;
using matsps.Parameters;
using matsps.AntAlgorithm;                       // параметры алгоритмов

namespace matsps
{
    public partial class frmMain : Form
    {
        #region Конструкторы и Данные
        
        /// <summary>
        /// лист отладочных данных
        /// </summary>
        private List<string> listr = null;
       
        public frmMain()                    
        {
            InitializeComponent();

            // Начальная инициализация параметров расчета. Используются параметры по умолчанию.
            _paramAnt = new AntParameters();
            _paramBnB = new BnBParameters();
            _paramGA  = new GAParameters();

            // Отправляем ссылку на Лист Маршрутов контроллу прорисовки
            liRoute = new List<Route>();
            ucCP.ListRoute = liRoute;

            //инициализация списков экземпляров алгоритмов
            _prAntList = new List<ProcessAnt>();
            _prNNList = new List<ProcessNearestNeighbour>();
            _prBnBList = new List<ProcessBranchAndBound>();
            _prGAList = new List<ProcessGeneticAlgorithm>();
        }

        /// <summary>
        /// Коллекция городов
        /// </summary>
        private CitiesCollection _cities;
        /// <summary>
        /// Параметры расчета алгоритма муравьиной колонии
        /// </summary>
        private matsps.Parameters.AntParameters _paramAnt;
        /// <summary>
        /// Параметры расчета алгоритма Ветвей и границ
        /// </summary>
        private matsps.Parameters.BnBParameters _paramBnB;
        /// <summary>
        /// Параметры расчета Генетического алгоритма
        /// </summary>
        private matsps.Parameters.GAParameters  _paramGA;

        private List<ProcessAnt>                _prAntList;
        private List<ProcessNearestNeighbour>   _prNNList;
        private List<ProcessBranchAndBound>     _prBnBList;
        private List<ProcessGeneticAlgorithm>   _prGAList;
        ///// <summary>
        ///// Обрабочик алгоритма расчета по методу Муравьиной колонии
        ///// </summary>
        //private ProcessAnt _prAnt;
        /// <summary>
        /// Обрабочик алгоритма расчета по методу Ближайшего соседа
        /// </summary>
        //private ProcessNearestNeighbour         _prNN;
        /// <summary>       
        /// Обрабочик алгоритма расчета по методу Ветвей и границ
        /// </summary>
        //private ProcessBranchAndBound           _prBnB;
        /// <summary>       
        /// Обрабочик алгоритма расчета Генетического алгоритма
        /// </summary>
        //private ProcessGeneticAlgorithm         _pGA;

        /// <summary>
        /// Лист расчитанных маршрутов. Все расчитанные маршруты за текущую сессию, помещаются сюда.
        /// </summary>
        private List<Route> liRoute;
        /// <summary>
        /// Список связей между процессами алгоиртмов и метками процесса выполнения
        /// </summary>
        private List<ProcessLinkedLabelPercent> liLinkedPrLabel;
        #endregion

        #region События главной формы

        /// <summary>
        /// Загрузка формы. Запуск алгоритма
        /// </summary>
        private void frmMain_Load(object sender, EventArgs e)       
        {
            tlStrpTxbCitiesCount.Text = "10"; // по умолчанию создаем 10 городов
            tlStrpTxbCitiesCount.Focus();
            tlStrpBtnCreateRandomCities_Click(this, new EventArgs());

            listr = new List<string>();

 
        }

        /// <summary>
        /// Хоткеи
        /// </summary>
        private void tlStrpTxbCitiesCount_KeyDown(object sender, KeyEventArgs e)
        {
            // Быстрый запуск Генетического алгоритма
            if (e.KeyData == (Keys.Control | Keys.G | Keys.Alt))
            {
                liLinkedPrLabel = new List<ProcessLinkedLabelPercent>();
                GeneticAlgorithmStart();
            }
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
            liLinkedPrLabel = new List<ProcessLinkedLabelPercent>();
            statusStrip1.Items.Clear();
            ToolStripLabel toolSTLInfo = new ToolStripLabel();
            statusStrip1.Items.Add(toolSTLInfo);

            switch (res)
            {
                case DialogResult.OK:
                    {
                        List<AlgStartParam> selectList = sa.getSelectList();//список выбранных алгоритмов                        
                        //int iCount = selectList.Count; //количество выбранных алгоритмов

                        for (int k = 0; k < selectList.Count; k++)
                        {       
                           string alg = selectList[k].name;
                             switch(alg)
                                {
                                    case "Муравьиной колонии":   
                                        {                                          
                                            for (int i = 0; i < selectList[k].InstCount; i++)
                                                AntAlgStart();
                                        } break;
                                    case "Ближайшего соседа": 
                                        {
                                            for (int j = 0; j < selectList[k].InstCount; j++)
                                                NearestNeighbourStart();  
                                        } break;
                                    case "Ветвей и границ (не более 20 городов)":
                                        {
                                            for (int j = 0; j < selectList[k].InstCount; j++)
                                                BranchAndBoundStart();
                                        } break;
                                    case "Генетический":
                                        {
                                            for (int j = 0; j < selectList[k].InstCount; j++)
                                                 GeneticAlgorithmStart(); 
                                                
                                        } break;
                                }; //switch(alg)                            
                         }//for
                    }//case DialogResult.OK
                    break;
            }
        }
        /// <summary>
        /// Сброс расчетов
        /// </summary>        
        private void tlStripBtnReset_Click(object sender, EventArgs e)              
        {
            liRoute.Clear();
            ucCP.Cities = _cities;
            ucCP.RefreshRoutePaint();
            rtxbOut.Clear();
            rtxbCities.Clear();
        }

        /// <summary>
        /// Сохранение городов в файл
        /// </summary>
        private void tlStrpBtnSaveCitiesCities_ButtonClick(object sender, EventArgs e)  
        {
            if (tlStrpBtnSaveModeCoords.Checked)
                SaveCitiesToFile(0);
            if (tlStrpBtnSaveModeDistance.Checked)
                SaveCitiesToFile(1);
        }
        private void tlStrpBtnSaveModeCoords_Click(object sender, EventArgs e)          
        {
            tlStrpBtnSaveModeCoords.Checked = true;
            tlStrpBtnSaveModeDistance.Checked = false;
        }
        private void tlStrpBtnSaveModeDistance_Click(object sender, EventArgs e)        
        {
            tlStrpBtnSaveModeCoords.Checked = false;
            tlStrpBtnSaveModeDistance.Checked = true;
        }
        private void tlStrpBtnLoadCities_Click(object sender, EventArgs e)              
        {
            // Создаем новый файловый диалог
            OpenFileDialog DialogOpen = new OpenFileDialog();
            // Задаема доступные расширения файлов
            DialogOpen.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";

            if (DialogOpen.ShowDialog() == DialogResult.OK)
            {
                _cities.RemoveAll();
                string sFileName = DialogOpen.FileName; //Получаем имя файла
                StreamReader sr = null;
                try
                {
                    sr = new StreamReader(sFileName);
                    String line;
                    string[] param; //массив координат записи (x,y)
                    int i = 0;

                    while ((line = sr.ReadLine()) != "")
                    {
                        param = Regex.Split(line, "\t");
                        int PosX = Convert.ToInt32(param[0]);
                        int PosY = Convert.ToInt32(param[1]);
                        _cities.Add(new City(PosX, PosY));
                        _cities[i].Index = i;
                        i++;
                    }
                    _cities.DistanceCalculate();
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
                tlStrpTxbCitiesCount.Text = _cities.Count.ToString();
                tlStrpTxbCitiesCount.SelectAll();
            }
        }
        /// <summary>
        /// Формирует строку для сохранения в файл
        /// </summary>
        /// <param name="filename">имя файла</param>
        /// <param name="mode">Метод формирования строки: 
        ///     0 - координаты, 
        ///     1 - матрица расстояний</param>
        private string GetFileStringToSave(string filename, int mode)                   
        {
            int Count = _cities.Count;  //количество городов
            string text = "";             //обнуляем строку

            if (mode == 0) // формирование координат
            {
                for (int i = 0; i < Count; i++)
                {
                    string posX = _cities.Cities[i].X.ToString(); //координата X
                    string posY = _cities.Cities[i].Y.ToString(); //координата Y
                    text += posX + "\t" + posY + "\r\n";
                }
            }

            if (mode == 1) // матрицы расстояний
            {
                for (int i = 0; i < Count; i++)
                    for (int j = 0; j < Count; j++)
                    {
                        text += String.Format(" {0,3} {1,3} {2,5:0.0}\r\n", i + 1, j + 1, _cities.Distance[i, j]);
                    }
            }

            return text;
        }
        /// <summary>
        /// Сохраняет коллекцию городов в файл 
        /// </summary>
        /// <param name="mode">Метод сохранения: 
        ///     0 - координаты, 
        ///     1 - матрица расстояний</param>
        private void SaveCitiesToFile(int mode)                                         
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
                string sFileName = DialogSave.FileName; //Получаем имя файла                
                string text = GetFileStringToSave(sFileName, mode);

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
            }//IF

        }


        /// <summary>
        /// Начать расчет методом Муравьиной колонии
        /// </summary>
        private void AntAlgStart()                              
        {
            // АЛГОРИТМ
            //if (_prAnt == null)
            //{
            //Добавляем экземпляр алгоритма в список
            _prAntList.Add(new ProcessAnt());

                // Интерфейс
                toolSTLInfo.Text = DateTime.Now.ToLongTimeString(); //.ToString("{0:H:mm:ss zzz}");
                tlStrpTxbCitiesCount.Enabled = false;
                //tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;
                // label c процентами
                ToolStripLabel labelPercent = new ToolStripLabel();
                labelPercent.BackColor = _prAntList[_prAntList.Count - 1].Drawing.Color;
                labelPercent.ForeColor = Color.White;
                labelPercent.Margin = new Padding(0,0,5,0);
                statusStrip1.Items.Add(labelPercent);
                liLinkedPrLabel.Add(new ProcessLinkedLabelPercent(_prAntList[_prAntList.Count - 1],labelPercent));

                //_prAnt = new ProcessAnt();
                //_prAnt.eventProgressChanged += new ProcessAnt.ProgressChanged(AntAlgProgressChange);
                //_prAnt.eventFinally += new EventHandler<EventArgs>(AntAlgFinally);
                //_prAnt.Parameters = _paramAnt;
                //_prAnt.Cities = _cities;
                //_prAnt.Start();
               
                
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
            //if (_prNN == null)
            //{
                // Интерфейс
                //ucCP.RouteLengthTextOut("");
                toolSTLInfo.Text = DateTime.Now.ToShortTimeString();
                tlStrpTxbCitiesCount.Enabled = false;
                //tsbNearestNeighbour.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                _prNNList.Add(new ProcessNearestNeighbour());
                _prNNList[_prNNList.Count - 1].eventProgressChanged += new ProcessNearestNeighbour.ProgressChanged(AntAlgProgressChange);
                _prNNList[_prNNList.Count - 1].eventFinally += new EventHandler<EventArgs>(PNNFinally);
                _prNNList[_prNNList.Count - 1].Parameters = _paramAnt;
                _prNNList[_prNNList.Count - 1].Cities = _cities;
                _prNNList[_prNNList.Count - 1].Start();
        }
        /// <summary>
        /// Начать расчет методом Ветвей и границ
        /// </summary>
        private void BranchAndBoundStart()                      
        {
            // АЛГОРИТМ
            //if (_prBnB == null)
            //{
            _prBnBList.Add(new ProcessBranchAndBound());

                // Интерфейс
                toolSTLInfo.Text = DateTime.Now.ToLongTimeString(); //.ToString("{0:H:mm:ss zzz}");
                tlStrpTxbCitiesCount.Enabled = false;
                //tlStrpBtnAntAlgStart.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;
                // label c процентами
                ToolStripLabel labelPercent = new ToolStripLabel();
                labelPercent.BackColor = _prBnBList[_prBnBList.Count - 1].Drawing.Color;
                labelPercent.ForeColor = Color.White;
                labelPercent.Margin = new Padding(0, 0, 5, 0);
                statusStrip1.Items.Add(labelPercent);
                liLinkedPrLabel.Add(new ProcessLinkedLabelPercent(_prBnBList[_prBnBList.Count - 1], labelPercent));

                //Добавляем экземпляр алгоритма в список                            
                _prBnBList[_prBnBList.Count - 1].eventProgressChanged += new ProcessBranchAndBound.ProgressChanged(AntAlgProgressChange);
                _prBnBList[_prBnBList.Count - 1].eventFinally += new EventHandler<EventArgs>(BnBAlgFinally);
                _prBnBList[_prBnBList.Count - 1].Parameters = _paramBnB;
                _prBnBList[_prBnBList.Count - 1].Cities = _cities;
                _prBnBList[_prBnBList.Count - 1].Start();
            //}
        }
        /// <summary>
        /// Начать расчёт с помощью Генетического алгоритма
        /// </summary>
        private void GeneticAlgorithmStart()                    
        {
            // АЛГОРИТМ
            //if (_pGA == null)
           // {
            ProcessGeneticAlgorithm pga = new ProcessGeneticAlgorithm();
            //_prGAList.Add(new ProcessGeneticAlgorithm());
            _prGAList.Add(pga);
            // Интерфейс
                //ucCP.RouteLengthTextOut("");
                toolSTLInfo.Text = DateTime.Now.ToShortTimeString();
                tlStrpTxbCitiesCount.Enabled = false;
                //tsbNearestNeighbour.Enabled = false;
                tlStrpBtnCreateRandomCities.Enabled = false;

                ToolStripLabel labelPercent = new ToolStripLabel();
                labelPercent.BackColor = _prGAList[_prGAList.Count - 1].Drawing.Color;
                labelPercent.ForeColor = Color.White;
                labelPercent.Margin = new Padding(0, 0, 5, 0);
                statusStrip1.Items.Add(labelPercent);
                liLinkedPrLabel.Add(new ProcessLinkedLabelPercent(_prGAList[_prGAList.Count - 1], labelPercent));

                //_pGA = new ProcessGeneticAlgorithm();
                _prGAList[_prGAList.Count - 1].eventProgressChanged += new ProcessGeneticAlgorithm.ProgressChanged(AntAlgProgressChange);
                _prGAList[_prGAList.Count - 1].eventFinally += new EventHandler<EventArgs>(GAFinally);
                _prGAList[_prGAList.Count - 1].Parameters = _paramGA;
                _prGAList[_prGAList.Count - 1].Cities = _cities;
                _prGAList[_prGAList.Count - 1].Start();
            //}
        }

        #endregion

        #region События алгоритмов
        /// <summary>
        /// Изменение в Алгоритмах
        /// </summary>
        /// <param name="value"></param>
        private void AntAlgProgressChange(object sender, int value, string label)                    
        {
            //prgbarProgress.Value = value;
            //toolSTProgress.Text = value + "%";
            try
            {
                Object locker = new Object();
                lock (locker)
                {
                    this.Invoke(new MethodInvoker(delegate()
                    {
                        //toolSTLProgress.Text = "Процент вполнения: " + value.ToString() + "%";
                        for (int i = 0; i < liLinkedPrLabel.Count; i++)
                        {
                            if (liLinkedPrLabel[i].Process == sender)
                            {
                                liLinkedPrLabel[i].Label.Text = (value==-1)?"-":(value.ToString() + label);
                                break;
                            }
                        }
                    }));
                }
                /*
                if (statusStrip1.InvokeRequired)
                {
                    var d = new IncrementCallback(AntAlgProgressChange);
                    Invoke(d, value);
                }
                else
                {
                    ToolStripProgress.Value = value;
                }*/
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
            Object thisLock = new Object();
            lock (thisLock)
            {
                // Critical code section
                this.Invoke(new MethodInvoker(delegate()
                {
                    // РЕЗУЛЬТАТЫ
                        //Начинаем перебор списка экземпляров алгоритма муравья
                        #region while
                        for (int i = 0; i < _prAntList.Count; i++ )
                        {
                            #region if
                            if (_prAntList[i].ResultInfo != null)
                            {
                                // Лист результатов расчета
                                rtxbOut.AppendText("        алгоритм муравья (" + i + ")       \n" );
                                List<string> listr = _prAntList[i].ResultInfo;
                                foreach (string str in listr)
                                {
                                    rtxbOut.AppendText(str);
                                }
                                rtxbOut.AppendText("--------------------------------------------\n");
                                // Лист последовательности городов
                                CitiesCollection CitiesInPath = _prAntList[i].ResultPath.Cities;
                                rtxbCities.Clear();
                                for (int k = 0; k < CitiesInPath.Count; k++)
                                {
                                    rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[k].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                                }

                                // Путь городов. Заносим лист.
                                _prAntList[i].ResultPath.Drawing.Color = _prAntList[i].Drawing.Color; // цвет маршрута
                                liRoute.Add(_prAntList[i].ResultPath);
                                ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                                ucCP.RefreshRoutePaint(); // обновляем прорисовку                    
 
                                toolSTLInfo.Text = "Время расчета: " + _prAntList[i].ProcessTime.ToString();
                                // Удаляем label-процента исполнения
                                for (int j = 0; j < liLinkedPrLabel.Count; j++)
                                    if (liLinkedPrLabel[j].Process == _prAntList[i])
                                    {
                                        toolStrip1.Items.Remove(liLinkedPrLabel[j].Label);
                                        liLinkedPrLabel.RemoveAt(j);

                                        if (liLinkedPrLabel.Count == 0)
                                            statusStrip1.Items.Clear();

                                        break;                                        
                                    }

                                // Готовность интерфейса
                                _prAntList[i] = null;
                                tlStrpTxbCitiesCount.Enabled = true;
                                tlStrpBtnCreateRandomCities.Enabled = true;

                                //удаление экземпляра алгортима из списка экземпляров
                                _prAntList.RemoveAt(i);
                                break;
                            }
                            #endregion
                        }
                        #endregion
                }));

            }
            

        }
        private void PNNFinally(object sender, EventArgs e)             
        {
            Object thisLock = new Object();
            lock (thisLock)
            {
                // Critical code section
                this.Invoke(new MethodInvoker(delegate()
                {
                    //Начинаем перебор списка экземпляров алгоритма муравья
                        #region while
                        for (int i = 0; i < _prNNList.Count; i++)
                        {
                            #region if
                            if (_prNNList[i].ResultList != null)
                            {
                                try
                                {
                                    // РЕЗУЛЬТАТЫ
                                    // Лист результатов расчета
                                    rtxbOut.AppendText("        ближайший сосед (" + i + ")       \n");
                                    List<string> listr = _prNNList[i].ResultList;
                                    foreach (string str in listr)
                                    {
                                        rtxbOut.AppendText(str);
                                    }
                                    rtxbOut.AppendText("\n--------------------------------------------\n");
                                    // Лист последовательности городов
                                    CitiesCollection CitiesInPath = _prNNList[i].ResultPath.Cities;
                                    rtxbCities.Clear();
                                    for (int k = 0; k < CitiesInPath.Count; k++)
                                    {
                                        rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[i].Index) + " X:" + CitiesInPath[k].X + " Y:" + CitiesInPath[k].Y + Environment.NewLine);
                                    }

                                    // Путь городов
                                    //_pnn.ResultPath.AlgorithmName = "Ближайший сосед";
                                    _prNNList[i].ResultPath.Drawing.Color = Color.LightSeaGreen;
                                    liRoute.Add(_prNNList[i].ResultPath);
                                    ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                                    ucCP.RefreshRoutePaint(); // обновляем прорисовку

                                    toolSTLInfo.Text = "Время расчета: " + _prNNList[i].ProcessTime.ToString();
                                    // Удаляем label-процента исполнения
                                    for (int j = 0; j < liLinkedPrLabel.Count; j++)
                                        if (liLinkedPrLabel[j].Process == _prNNList[i])
                                        {
                                            toolStrip1.Items.Remove(liLinkedPrLabel[j].Label);
                                            liLinkedPrLabel.RemoveAt(j);

                                            if (liLinkedPrLabel.Count == 0)
                                                statusStrip1.Items.Clear();
                                            break;
                                        }

                                    // Готовность интерфейса
                                    _prNNList[i] = null;
                                    tlStrpTxbCitiesCount.Enabled = true;
                                    //tlStrpBtnAntAlgStart.Enabled = true;
                                    tlStrpBtnCreateRandomCities.Enabled = true;
                                    //tsbNearestNeighbour.Enabled = true;
                                    //ToolStripProgress.Visible = false;
                                    //toolSTLProgress.Visible = false;

                                    //удаление экземпляра алгортима из списка экземпляров
                                    _prNNList.RemoveAt(i);
                                    break;
                                }//try
                                catch (Exception ex)
                                { 
                                    MessageBox.Show(ex.Message + ex.StackTrace); 
                                } 
                            }//IF
                             #endregion
                        }//For
                        #endregion
                }));
            }

        }
        private void BnBAlgFinally(object sender, EventArgs e)          
        {
            Object thisLock = new Object();
            lock (thisLock)
            {
                // Critical code section
                this.Invoke(new MethodInvoker(delegate()
                {
                    // РЕЗУЛЬТАТЫ
                    //Начинаем перебор списка экземпляров алгоритма муравья
                    #region while
                    for (int i = 0; i < _prBnBList.Count; i++)
                    {
                        #region if
                        if (_prBnBList[i].ResultInfo != null)
                        {
                            // Лист результатов расчета
                            rtxbOut.AppendText("        ветви и границы (" + i + ")       \n");
                            List<string> listr = _prBnBList[i].ResultInfo;
                            foreach (string str in listr)
                            {
                                rtxbOut.AppendText(str);
                                rtxbOut.AppendText("\n");
                            }
                            rtxbOut.AppendText("--------------------------------------------\n");

                            // Лист последовательности городов
                            CitiesCollection CitiesInPath = _prBnBList[i].ResultPath.Cities;
                            rtxbCities.Clear();
                            for (int k = 0; k < CitiesInPath.Count; k++)
                            {
                                rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[k].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                            }

                            // Путь городов. Заносим лист.
                            //_prBnBList[i].ResultPath.Drawing.Color = Color.Orange; // цвет маршрута
                            //liRoute.Add(_prBnBList[i].ResultPath);
                            _prBnBList[i].ResultPath.Drawing.Color = _prBnBList[i].Drawing.Color;
                            liRoute.AddRange(_prBnBList[i].ResultPathList);
                            ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                            ucCP.RefreshRoutePaint(); // обновляем прорисовку                    

                            toolSTLInfo.Text = "Время расчета: " + _prBnBList[i].ProcessTime.ToString();
                            // Удаляем label-процента исполнения
                            for (int j = 0; j < liLinkedPrLabel.Count; j++)
                                if (liLinkedPrLabel[j].Process == _prBnBList[i])
                                {
                                    toolStrip1.Items.Remove(liLinkedPrLabel[j].Label);
                                    liLinkedPrLabel.RemoveAt(j);

                                    if (liLinkedPrLabel.Count == 0)
                                        statusStrip1.Items.Clear();
                                    break;
                                }

                            // Готовность интерфейса
                            _prBnBList[i] = null;
                            tlStrpTxbCitiesCount.Enabled = true;
                            tlStrpBtnCreateRandomCities.Enabled = true;

                            //удаление экземпляра алгортима из списка экземпляров
                            _prBnBList.RemoveAt(i);
                            break;
                        }
                        #endregion
                    }
                    #endregion
                }));

            }            
        }
        private void GAFinally(object sender, EventArgs e)
        {
            Object thisLock = new Object();
            lock (thisLock)
            {
                // Critical code section
                this.Invoke(new MethodInvoker(delegate()
                {
                    // РЕЗУЛЬТАТЫ
                    //Начинаем перебор списка экземпляров алгоритма муравья
                    #region while
                    for (int i = 0; i < _prGAList.Count; i++)
                    {
                        #region if
                        if (_prGAList[i].ResultInfo != null)
                        {
                            // Лист результатов расчета
                            rtxbOut.AppendText("        генетический (" + i + ")       \n");
                            List<string> listr = _prGAList[i].ResultInfo;
                            foreach (string str in listr)
                            {
                                rtxbOut.AppendText(str);
                                rtxbOut.AppendText("\n");
                            }
                            rtxbOut.AppendText("--------------------------------------------\n");

                            // Лист последовательности городов
                            CitiesCollection CitiesInPath = _prGAList[i].ResultPath.Cities;
                            rtxbCities.Clear();
                            for (int k = 0; k < CitiesInPath.Count; k++)
                            {
                                rtxbCities.AppendText(String.Format("{0:0000}", CitiesInPath[k].Index) + " X:" + CitiesInPath[i].X + " Y:" + CitiesInPath[i].Y + Environment.NewLine);
                            }

                            // Путь городов. Заносим лист.
                            //_prBnBList[i].ResultPath.Drawing.Color = Color.Orange; // цвет маршрута
                            //liRoute.Add(_prBnBList[i].ResultPath);
                            _prGAList[i].ResultPath.Drawing.Color = _prGAList[i].Drawing.Color;
                            liRoute.Add(_prGAList[i].ResultPath);
                            ucCP.RefreshRouteList();    // обновляем таблицу с листом маршрутов
                            ucCP.RefreshRoutePaint(); // обновляем прорисовку                    

                            toolSTLInfo.Text = "Время расчета: " + _prGAList[i].ProcessTime.ToString();
                            // Удаляем label-процента исполнения
                            for (int j = 0; j < liLinkedPrLabel.Count; j++)
                                if (liLinkedPrLabel[j].Process == _prGAList[i])
                                {
                                    toolStrip1.Items.Remove(liLinkedPrLabel[j].Label);
                                    liLinkedPrLabel.RemoveAt(j);

                                    if (liLinkedPrLabel.Count == 0)
                                        statusStrip1.Items.Clear();
                                    break;
                                }

                            // Готовность интерфейса
                            _prGAList[i] = null;
                            tlStrpTxbCitiesCount.Enabled = true;
                            tlStrpBtnCreateRandomCities.Enabled = true;

                            //удаление экземпляра алгортима из списка экземпляров
                            _prGAList.RemoveAt(i);
                            break;
                        }
                        #endregion
                    }
                    #endregion
                }));

            }
        }


        #endregion





        
    }


    /// <summary>
    /// Содержит имя и параметры запуска алгоритма
    /// </summary>
    public class AlgStartParam                                  
    {
        public AlgStartParam(string name, int instCount)
        {
            this.name = name;
            this.InstCount = instCount;
        }
        #region Свойства
        /// <summary>
        /// Номер выбранного алгоритма
        /// </summary>
        public int AlgNumber
        {
            get;
            set;
        }
        /// <summary>
        /// Ко-во экземаляров для запуска
        /// </summary>
        public int InstCount
        {
            get;
            set;
        }
        
        /// <summary>
        /// Имя алгоритма
        /// </summary>
        public string name
        {
            get;
            set;
        }


        #endregion
    }

    /// <summary>
    /// Содержит связь между Process и label процента исполнения
    /// </summary>
    public class ProcessLinkedLabelPercent                      
    {
        #region Конструкторы
        public ProcessLinkedLabelPercent(Object process, ToolStripLabel label)
        {
            _process = process;
            _label = label;
        }
        #endregion

        #region Поля
        private Object _process;
        private ToolStripLabel _label;
        #endregion

        #region Свойства
        /// <summary>
        /// Процесс алгоиртма (только для чтения)
        /// </summary>
        public Object Process               
        {
            get
            {
                return _process;
            }
        }
        /// <summary>
        /// Метка с процентом выполнения (только для чтения)
        /// </summary>
        public ToolStripLabel Label         
        {
            get
            {
                return _label;
            }
        }
        #endregion
    }
}