using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Timers;
using System.Threading;

using matsps.CommonData;
using matsps.AntAlgData;
using matsps.Parameters;

namespace matsps.GeneticAlgorithm
{
    class GeneticAlgorithmTravelSalesman
    {
        #region Конструкторы и Данные
        public GeneticAlgorithmTravelSalesman()                                               
        {

        }
        public GeneticAlgorithmTravelSalesman(CitiesCollection cities):this()       
        {
            Cities = cities;
        }
        public GeneticAlgorithmTravelSalesman(CitiesCollection cities, GAParameters gap)
            :this(cities)                                                           
        {
            //SetParameters(gap);
            _gap = gap;
            tmrTimer = new System.Timers.Timer(10);                                      //инициализация таймера
            //tmrTimer.Elapsed += new ElapsedEventHandler(tmrTimer_Elapsed); //подписка на событие таймера "Elapsed"
        }

        /// <summary>
        /// Массив индивидов
        /// </summary>
        //private Agent[] agarrIndividuals;
        //private List<Individual> liInd = new List<Individual>();
        private List<int> liRoute = new List<int>();
        private CitiesCollection ccRoute = new CitiesCollection();
        private List<Agent> _liAgents = new List<Agent>();
        private Route _BestPath;
        //private double best;
        //private int bestIndex;

        private decimal maxTime;            //максимум проходов
        private decimal curTime;            //счетчик проходов
        private System.Timers.Timer tmrTimer = new System.Timers.Timer();         //таймер

        Random rnd = new Random();

        /// <summary>
        /// Поток вычислений
        /// </summary>
        private Thread t = null;
        /// <summary>
        /// Событие для остановки потока вычислений
        /// </summary>
        //static EventWaitHandle wh = new AutoResetEvent(false);
        /// <summary>
        /// Показывает, запущен ли сейчас поток
        /// </summary>
        private bool bIsThreadAlive = false;

        
        #endregion

        #region Свойства
        /// <summary>
        /// Возвращает или задает Коллекцию городов
        /// </summary>
        public CitiesCollection Cities                        
        {
            set;
            get;
        }

        /// <summary>
        /// Возвращает лучший маршрут
        /// </summary>
      //  public CitiesCollection BestPath
       // {
       //     get { return _BestPath; }
       // }

        private GAParameters _gap;
        #endregion

        #region Свойства результатов расчета
        /// <summary>
        ///  Лист времени расчёта каждого графа в маршруте
        /// </summary>
        List<string> listrTime;
        /// <summary>
        /// Лист длин маршрута по времени
        /// </summary>
        public List<string> ListTimeRoute
        {
            get
            {
                return listrTime;
            }
        }

        /// <summary>
        /// Лучший маршрут !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
        /// </summary>
        public Route BestPath()
        {
            if (_liAgents.Count != 0)
            {
                _liAgents.Sort();
                return _liAgents[0].Route;
            }
            else
            {
                return null;
            }
            
            //try
            //{
            //    Agent _bestIndividual = new Agent(agarrIndividuals[0].Route.Cities);
            //    _bestIndividual.Route = agarrIndividuals[0].Route;
            //    for (int i = 1; i < agarrIndividuals.Length; i++)
            //    {
            //        if (agarrIndividuals[i].RouteLength < _bestIndividual.RouteLength)
            //        {
            //            _bestIndividual = agarrIndividuals[i];
            //        }
            //    }
            //    return _bestIndividual.Route;   
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Не могу рассчитать лучший маршрут" + ex.Message + ex.StackTrace, "GenAlgTS, BestPath()");
            //}
           
        }

        #endregion

        ///// <summary>
        ///// Установить параметры алгоритма
        ///// </summary>
        //public void SetParameters(GAParameters param)
        //{
        //    if (Cities != null)
        //    {
        //        // Создаем соседей
        //        InitIndividuals();
        //    }

        //    _parameters = param;
        //    //best = _parameters.MaxTour;
        //   // OnFinally(EventArgs.Empty);
        //}

        ///// <summary>
        ///// Инициализация индивидов
        ///// </summary>
        //private void InitIndividuals()
        //{
        //    agarrIndividuals = new Agent[Cities.Count * 2];
        //}

        /// <summary>
        /// Произвести один расчет
        /// </summary>
        public void Calculate()
        {
            listrTime = new List<string>();

            if (t == null)
            {
                t = new Thread(BackgroundCalculate);
                t.IsBackground = true;
                t.Start();
                bIsThreadAlive = true;
            }
            //t.Join();            
        }

        public void BackgroundCalculate()
        {
            try
            {
                tmrTimer.Start();


                int iGenerations = _gap.GenerationsCount; 

                DateTime dtStart = DateTime.Now;
                string strOut;
                maxTime = iGenerations;
                Generation gen;
                if (Cities != null)
                {
                    gen = new Generation(_gap, Cities);
                }
                else
                {
                    throw new Exception("Коллекция городов пуста...");
                }

                for (int i = 0; i < iGenerations; i++)
                {
                    gen.PerformCrossbreeding();
                    gen.PerformMutation();
                    gen.PerformSelection();
                    _liAgents.Add(gen.GetBest());

                    curTime = i;

                    int iPercent = 0;
                    //Подсчитываем процент прогресса
                    if (maxTime > 0)
                    {
                        decimal dPercent = (decimal)curTime * 100 / maxTime;
                        iPercent = (int)dPercent;
                    }
                    else
                    { }
                    GeneticAlgorithmChangesEventArgs e = new GeneticAlgorithmChangesEventArgs(iPercent, "%");
                    

                    OnProgressChanged(e);
                }
                //agarrIndividuals = gen.GetAllAgents();


                



                // Вычисление завершено. Возвращаем результаты по событию
                OnFinally(new EventArgs());
            }
            catch (ThreadAbortException taex)
            { listrTime.Add(taex.Message); }
            catch(Exception ex)
            {
                listrTime.Add(ex.Message);
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }
        
        private static decimal Summ(int num)
        { 
            if (num == 0)
            {
                return 0;
            }
            else
            {
                return num + Summ(num - 1);
            }
        }
        //private bool PointIsNotFound(CitiesCollection route, int point)
        //{
        //    bool result = true;
        //    for (int i = 0; i < route.Count; i++)
        //    {
        //        if (point == route[i].Index)
        //            result = false;
        //    }
        //    return result;
        //}
        
        #region Событие
        /// <summary>
        /// Генерируется при изменении прогресса выполнения алгоритма
        /// </summary>
        //public event ProgressChanged eventProgressChanged;
        // Член события, делегат
        public event EventHandler<GeneticAlgorithmChangesEventArgs> eventProgressChanged;
        public event EventHandler<EventArgs> eventFinally;

        // Уведомляет подписанные на событие объекты
        protected virtual void OnProgressChanged(GeneticAlgorithmChangesEventArgs e)              
        {
            EventHandler<GeneticAlgorithmChangesEventArgs> tmp = eventProgressChanged;

            if (tmp != null)
                tmp(this, e);
        }
        protected virtual void OnFinally(EventArgs e)                                   
        {
            //OnProgressChanged(NearestNeighbourChangesEventArgs(100, true));
            curTime = maxTime;
            //tmrTimer_Elapsed(this, EventArgs.Empty);
            EventHandler<EventArgs> tmp = eventFinally;

            if (tmp != null)
            {
                bIsThreadAlive = false;
                tmp(this, e);
                t.Abort();
                t = null;
            }
        }

        // Метод, вызывающий событие
      //  protected virtual void tmrTimer_Elapsed(object sender, EventArgs e)             
       // {
            //wh.WaitOne();

            //int iPercent = 0;
            ////Подсчитываем процент прогресса
            //if (maxTime > 0)
            //{
            //    decimal dPercent = (decimal)curTime * 100 / maxTime;
            //    iPercent = (int)dPercent;  //только целые значения | а какие ещё могут быть в int?!
            //}
            //else 
            //{ }
            //GeneticAlgorithmChangesEventArgs eee = new GeneticAlgorithmChangesEventArgs(iPercent, false);

            //OnProgressChanged(eee); // да, еее - это по-мудацки!

            //wh.Set();
       // }
        #endregion


    }

    //------------------------------------------------------------------
    //     Аргументы событий изменения в генетическом алгоритме
    //------------------------------------------------------------------
    /// <summary>
    /// Аргументы событий изменения в алгоритме ближайшего соседа
    /// </summary>
    internal class GeneticAlgorithmChangesEventArgs : EventArgs
    {
        private readonly double _dPercent;
        private string _strLabel;

        public GeneticAlgorithmChangesEventArgs(double percent, string label)
        {
            _dPercent = percent;
            _strLabel = label;
        }

        /// <summary>
        /// Процент выполнения алгоритма
        /// </summary>
        public double Percent
        { get { return _dPercent; } }

        /// <summary>
        /// Подпись к состоянию ("%" или "итерция")
        /// </summary>
        public string Label
        {
            get { return _strLabel; }
        }
    };
}
