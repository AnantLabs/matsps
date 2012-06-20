using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Параметры алгоритма
    /// </summary>
    public class Parameters
    {
        #region Свойства
        /// <summary>
        /// Возвращает значение параметром по умолчанию
        /// </summary>
        public static Parameters Default
        {
            get
            {
                Parameters param = new Parameters();
                param.AntCount = 50;
                param.IterationCount = 10;
                param.EvaporationValue = 0.1;
                return param;
            }
        }

        /// <summary>
        /// Количество муравьев в алгоритме (количество вариантов доставки)
        /// </summary>
        public int AntCount { set; get; }

        /// <summary>
        /// Количество итераций алгоритма
        /// </summary>
        public int IterationCount { set; get; }

        /// <summary>
        /// Испаряесое значение
        /// </summary>
        public Double EvaporationValue
        {
            set;
            get;
        }
        #endregion Свойства
    }
}
