using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;

using ant.AntAlgData;
using ant.CommonData;
using ant.AntAlgLogic;

namespace ant.NearestNeighbour
{
    class NearestNeighbourTravelSalesman
    {
        #region Конструкторы и Данные
        public NearestNeighbourTravelSalesman()                                               
        {

        }
        public NearestNeighbourTravelSalesman(DataCitiesCollection cities):this()       
        {
            Cities = cities;
        }
        public NearestNeighbourTravelSalesman(DataCitiesCollection cities, AntAlgDataParameters param)
            :this(cities)                                                           
        {
            SetParameters(param);
            tmrTimer = new System.Timers.Timer(10);                                      //инициализация таймера
            tmrTimer.Elapsed += new ElapsedEventHandler(tmrTimer_Elapsed); //подписка на событие таймера "Elapsed"
        }


        //private List<int> liRoute = new List<int>();
        private DataCitiesCollection ccRoute = new DataCitiesCollection();
        public DataCitiesCollection BestPath = new DataCitiesCollection();
        private double best;
        private int bestIndex;

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
        public DataCitiesCollection Cities                        
        {
            set;
            get;
        }


        private AntAlgDataParameters _parameters;
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
        /// Коллекция с лучшим маршрутом
        /// </summary>
       // public AntAlgDataCitiesCollection BestPath
       // {
       //     get
        //    {
        //        AntAlgDataCitiesCollection cities = new AntAlgDataCitiesCollection();
        //        cities.MaxDistance = Cities.MaxDistance;
        //        for (int i = 0; i < Cities.Count; i++)
        //        {
        //          //  AntAlgDataCity newCity = new AntAlgDataCity(Ants[bestIndex].PathGet(i).X, Ants[bestIndex].PathGet(i).Y);
        //            //newCity.Index = Ants[bestIndex].PathGet(i).Index;
        //            //cities.Add(newCity);
        //        }
        //        return cities;
        //    }
        //}
        #endregion

        /// <summary>
        /// Установить параметры алгоритма
        /// </summary>
        public void SetParameters(AntAlgDataParameters param)
        {
            if (Cities != null)
            {
                // Создаем соседей
                InitNeighbours();
            }

            _parameters = param;
            best = _parameters.MaxTour;
           // OnFinally(EventArgs.Empty);
        }

        /// <summary>
        /// Инициализация соседей(запуск таймера)
        /// </summary>
        private void InitNeighbours()
        {

        }

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
                tmrTimer.Interval = 10;
                tmrTimer.Start();
                // Нужен для списка Табу
                AntAlgDataAnt ant = new AntAlgDataAnt();

                ant.TabuInit(Cities.Count);
                int iCurrentCity = 0;
                //ccRoute = new AntAlgDataCitiesCollection();
                //Cities[iCurrentCity].Index = iCurrentCity;
                DataCity city = Cities[iCurrentCity];
                city.Index = iCurrentCity;
                ccRoute.Add(city);
                ccRoute.MaxDistance = _parameters.MaxDistance;
                DateTime dtStart = DateTime.Now;
                string strOut;
                ant.TabuSet(iCurrentCity, 1);
                maxTime = Summ(Cities.Count);
                for (int i = 0; i < Cities.Count; i++)
                {
                    curTime = Summ(i);
                    double dCurrentDistance = double.MaxValue;
                    double dMinimum = double.MaxValue;
                    int iMinimum = -1;
                    DataCity CityI = Cities[iCurrentCity];
                    //CityI.Index = i;
                    for (int j = 0; j < Cities.Count; j++)
                    {
                        //if (ant.TabuGet(j) == 0 && j != i && (ccRoute.Count == 0 || PointIsNotFound(ccRoute, j)))
                        if (ant.TabuGet(j) != 1)
                        {
                            DataCity CityJ = Cities[j];
                            // CityJ.Index = j;

                            dCurrentDistance = Cities.Distance[iCurrentCity, j];
                            // Math.Sqrt((Math.Pow(Math.Abs(CityI.X - CityJ.X), 2) +
                            //           (Math.Pow(Math.Abs(CityI.Y - CityJ.Y), 2))));
                            if (dCurrentDistance < dMinimum)
                            {
                                dMinimum = dCurrentDistance;
                                iMinimum = j;
                            }
                        }
                    }
                    if (iMinimum != -1)
                    {
                        iCurrentCity = iMinimum;
                        DataCity city_temp = Cities[iCurrentCity];
                        city_temp.Index = iCurrentCity;
                        ccRoute.Add(city_temp);
                        ant.TabuSet(iCurrentCity, 1);
                        TimeSpan tsElapsed = (DateTime.Now - dtStart);
                        strOut = String.Format("{0:00000} ", i+1) + String.Format(" Time is {0:00}s {1:000}ms", tsElapsed.Seconds, tsElapsed.Milliseconds) + "\n"; // Дописать!!!!!
                        listrTime.Add(strOut);
                    }
                }
                strOut = "\nОбщее время:\n" + String.Format("{0:t}", DateTime.Now - dtStart); // Дописать!!!!!
                listrTime.Add(strOut);
                tmrTimer.Stop();
                BestPath = ccRoute;
                OnFinally(EventArgs.Empty);
            }
            catch (ThreadAbortException taex)
            { listrTime.Add(taex.Message); }
            catch(Exception ex)
            {listrTime.Add(ex.Message);}
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
        //private bool PointIsNotFound(DataCitiesCollection route, int point)
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
        public event EventHandler<NearestNeighbourChangesEventArgs> eventProgressChanged;
        public event EventHandler<EventArgs> eventFinally;

        // Уведомляет подписанные на событие объекты
        protected virtual void OnProgressChanged(NearestNeighbourChangesEventArgs e)              
        {
            EventHandler<NearestNeighbourChangesEventArgs> tmp = eventProgressChanged;

            if (tmp != null)
                tmp(this, e);
        }
        protected virtual void OnFinally(EventArgs e)                                   
        {
            //OnProgressChanged(NearestNeighbourChangesEventArgs(100, true));
            curTime = maxTime;
            tmrTimer_Elapsed(this, EventArgs.Empty);
            EventHandler<EventArgs> tmp = eventFinally;

            if (tmp != null)
            {
                bIsThreadAlive = false;
                tmp(this, e);
               // t.Abort();
               // t = null;
            }
        }

        // Метод, вызывающий событие
        protected virtual void tmrTimer_Elapsed(object sender, EventArgs e)             
        {
            //wh.WaitOne();

            int iPercent = 0;
            //Подсчитываем процент прогресса
            if (maxTime > 0)
            {
                decimal dPercent = (decimal)curTime * 100 / maxTime;
                iPercent = (int)dPercent;  //только целые значения | а какие ещё могут быть в int?!
            }
            else 
            { }
            NearestNeighbourChangesEventArgs eee = new NearestNeighbourChangesEventArgs(iPercent, false);

            OnProgressChanged(eee); // да, еее - это по-мудацки!

            //wh.Set();
        }
        #endregion


    }

    //------------------------------------------------------------------
    //     Аргументы событий изменения в алгоритме ближайшего соседа
    //------------------------------------------------------------------
    /// <summary>
    /// Аргументы событий изменения в алгоритме ближайшего соседа
    /// </summary>
    internal class NearestNeighbourChangesEventArgs : EventArgs
    {
        private readonly double dPercent;
        private bool bCanContinue;

        public NearestNeighbourChangesEventArgs(double percent, bool canContinue)
        {
            dPercent = percent;
            bCanContinue = canContinue;
        }

        /// <summary>
        /// Процент выполнения алгоритма
        /// </summary>
        public double Percent
        { get { return dPercent; } }

        /// <summary>
        /// Позволяет прододжить выполнение
        /// </summary>
        public bool CanContinue
        {
            set
            {
                bCanContinue = value;
            }
            get
            {
                return bCanContinue;
            }
        }
    };
}
