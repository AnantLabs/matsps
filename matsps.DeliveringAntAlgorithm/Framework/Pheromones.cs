using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Феромоны. Строка - машина. столбец - точка
    /// </summary>
    public class Pheromones
    {
        #region Свойства
        /// <summary>
        /// Матрица феромонов
        /// </summary>
        public Double[,] PheromonesMatrix
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
        /// <summary>
        /// Создает экземпляр феромонов с заданными параметрами
        /// </summary>
        /// <param name="carsCount">Количество машин</param>
        /// <param name="clientsCount">Количество клиентов</param>
        public Pheromones(int carsCount,int clientsCount)
        {
            if (carsCount <= 0 || clientsCount <= 0)
            { 
                throw new ArgumentOutOfRangeException();
            }
            PheromonesMatrix = new Double[carsCount, clientsCount];
        }
        #endregion

        #region Открытые методы
        /// <summary>
        /// Испарение феромона
        /// </summary>
        public void Evaporation()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
