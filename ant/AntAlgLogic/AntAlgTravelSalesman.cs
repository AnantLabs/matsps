﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ant.AntAlgData;

namespace ant.AntAlgLogic
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
        public AntAlgTravelSalesman(AntAlgDataCitiesCollection cities):this()       
        {
            Cities = cities;
        }
        public AntAlgTravelSalesman(AntAlgDataCitiesCollection cities, AntAlgDataParameters param)
            :this(cities)                                                           
        {
            SetParameters(param);
        }

        private double best;
        private int bestIndex;
        Random rnd = new Random();

        /// <summary>
        /// Массив значений феромонов в каждом городе
        /// </summary>
        private double[,] Pheromone;
        #endregion



        #region Свойства
        /// <summary>
        /// Коллекция муравьев
        /// </summary>
        public AntAlgDataAntsCollection Ants                            
        {
            set;
            get;
        }

        /// <summary>
        /// Коллекция городов
        /// </summary>
        public AntAlgDataCitiesCollection Cities                        
        {
            set;
            get;
        }


        private AntAlgDataParameters _parameters;
        #endregion



        #region Методы

        /// <summary>
        /// Установить параметры алгоритма
        /// </summary>
        public void SetParameters(AntAlgDataParameters param)               
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


        public List<string> Calculate()
        {
            int curTime = 0;
            List<string> listrTime = new List<string>();

            int i = 1;
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

            listrTime.Add(string.Format( "best tour {0:000.00\n}", best) );

            return listrTime;
        }
        #endregion
    }
}
