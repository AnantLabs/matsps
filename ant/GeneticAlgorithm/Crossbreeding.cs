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
        
        private static Random _rnd = new Random();
        
        #endregion Variables

        #region Functions

        /// <summary>
        /// Perform a crossbreeding between two Agents
        /// </summary>
        /// <param name="agent1">Reference to first Agent(will be modified, recommended to send a copy)</param>
        /// <param name="agent2">Reference to second Agent(will be modified, recommended to send a copy</param>
        public void Perform(ref Agent agent1, ref Agent agent2)
        {
            this.OnePointCrossbreeding(ref agent1, ref agent2);
            //this.CyclicCrossbreeding(ref agent1, ref agent2);
        }

        /// <summary>
        /// Crossbreeding with one break point in route
        /// </summary>
        /// <param name="agent1">First agent</param>
        /// <param name="agent2">Second agent</param>
        private void OnePointCrossbreeding(ref Agent agent1, ref Agent agent2)
        {
            try
            {
                if (agent1.Route.Count == agent2.Route.Count)
                {
                    int iCount = agent1.Route.Count;
                    int iBreakPoint = _rnd.Next(agent1.Route.Count);

                    CitiesCollection tmpCC1 = new CitiesCollection();
                    CitiesCollection tmpCC2 = new CitiesCollection();

                    Agent agentCopy1 = new Agent(agent1.Route.Cities);
                    Agent agentCopy2 = new Agent(agent2.Route.Cities);

                    for (int i = 0; i < iBreakPoint; i++)
                    {
                        // Adding first city to collection and deleting it from Agent
                        tmpCC1.Add(agent1.Route.Cities[i]);
                        agent2.Route.Cities.Cities.Remove(agent1.Route.Cities[i]);
                        //agent1.Route.Cities.Cities.RemoveAt(0);

                        tmpCC2.Add(agentCopy2.Route.Cities[i]);
                        agentCopy1.Route.Cities.Cities.Remove(agentCopy2.Route.Cities[i]);
                        //agentCopy2.Route.Cities.Cities.RemoveAt(0);
                    }
                    //for (int i = 0; i < iBreakPoint; i++)
                    //{
                    //    agent1.Route.Cities.Cities.Remove(tmpCC1[i]);
                    //    agent2.Route.Cities.Cities.Remove(tmpCC2[i]);
                    //}

                    for (int i = 0; i < iCount - iBreakPoint; i++)
                    {
                        tmpCC1.Add(agent2.Route.Cities[i]);
                        tmpCC2.Add(agentCopy1.Route.Cities[i]);
                    }
                    agent1.Route = new Route(tmpCC1, "генетический алгоритм");
                    agent2.Route = new Route(tmpCC2, "генетический алгоритм");
                }
                else
                {
                    throw new Exception("Число городов в агентах не совпадает!");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message + ex.StackTrace);
                throw;
            }
        }

        /// <summary>
        /// Cyclic crossbreeding
        /// </summary>
        /// <param name="agent1">First agent</param>
        /// <param name="agent2">Second agent</param>
        private void CyclicCrossbreeding(ref Agent agent1, ref Agent agent2)
        {
            if (agent1.Route.Count == agent2.Route.Count)
            {
                int iCount = agent1.Route.Count;
                int iBreakPoint = _rnd.Next(agent1.Route.Count);

                CitiesCollection tmpCC1 = new CitiesCollection();
                CitiesCollection tmpCC2 = new CitiesCollection();

                Agent agentCopy1 = new Agent(agent1.Route.Cities);
                Agent agentCopy2 = new Agent(agent2.Route.Cities);

                while (agent1.Route.Count > 1)
                {
                    // Копируем города из агентов(первый из первого, последный из второго)
                    City tmpc1 = agent1.Route.Cities[0];
                    tmpCC1.Add(tmpc1);
                    agent1.Route.Cities.Cities.Remove(tmpc1);
                    agent2.Route.Cities.Cities.Remove(tmpc1);

                    City tmpc2 = agent2.Route.Cities[agent2.Route.Cities.Count - 1];
                    tmpCC1.Add(tmpc2);
                    agent1.Route.Cities.Cities.Remove(tmpc2);
                    agent2.Route.Cities.Cities.Remove(tmpc2);

                    // Копируем города из копий Агентов
                    tmpc1 = agentCopy2.Route.Cities[0];
                    tmpCC2.Add(tmpc1);
                    agentCopy1.Route.Cities.Cities.Remove(tmpc1);
                    agentCopy2.Route.Cities.Cities.Remove(tmpc1);

                    tmpc2 = agentCopy1.Route.Cities[agentCopy1.Route.Cities.Count];
                    tmpCC2.Add(tmpc2);
                    agentCopy1.Route.Cities.Cities.Remove(tmpc2);
                    agentCopy2.Route.Cities.Cities.Remove(tmpc2);
                    
                }
                // Если количество городов нечётное, один город остаётся в Агенте
                if (agent1.Route.Count == 1)
                {
                    tmpCC1.Add(agent1.Route.Cities[0]);
                    tmpCC2.Add(agent1.Route.Cities[0]);
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
