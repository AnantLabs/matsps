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
        /// <summary>
        /// Обход вниз
        /// </summary>
        /// <param name="parent">Текущий родительский узел</param>
        private void GoDown(BnBNode currentNode)
        {
            currentNode.Data.PowCalc();                     // подсчет степеней у нулевых элементов            

            currentNode.Data.LogMatrix();

            double dMaxPowValue = currentNode.Data.GetMaxPowValue();
            for (int i = 0; i < currentNode.Data.Length; i++ )
                for (int j = 0; j < currentNode.Data.Length; j++)
                    if (currentNode.Data.Distance[i, j].Pow == dMaxPowValue)
                    {
                        // Пересечение с максимальным значением степени

                        int childI = currentNode.Data.VerIndexes[i];
                        int childJ= currentNode.Data.HorIndexes[j];

                        // Берется дуга i,j
                        BnBNodeData childDataFirst = new BnBNodeData( currentNode.Data, i,j);
                        BnBNode childNodeFirst = new BnBNode(childDataFirst);                        
                        childDataFirst.RecalcPath(childI, childJ);               // добавляем дугу в маршрут и исключаем закольцованность
                        childDataFirst.ReductedMatrix();                         // приведение матрицы
                        childDataFirst.PowCalc();                                // подсчет степеней матрицы
                        childDataFirst.LogMatrix();

                        // Не берется дуга i,j
                        BnBNodeData childDataSecond = new BnBNodeData(currentNode.Data);
                        BnBNode childNodeSecond = new BnBNode(childDataSecond);  
                        childDataSecond.RemoveLoopback(childI, childJ);
                        childDataSecond.RemoveLoopback(childJ, childI);
                        childDataSecond.ReductedMatrix();
                        childDataSecond.PowCalc();
                        childDataSecond.LogMatrix();

                        currentNode.Nodes.Add(childNodeFirst);
                        currentNode.Nodes.Add(childNodeSecond);

                        break;
                    }

            // Находим минимальный суммарный вес у потомков
            double minSummWeight = double.MaxValue;
            for (int i = 0; i < currentNode.Nodes.Count; i++)
                if (currentNode.Nodes[i].Data.SummWeight < minSummWeight)
                    minSummWeight = currentNode.Nodes[i].Data.SummWeight;
            // Запускаем функцию ОбходВверх для первого дочернего элемента с минимальным суммарным весом
            for (int i = 0; i < currentNode.Nodes.Count; i++ )
                if(currentNode.Nodes[i].Data.SummWeight == minSummWeight)
                {
                    GoUp(currentNode.Nodes[i]);
                    break;
                }
        }

        /// <summary>
        /// Обход вверх
        /// </summary>
        private void GoUp(BnBNode currentNode)
        {

        }
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

            GoDown(_greatParentNode);
        }
        #endregion
    }
}
