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
        #endregion
    }
}
