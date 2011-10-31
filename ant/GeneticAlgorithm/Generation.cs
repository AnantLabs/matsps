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

        #region Конструкторы и данные

        public Generation(GAParameters gap, CitiesCollection cities)
        {
            _gap = gap;
            _cities = cities;
            _liAgents = new List<Agent>();
            for (int i = 0; i < gap.Population/2; i++)
            {
                _liAgents.Add(this.NewAgent());
            }
        }

        private GAParameters _gap;
        private Random _rnd = new Random();
        private List<Agent> _liAgents;
        CitiesCollection _cities;

        #endregion


        /// <summary>
        ///  Создать рандомно заполненного агента
        /// </summary>
        /// <returns></returns>
        private Agent NewAgent()
        {
            Agent tmpag = new Agent();            
            
            CitiesCollection tmpCC = new CitiesCollection(_gap.CitiesCount);

            int firstCity;
            firstCity = _rnd.Next(_cities.Count);
            City tmpc = (City)_cities[firstCity].Clone();
            //tmpc.Index = 0;
            
            tmpag.TabooInit(_cities.Count);
            tmpag.TabooSet(firstCity, true);
            tmpCC.Add(tmpc);//+ использовать просто Route// CitiesCollection
            for (int i = 1; i < _cities.Count; i++)
            {
                int nextCityIndex;
                
                do
                { nextCityIndex = _rnd.Next(_cities.Count); }
                while(  tmpag.TabooGet(nextCityIndex) == true);
                tmpag.TabooSet(nextCityIndex,true);
                
                tmpc = (City)_cities[nextCityIndex].Clone();
                tmpc.Index = nextCityIndex;
                tmpCC.Add(tmpc);
            }
            //tmpRoute.Drawing2.Color = System.Drawing.Color.Indigo; ???
            Route tmpRoute = new Route(tmpCC, "генетический алгоритм");
            tmpag.Route = tmpRoute;
            return tmpag;
        }

        public void DoChildren()
        {
            try
            {
                if (_liAgents.Count > _gap.Population / 2)
                {
                    this.RemoveWorst();
                }
                List<Agent> _liAgentsTemp = new List<Agent>(_liAgents);
                //_liAgentsTemp = _liAgents.;
                //for(int i = 0; i < _liAgents.Count/2; i++)
                //{
                do
                {
                    int parent1, parent2;
                    //Random rnd = new Random();
                    
                    //первый родитель
                    parent1 = _rnd.Next(_liAgentsTemp.Count);
                    Agent tmpag1 = _liAgentsTemp[parent1];
                    _liAgentsTemp.Remove(tmpag1);
                    
                    //второй родитель
                    parent2 = _rnd.Next(_liAgents.Count);
                    Agent tmpag2 = _liAgentsTemp[parent2];
                    _liAgentsTemp.Remove(tmpag2);
                    
                    // Точка разрыва в маршруте
                    int break_point;
                    break_point = _rnd.Next(_gap.CitiesCount);

                    int i = 0;
                    Route tmpr1 = new Route("генетический алгоритм");
                    Route tmpr2 = new Route("генетический алгоритм");
                    while (i < break_point)
                    {
                        tmpr1.Cities.Add(tmpag1.Route.Cities[i]);
                        tmpr2.Cities.Add(tmpag2.Route.Cities[i]);
                    }
                    while (i < _gap.CitiesCount)
                    {
                        tmpr1.Cities.Add(tmpag2.Route.Cities[i]);
                        tmpr2.Cities.Add(tmpag1.Route.Cities[i]);
                    }
                    tmpag1.Route = tmpr1;
                    tmpag2.Route = tmpr2;

                    _liAgents.Add(tmpag1);
                    _liAgents.Add(tmpag2);
                }
                while(_liAgentsTemp.Count > 0);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Удаляем худших агентов из листа _liAgents 
        /// </summary>
        public void RemoveWorst()
        {
            try
            {
                if (_liAgents.Count > _gap.CitiesCount / 2)
                {
                    // Вот для этой строчки и нужен IComparable
                    _liAgents.Sort();
                }
                while (_liAgents.Count > _gap.CitiesCount / 2)
                {
                    _liAgents.RemoveAt(_liAgents.Count - 1);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }

        }

        /// <summary>
        /// Выдает массив всех агентов в поколении
        /// </summary>
        /// <returns></returns>
        public Agent[] GetAllAgents()   
        {
            return _liAgents.ToArray();
        }
   
 }
    
}
