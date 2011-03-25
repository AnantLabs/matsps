using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.Parameters
{
    /// <summary>
    ///  Параметры генетического алгоритма
    /// </summary>
    class GAParameters: IParameters
    {
        #region Конструторы и Данные
        public GAParameters()
        {
            MaxDistance = 100;
            GenerationsCount = 1000;
            Population = 100;
        }
        public GAParameters(int CCount) :this()
        {
            CitiesCount = CCount;
        }

        #endregion

        #region Свойства
        /// <summary>
        ///  Максимальное расстояние между городами
        /// </summary>
        public int MaxDistance                  
        {
            set;
            get;
        }

        /// <summary>
        /// Количество создаваемых поколений
        /// </summary>
        public int GenerationsCount                    
        {
            set;
            get;
        }

        /// <summary>
        /// Количество городов
        /// </summary>
        public int CitiesCount
        {
            set;
            get;
        }

        /// <summary>
        /// Популяция(количество Агентов)
        /// </summary>
        public int Population
        {
            set;
            get;
        }
        #endregion
    }
}
