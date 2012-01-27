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
        /// <summary>
        /// Родительский узел
        /// </summary>
        private BnBNode _parent;

        /// <summary>
        /// Счетчик экземпляров класса
        /// </summary>
        private static int _counter = 0;

        private int _index;
        #endregion

        #region Конструкторы

        public BnBNode(BnBNode parent)                                    
        {
            _counter++;
            _index = _counter;

            _parent = parent;
            Forbidden = false;
            Viewed = false;
            Nodes = new BnBNodesCollection();
        }
        public BnBNode(BnBNode parent, BnBNodeData data)
            : this(parent)                                        
        {
            Data = data;
        }

        #endregion

        #region Свойство
        /// <summary>
        /// Индекс узла. Порядковый номер. (только для чтения)
        /// </summary>
        public int Index                        
        {
            get
            {
                return _index;
            }
        }

        /// <summary>
        /// Узел запрещен для обхода
        /// </summary>
        public bool Forbidden                   
        {
            set;
            get;
        }

        /// <summary>
        /// Узел уже просмотрен
        /// </summary>
        public bool Viewed                      
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
        /// Родительский узел (только для чтения)
        /// </summary>
        public BnBNode ParentNode               
        {
            get
            {
                return _parent;
            }
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
