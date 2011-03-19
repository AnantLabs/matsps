using System;
using System.Collections.Generic;
using System.Text;

using matsps.CommonData;
using matsps.BranchAndBound.BnBAlgData;
using matsps.Parameters;

namespace matsps.BranchAndBound.BnBAlgLogic
{
    /// <summary>
    /// Алгоритм расчета задачи коммивояжера с помощью метода Ветвей и Границ
    /// </summary>
    class BranchAndBoundTravelSalesman
    {
        #region Конструкторы
        public BranchAndBoundTravelSalesman()                                                               
        {
        } 
        public BranchAndBoundTravelSalesman(CitiesCollection cities)
            : this()                                                                                        
        {
            Cities = cities;
        }
        public BranchAndBoundTravelSalesman(CitiesCollection cities, BnBParameters param):this(cities)      
        {
            _parameters = param;
        }
        #endregion

        #region Поля
        /// <summary>
        /// поле параметров алгоритма ветвей и границ
        /// </summary>
        private BnBParameters _parameters;

        /// <summary>
        /// Первый родительский узел. Начало дерева.
        /// </summary>
        /// 
        ///         0 - это он
        ///        / \
        ///       *   *
        private BnBNode _greatParentNode;
        #endregion

        #region Свойства
        /// <summary>
        /// Возвращает или задает Коллекцию городов
        /// </summary>
        public CitiesCollection Cities                  
        {
            set;
            get;
        }
        #endregion

        #region Методы (внутренние)
        #endregion

        #region Методы (внешние)
        /// <summary>
        /// Запуска расчета в алгоритме Ветвей и Границ
        /// </summary>
        public void Calculate()
        {
            BnBNodeData nData = new BnBNodeData(Cities);    // заносим начальные данные из коллекции городов
            nData.ReductedMatrix();                         // приводим матрице по столбцам и строкам
            _greatParentNode = new BnBNode(nData);          // создаем первый родительский узел с данными
        }
        #endregion
    }
}
