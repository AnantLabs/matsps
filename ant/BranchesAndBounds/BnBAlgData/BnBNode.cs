using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BranchesAndBounds.BnBAlgData
{
    /// <summary>
    /// Узел дерева
    /// </summary>
    class BnBNode
    {
        #region Поля


        #endregion

        #region Конструкторы

        BnBNode()
        { 
            
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


        #endregion
    }
}
