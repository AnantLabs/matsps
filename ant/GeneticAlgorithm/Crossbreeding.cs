using System;
using System.Collections.Generic;
using System.Text;

using matsps.CommonData;

namespace matsps.GeneticAlgorithm
{
    class Crossbreeding
    {
        #region Constructors

        public Crossbreeding()
        {
            
        }

        #endregion Constructors

        #region Variables
        
        Random _rnd = new Random();
        
        #endregion Variables

        #region Functions

        /// <summary>
        /// Perform a crossbreeding between two Agents
        /// </summary>
        /// <param name="agent1">Reference to first Agent(will be modified, recommended to send a copy)</param>
        /// <param name="agent2">Reference to second Agent(will be modified, recommended to send a copy</param>
        public void Perform(ref Agent agent1, ref Agent agent2)
        {
            if (agent1.Route.Count == agent2.Route.Count)
            {
                int iCount = agent1.Route.Count;
                int iBreakPoint = _rnd.Next(agent1.Route.Count);

                CitiesCollection tmpCC1 = new CitiesCollection();
                CitiesCollection tmpCC2 = new CitiesCollection();
                for (int i = 0; i < iBreakPoint; i++)
                {
                    tmpCC1.Add(agent1.Route.Cities[i]);
                    tmpCC2.Add(agent1.Route.Cities[i]);
                }

                for (int i = 0; i < iBreakPoint; i++)
                {
                    agent1.Route.Cities.Cities.Remove(tmpCC1[i]);
                    agent2.Route.Cities.Cities.Remove(tmpCC2[i]);
                }

                for (int i = 0; i < iCount - iBreakPoint; i++)
                {
                    tmpCC1.Add(agent2.Route.Cities[i]);
                    tmpCC2.Add(agent1.Route.Cities[i]);
                }
                agent1.Route = new Route(tmpCC1, "генетический алгоритм");
                agent2.Route = new Route(tmpCC2, "генетический алгоритм");
            }
            else
            {
                throw new Exception("Число городов в агентах не совпадает!");
            }
        }

        #endregion Functions
    }
}
