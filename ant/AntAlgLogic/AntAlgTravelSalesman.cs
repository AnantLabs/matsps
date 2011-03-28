using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;     // потоки

using matsps.AntAlgData;           // данные для алгоритма муравья
using matsps.CommonData;           // совместные данные
using matsps.Parameters;           // параметры алгоритмов

namespace matsps.AntAlgLogic
{
    /// <summary>
    /// Алгоритм расчета задачи коммивояжера с помощью муравьиной колонии
    /// </summary>
    class AntAlgTravelSalesman
    {
        #region Конструкторы и Данные
        public AntAlgTravelSalesman()                                               
        {

        }
        public AntAlgTravelSalesman(CitiesCollection cities)
            : this()       
        {
            Cities = cities;
        }
        public AntAlgTravelSalesman(CitiesCollection cities, AntParameters param)
            :this(cities)                                                           
        {
            SetParameters(param);
            maxTime = _parameters.MaxTime;                                 //установка максимального ко-ва проходов
            tmrTimer = new System.Timers.Timer(10);                                      //инициализация таймера
            tmrTimer.Elapsed += new ElapsedEventHandler(tmrTimer_Elapsed); //подписка на событие таймера "Elapsed"
        }

        private double best;
        private int bestIndex;

        private int maxTime;            //максимум проходов
        private int curTime;            //счетчик проходов
        private System.Timers.Timer tmrTimer;         //таймер

        Random rnd = new Random();

        /// <summary>
        /// Поток вычислений
        /// </summary>
        private Thread t = null;
        /// <summary>
        /// Событие для остановки потока вычислений
        /// </summary>
        static EventWaitHandle wh = new AutoResetEvent(false);
        /// <summary>
        /// Показывает, запущен ли сейчас поток
        /// </summary>
        private bool bIsThreadAlive = false;


        /// <summary>
        /// Массив значений феромонов в каждом городе
        /// </summary>
        private double[,] Pheromone;
        #endregion


        #region Свойства
        /// <summary>
        /// Возвращает или задает Коллекцию муравьев
        /// </summary>
        public AntAlgDataAntsCollection Ants                            
        {
            set;
            get;
        }

        /// <summary>
        /// Возвращает или задает Коллекцию городов
        /// </summary>
        public CitiesCollection Cities                        
        {
            set;
            get;
        }


        private AntParameters _parameters;
        #endregion


        #region Свойства результатов расчета
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
        public CitiesCollection BestPath                          
        {
            get
            {
                CitiesCollection cities = new CitiesCollection();
                cities.MaxDistance = Cities.MaxDistance;
                for (int i = 0; i < Cities.Count; i++)
                {
                    City newCity = new City(Ants[bestIndex].PathGet(i).X, Ants[bestIndex].PathGet(i).Y);
                    newCity.Index = Ants[bestIndex].PathGet(i).Index;
                    cities.Add(newCity);
                }
                //Route route = new Route(cities,"муравьиной колонии");

                return cities;
            }
        }
        #endregion

        #region Методы

        /// <summary>
        /// Установить параметры алгоритма
        /// </summary>
        public void SetParameters(AntParameters param)               
        {
            if (Cities != null)
            {
                // Создаем Муравьев
                Ants = new AntAlgDataAntsCollection(param.MaxCities);
                Ants.InitAnts(Cities);
            }

            _parameters = param;
            best = _parameters.MaxTour;

            if (Cities != null)
            {
                Pheromone = new double[ Cities.Count, Cities.Count];

                for (int from = 0; from < Cities.Count; from++)
                    for (int to = 0; to < Cities.Count; to++)
                        Pheromone[from, to] = _parameters.InitPheromone;
            }
        }

        /// <summary>
        /// Перезапустить муравьев для повторного расчета
        /// </summary>
        private void RestartAnts()                                          
        {
            int ant, i, to = 0;

            for (ant = 0; ant < Ants.Count; ant++)
            {
                if (Ants[ant].TourLength < best)
                {
                    best = Ants[ant].TourLength;
                    bestIndex = ant;
                }

                Ants[ant].NextCity = null;
                Ants[ant].TourLength = 0.0;

                for (i = 0; i < Cities.Count; i++)
                {
                    Ants[ant].TabuSet(i, 0);
                    Ants[ant].PathSet(i, null);
                }

                if (to == Cities.Count)
                    to = 0;
                Ants[ant].CurrentCity = Cities[to++];
                Ants[ant].PathIndex = 1;
                Ants[ant].PathSet(0, Ants[ant].CurrentCity);

                Ants[ant].TabuSet(Ants[ant].CurrentCity.Index, 1);
            }
        }

        /// <summary>
        /// Расчет вероятности движения муравья
        /// </summary>
        /// <param name="from">Из этого города</param>
        /// <param name="to">В этот город</param>
        /// <returns>Вероятность движения</returns>
        private double AntProduct(int from, int to)                         
        {
            double val = Math.Pow( Pheromone[from, to],_parameters.ALPHA) *
                    Math.Pow(((double)1.0 / (double)Cities.Distance[from, to]), _parameters.BETA);
            return val;
        }

        private double GetSRand()                                           
        {
            return GetSRand(1);
        }
        private double GetSRand(int x)                                      
        {
            return (double)rnd.Next() / (double)Int32.MaxValue;
        }

        /// <summary>
        /// Определяет, какую грань выберет муравей в соотвествии со списком ТАБУ.
        /// </summary>
        /// <param name="ant">Индекс муравья в массиве</param>
        /// <returns>Номер грани</returns>
        private int SelectNewCity(int ant)                                  
        {
            int from, to;
            double denom = 0.0;

            // Выбрать следующий город
            from = Ants[ant].CurrentCity.Index;

            // Расчет знаменателя
            for (to = 0; to < Cities.Count; to++)
            {
                if (Ants[ant].TabuGet(to) == 0)
                {
                    if( Cities.Distance[from, to] != 0 ) 
                        denom += AntProduct(from, to);
                }
            }

            if (double.IsInfinity(denom))
            {
                ;
            }
            if (denom == 0.0)
            {
                throw new Exception("denom == 0");
            }

            do
            {
                double p;

                to++;
                if (to >= Cities.Count)
                    to = 0;

                if (Ants[ant].TabuGet(to) == 0)
                {
                    p = AntProduct(from, to) / denom;
                    double r = GetSRand();
                    if ( r < p )
                        break;
                }
            } while (true);

            return to;
        }

        /// <summary>
        /// Рассчитывает  движение муравьев по графу
        /// </summary>
        /// <returns></returns>
        private int SimulateAnts()                                          
        {
            int k;
            int moving = 0;

            for (k = 0; k < Ants.Count; k++)
            {
                // Убедиться, что у муравья есть куда идти
                if (Ants[k].PathIndex < Cities.Count)
                {
                    Ants[k].NextCity = Cities[SelectNewCity(k)];
                    Ants[k].TabuSet(Ants[k].NextCity.Index, 1);
                    Ants[k].PathSet(Ants[k].PathIndex++, Ants[k].NextCity);
                    Ants[k].TourLength += Cities.Distance[ Ants[k].CurrentCity.Index , Ants[k].NextCity.Index ];

                    // Обработка окончания путешествия (из последнего города в первый)
                    if (Ants[k].PathIndex == Cities.Count)
                    {
                        Ants[k].TourLength +=
                            Cities.Distance[ Ants[k].PathGet(Cities.Count - 1).Index, Ants[k].PathGet(0).Index];
                    }

                    Ants[k].CurrentCity = Ants[k].NextCity;
                    moving++;
                }
            }

            return moving;
        }

        /// <summary>
        /// Испарение и размещение нового фермента
        /// </summary>
        private void UpdateTrails()                                         
        {
            int from, to, i, ant;

            // Испарение фермента
            for (from = 0; from < Cities.Count; from++)
            {
                for (to = 0; to < Cities.Count; to++)
                {
                    if (from != to)
                    {
                        Pheromone[from , to] *= (1.0 - _parameters.RHO);

                        if (Pheromone[from ,to] < 0.0)
                            Pheromone[from , to] = _parameters.InitPheromone;
                    }
                }
            }

            // Нанесение нового фермента

            // Для пути каждого муравья
            for (ant = 0; ant < Cities.Count; ant++)
            {
                // Обновляем каждый шаг пути
                for (i = 0; i < Cities.Count; i++)
                {
                    if (i < Cities.Count - 1)
                    {
                        from = Ants[ant].PathGet(i).Index;
                        to = Ants[ant].PathGet(i + 1).Index;
                    }
                    else
                    {
                        from = Ants[ant].PathGet(i).Index;
                        to = Ants[ant].PathGet(0).Index;
                    }

                    Pheromone[from, to] += (_parameters.QVAL / Ants[ant].TourLength);
                    Pheromone[to, from] = Pheromone[from, to];
                }
            }

            for (from = 0; from < Cities.Count; from++)
            {
                for (to = 0; to < Cities.Count; to++)
                {
                    Pheromone[from, to] *= _parameters.RHO;
                }
            }
        }

        /// <summary>
        /// Произвести один расчет
        /// </summary>
        /// <returns>Список результара расчета</returns>
        public void Calculate()                                             
        {
            listrTime = new List<string>();

            if(t == null)
            {
                    t = new Thread(BackgroundCalculate);
                    t.IsBackground = true;
                    t.Start();
                    bIsThreadAlive = true;
            }
            //t.Join();            
        }

        /// <summary>
        /// Возобновить выполнения вычислений
        /// </summary>
        public void CalculateContinue()                                     
        {
            wh.Set();
        }

        private void BackgroundCalculate()                                  
        {
            try
            {
                int i = 1;
                curTime = 0; //обнуление счетчика проходов
                tmrTimer.Start(); //запуск таймера
                while (curTime++ < _parameters.MaxTime)
                {
                    try
                    {
                        if (SimulateAnts() == 0)
                        {
                            UpdateTrails();

                            if (curTime != Cities.Count)
                                RestartAnts();

                            string strOut = String.Format("{0:00000} ", i) + String.Format(" Time is {0:000}", curTime) + String.Format(" {0:000.00}\n", best);
                            i++;
                            listrTime.Add(strOut);
                        }
                    }
                    catch (Exception ex)
                    {
                        listrTime.Add(ex.Message);
                    }
                }
                tmrTimer.Stop();           //остановка таймера

                //AntAlgChangesEventArgs e = new AntAlgChangesEventArgs(101, false); //посылаем значение 101%(алгоритм завершен)
                //OnProgressChanged(e);
                listrTime.Add(string.Format("best tour {0:000.00\n}", best));


                // Вычисление завершено
                OnFinally(new EventArgs());
            }
            catch (ThreadAbortException ex)
            {
            }
        }
        #endregion

        #region Событие
        /// <summary>
        /// Генерируется при изменении прогресса выполнения алгоритма
        /// </summary>
        //public event ProgressChanged eventProgressChanged;
        // Член-события, делегат
        public event EventHandler<AntAlgChangesEventArgs> eventProgressChanged;
        public event EventHandler<EventArgs> eventFinally;

        // Уведомляет подписанные на событие объекты
        protected virtual void OnProgressChanged(AntAlgChangesEventArgs e)              
        {
            EventHandler<AntAlgChangesEventArgs> tmp = eventProgressChanged;

            if (tmp != null)
                tmp(this, e);
        }
        protected virtual void OnFinally(EventArgs e)                                   
        {
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
        protected virtual void tmrTimer_Elapsed(object sender, EventArgs e)             
        {
            //wh.WaitOne();

            //Подсчитываем процент прогресса
            int iPersent = curTime * 100 / maxTime; //только целые значения
            AntAlgChangesEventArgs eee = new AntAlgChangesEventArgs(iPersent, false);

            OnProgressChanged(eee); // да, еее - это по-мудацки!

            //wh.Set();
        }
        #endregion
    }


    //          Аргументы событий изменения в алгоритме муравья   
    /// <summary>
    /// Аргументы событий изменения в алгоритме муравья
    /// </summary>
    internal class AntAlgChangesEventArgs : EventArgs                                   
    {
        private readonly double dPercent;
        private bool bCanContinue;

        public AntAlgChangesEventArgs(double percent, bool canContinue)         
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
