using System;
using System.Collections.Generic;
using System.Text;


namespace matsps.BranchAndBound.BnBAlgData
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    class BnBNode
    {
        #region Поля
        
        #endregion

        #region Конструкторы

        public BnBNode()                                    
        {
            Forbidden = false;
            Nodes = new BnBNodesCollection();
        }
        public BnBNode(BnBNodeData data)
            : this()                                        
        {
            Data = data;
        }

        #endregion

        #region Свойство

        /// <summary>
        /// Запрещен ли узел для обхода
        /// </summary>
        public bool Forbidden                   
        {
            set;
            get;
        }

        /// <summary>
        /// Коллекция узлов
        /// </summary>
        public BnBNodesCollection Nodes         
        {
            set;
            get;
        }
        /// <summary>
        /// Данные о матрице, пути и т.д.
        /// </summary>
        public BnBNodeData Data                
        {
            set;
            get;
        }
        #endregion
    }
}
