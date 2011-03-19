using System;
using System.Collections.Generic;
using System.Text;

namespace BranchesAndBounds.BnBAlgData
{
    /// <summary>
    /// Коллекция узлов
    /// </summary>
    class BnBNodesCollection
    {
        #region Поля

        /// <summary>
        /// Список узлов
        /// </summary>
        private  List<BnBNode> _nodesList = null;

        #endregion

        #region Конструкторы

        BnBNodesCollection()
        {

        }

        #endregion

        #region Свойства

        /// <summary>
        /// Доступ к узлу по индексу
        /// </summary>
        public BnBNode this[int index]
        {
            set { _nodesList[index] = value; }
            get { return _nodesList[index]; }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Добавляет новый узел в коллекцию
        /// </summary>
        /// <param name="node"></param>
        public void Add(BnBNode node)
        { 
            _nodesList.Add(node);
        }

        #endregion
    }
}
