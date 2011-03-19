using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.BranchAndBound.BnBAlgData
{
    /// <summary>
    /// Класс хранит часть расчитанного пути в узле дерева
    /// </summary>
    class BnBNodePath
    {
        #region Конструкторы
        public BnBNodePath()        
        {
            _arc = new List<string>();
            _piePath = new List<string>();
        }
        #endregion

        #region Поля
        /// <summary>
        /// Дуга "4-3"
        /// </summary>
        private List<string> _arc;
        /// <summary>
        /// Кусок пути "3-2-1"
        /// </summary>
        private List<string> _piePath;
        #endregion
    }
}
