using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using matsps.CommonData;
using matsps.Parameters;

namespace matsps.GeneticAlgorithm
{
    class Generation
    {

        #region Конструкторы

        /// <summary>
        /// Создать новое поколение
        /// </summary>
        /// <param name="gap">Параметры генетического алгоритма</param>
        /// <param name="cities">Города для расчёта</param>
        public Generation(GAParameters gap, CitiesCollection cities)
        {
            _gap = gap;
            _cities = cities;
            _liAgents = new List<Agent>();
            for (int i = 0; i < _gap.Population; i++)
            {
                _liAgents.Add(new Agent(_cities));
            }
        }

        #endregion Конструкторы

        #region Локальные переменные 
        
        /// <summary>
        /// Рандом
        /// </summary>
        private Random _rnd = new Random();

        private Mutations _mutations = new Mutations();

        private Crossbreeding _crossbreeding = new Crossbreeding();

        /// <summary>
        /// Параметры генетического алгоритма
        /// </summary>
        private GAParameters _gap;
     
        /// <summary>
        /// Лист агентов(поколение)
        /// </summary>
        private List<Agent> _liAgents;
        /// <summary>
        /// Коллекция городов для расчёта
        /// </summary>
        CitiesCollection _cities;

        #endregion Локальные переменные

        #region Функции

        ///// <summary>
        /////  Создать рандомно заполненного агента
        ///// </summary>
        ///// <returns>Агент со случайным маршрутом</returns>
//        private Agent NewAgent()
//        {
//            return new Agent(_cities);
//       }


        public void PerformCrossbreeding()
        {
            if (_liAgents.Count > _gap.SurviversCount)
            {
                this.PerformSelection();
            }
            List<Agent> _liAgentsTemp = new List<Agent>(_liAgents);

            while (_liAgents.Count < _gap.Population)
            {
                int parent1, parent2;
                //Random rnd = new Random();
                
                //первый родитель
                parent1 = _rnd.Next(_liAgentsTemp.Count);
                Agent tmpag1 = new Agent(_liAgentsTemp[parent1].Route.Cities); ;
               // _liAgentsTemp.Remove(tmpag1);
                
                //второй родитель
                parent2 = _rnd.Next(_liAgentsTemp.Count);
                Agent tmpag2 = new Agent(_liAgentsTemp[parent2].Route.Cities);
               // _liAgentsTemp.Remove(tmpag2);

                _crossbreeding.Perform(ref tmpag1, ref tmpag2);

                
                //// Точка разрыва в маршруте
                //int break_point;
                //break_point = _rnd.Next(_gap.CitiesCount);

                //int i = 0;
                //Route tmpr1 = new Route("генетический алгоритм");
                //Route tmpr2 = new Route("генетический алгоритм");
                //// Копируем точки из родителей в детей до точки разрыва
                //while (i < break_point)
                //{
                //    tmpr1.Cities.Add(tmpag1.Route.Cities[i]);
                //    tmpr2.Cities.Add(tmpag2.Route.Cities[i]);
                //    i++;
                //}

                //// 
                //while (i < _gap.CitiesCount)
                //{
                //    tmpr1.Cities.Add(tmpag2.Route.Cities[i]);
                //    tmpr2.Cities.Add(tmpag1.Route.Cities[i]);
                //}
                //tmpag1.Route = tmpr1;
                //tmpag2.Route = tmpr2;
            
                _liAgents.Add(tmpag1);
                _liAgents.Add(tmpag2);
            }
            

        }

        public void PerformMutation()
        {
            _mutations.SetMutationsProbability(
                _gap.CitySwitchMutationProbability,
                _gap.IsolatedChainMutationProbability,
                _gap.NewAgentMutationProbability);

            for (int i = 0; i < _liAgents.Count; i++)
            {
                double dMutation = _rnd.NextDouble();

                if (dMutation < _gap.MutationPercent)
                {
                    Agent agent = _liAgents[i];
                    _mutations.Perform(ref agent);
                }
            }
        }

        /// <summary>
        /// Удаляем худших агентов из листа _liAgents 
        /// </summary>
        public void PerformSelection()
        {
            if (_liAgents.Count > _gap.SurviversCount)
            {
                // Вот для этой строчки и нужен IComparable
                _liAgents.Sort();
                _liAgents.RemoveRange(_gap.SurviversCount, _liAgents.Count - _gap.SurviversCount);
            }
           // while (_liAgents.Count > _gap.SurviversCount)
           // {
            //    _liAgents.RemoveAt(_liAgents.Count - 1);
            //}
        }


        /// <summary>
        /// Получить лучший маршрут
        /// </summary>
        /// <returns>Лучший маршрут в текущем поколении</returns>
        public Agent GetBest()
        {
            _liAgents.Sort();
            return _liAgents[0];
        }


        #endregion Функции
    }
    
}
