using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Феромоны
    /// </summary>
    public class Pheromones
    {
        #region Свойства
        /// <summary>
        /// Матрица феромонов
        /// </summary>
        public Double[,] Pherpmones
        {
            set;
            get;
        }
        /// <summary>
        /// Испаряемое значение
        /// </summary>
        public Double EvaporationValue
        {
            set;
            get;
        }
        /// <summary>
        /// Начальное значение феромонов
        /// </summary>
        public Double StartValue
        { 
            set;
            get;
        }
        #endregion

        #region Конструкторы
        public Pheromones(int carsCount,int clientsCount)
        {
            if (carsCount <= 0 || clientsCount <= 0)
            { 
                throw new ArgumentOutOfRangeException();
            }
            Pherpmones = new Double[carsCount, clientsCount];
        }
        #endregion

        /// <summary>
        /// Испарение феромона
        /// </summary>
        public void Evaporation()
        {
            throw new NotImplementedException();
        }
        
    }
}
