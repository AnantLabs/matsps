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
            _iMutationPercent = 5;
            //AllowAllMutations = true;
        }
        public GAParameters(int CCount) :this()
        {
            CitiesCount = CCount;
        }

        private int _iMutationPercent;
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

        #region Свойства мутаций
        /*
        public bool AllowAllMutations
        {
            set;
            get;
        }
        */
        
        // Процент мутации особей в поколении
        public int MutationPercent
        {
            set
            {
                if (value < 0 || value > 100)
                {
                    _iMutationPercent = 5;
                    throw new Exception("Неверное значение мутации. Будет выставлено значение по умолчанию(5).");
                }
                else
                {
                    _iMutationPercent = value;
                }
            }
            get
            {
                return _iMutationPercent;
            }
        }

        #endregion Свойства мутаций
    }
}
