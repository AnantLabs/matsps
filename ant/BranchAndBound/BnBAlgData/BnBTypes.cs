using System;
using System.Collections.Generic;
using System.Text;

namespace ant.BranchAndBound.BnBAlgData
{
    /// <summary>
    /// Данные в ячейке матрицы расстояний
    /// </summary>
    public struct CellData
    {
        /// <summary>
        /// Длина дуги
        /// </summary>
        public double Value
        {
            set;
            get;
        }
        /// <summary>
        /// Степень ячейки
        /// </summary>
        public double Pow
        {
            set;
            get;
        }
    }
}
