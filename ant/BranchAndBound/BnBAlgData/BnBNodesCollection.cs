using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.BranchAndBound.BnBAlgData
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

        public BnBNodesCollection()
        {
            _nodesList = new List<BnBNode>();
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

        /// <summary>
        /// Количество элементов в коллекции узлов
        /// </summary>
        public int Count                        
        {
            get
            {
                return _nodesList.Count;
            }
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
