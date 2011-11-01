using System;
using System.Collections.Generic;
using System.Text;

using matsps.CommonData;

namespace matsps.GeneticAlgorithm
{
    class Mutations
    {
        #region Constuctors

        public Mutations()
        {

        }

        #endregion Constuctors

        #region Variables

        private Random _rnd = new Random(); 

        #endregion Variables

        #region Functions

        public void CitySwitchMutation(ref Agent agent)
        {
            // Getting Cities collection
            CitiesCollection tmpCC = new CitiesCollection(agent.Route.Cities);

            // Choosing random cities indexes
            int iCity1, iCity2;

            iCity1 = _rnd.Next(tmpCC.Count);
            iCity2 = _rnd.Next(tmpCC.Count);

            // Writing cities in local variables
            City tmpc1 = tmpCC[iCity1];
            City tmpc2 = tmpCC[iCity2];

            // Switching cities
            tmpCC[iCity1] = tmpc2;
            tmpCC[iCity2] = tmpc1;

            // Adding temporary collection to the Agent
            agent.Route.Cities = tmpCC;

        }

        public void IsolatedChainMutation(ref Agent agent)
        {
            // Mutation takes place only if it is more than 4 cities in collection
            if (agent.Route.Cities.Count > 4)
            {
                // Getting Cities collection
                CitiesCollection tmpCC = new CitiesCollection(agent.Route.Cities);

                // Choosing random cities indexes
                int iCity1, iCity2;

                iCity1 = _rnd.Next(tmpCC.Count - 4);
                iCity2 = _rnd.Next(iCity1 + 4, tmpCC.Count);

            }
            
        }

        #endregion Functions
    }
}
