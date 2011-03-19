using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.BranchAndBound.BnBAlgData
{
    /// <summary>
    /// Данные узла
    /// </summary>
    class BnBNodeData
    {
        /// <summary>
        /// Путь
        /// </summary>
        private BnBNodePath _path;
        /// <summary>
        /// Данные в ячейке матрицы расстояний
        /// </summary>
        private CellData[,] _arrDistance;
        /// <summary>
        /// Массив горизонтальных индексов
        /// </summary>
        private int[] _iArrHorIndexes;
        /// <summary>
        /// Массив вертикальных индексов
        /// </summary>
        private int[] _iArrVerIndexes;

        /// <summary>
        /// суммарный вес
        /// </summary>
        private double dSumVeight; 
    }
}
