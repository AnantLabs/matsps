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

        /// <summary>
        /// Критерий веса для дерева (минимальный суммарный вес)
        /// </summary>
        private double _dCriterialWeight = double.PositiveInfinity;

        /// <summary>
        /// Флаг. Функция ОбходВверх закончена.
        /// </summary>
        private bool _bGoUpEnded = false;
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
            // Если матрица 2х2
            if (currentNode.Data.Length == 2)
            {
                for (int i = 0; i < currentNode.Data.Length; i++ )
                    for (int j = 0; j < currentNode.Data.Length; j++)
                        if (currentNode.Data.Distance[i, j].Value == 0)
                        {
                            int childI = currentNode.Data.VerIndexes[i];
                            int childJ = currentNode.Data.HorIndexes[j];

                            currentNode.Data.RecalcPath(childI, childJ);
                        }
                _dCriterialWeight = currentNode.Data.SummWeight;
                currentNode.Data.LogMatrix();

                currentNode.Viewed = true;
                // Запускаем функцию ОбходВверх(текущий_узел);
                GoUp(currentNode);
                return;
            }
            
            //currentNode.Data.LogMatrix();
            if (currentNode.Viewed == false)
            {
                double dMaxPowValue = currentNode.Data.GetMaxPowValue();
                for (int i = 0; i < currentNode.Data.Length; i++)
                    for (int j = 0; j < currentNode.Data.Length; j++)
                        if (currentNode.Data.Distance[i, j].Pow == dMaxPowValue)
                        {
                            // Пересечение с максимальным значением степени

                            int childI = currentNode.Data.VerIndexes[i];
                            int childJ = currentNode.Data.HorIndexes[j];

                            // Берется дуга i,j
                            BnBNodeData childDataFirst = new BnBNodeData(currentNode.Data, i, j);
                            BnBNode childNodeFirst = new BnBNode(currentNode, childDataFirst);
                            childDataFirst.RecalcPath(childI, childJ);               // добавляем дугу в маршрут и исключаем закольцованность
                            childDataFirst.ReductedMatrix();                         // приведение матрицы
                            childDataFirst.PowCalc();                                // подсчет степеней матрицы
                            childDataFirst.LogMatrix();

                            // Не берется дуга i,j
                            BnBNodeData childDataSecond = new BnBNodeData(currentNode.Data);
                            BnBNode childNodeSecond = new BnBNode(currentNode, childDataSecond);
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
                // Запускаем функцию ОбходВниз для первого дочернего элемента с минимальным суммарным весом
                for (int i = 0; i < currentNode.Nodes.Count; i++)
                    if (currentNode.Nodes[i].Data.SummWeight == minSummWeight)
                    {
                        GoDown(currentNode.Nodes[i]);
                        break;
                    }
            }
            return;
        }

        /// <summary>
        /// Обход вверх
        /// </summary>
        private void GoUp(BnBNode currentNode)                  
        {
            if (_bGoUpEnded) // функция возвратилась
                return;

            //получили родителя
            BnBNode parentNode = currentNode.ParentNode;
            BnBNode childNode = currentNode;
            for (int i = 0; i < parentNode.Nodes.Count; i++)
            {
                if( parentNode.Nodes[i] != childNode )
                {
                    if( parentNode.Nodes[i].Data.SummWeight <= _dCriterialWeight )
                    {
                        if( parentNode.Nodes[i].Viewed  == false )
                        {
                            //parentNode.Nodes[i].Viewed = true;
                            // Обход вниз для i-го ребенка
                            GoDown(parentNode.Nodes[i]);
                        }
                    }
                    else
                    {
                        parentNode.Nodes[i].Forbidden = true;
                    }
                }
            }

            // Продолжаем функцию ОбходВверх для родителя родителя.
            if (parentNode.ParentNode != null)
            {
                parentNode.Viewed = true;
                GoUp(parentNode);
            }
            else
            {
                _bGoUpEnded = true;
                return;
            }

            return;
        }

        /// <summary>
        /// Находит лучшие пути
        /// </summary>
        /// <param name="currentNode">Текущий узед</param>
        /// <param name="liPath">Накопленный путь ввиде списка</param>
        /// <returns></returns>
        private List<string> FindBest(BnBNode currentNode, List<string> liPath)
        {
            if (currentNode.Data.Length == 2)
            {
                if( currentNode.Data.SummWeight <= _dCriterialWeight )
                    liPath.Add(currentNode.Index + " | " + currentNode.Data.Path.PiePath[0] + " | " + currentNode.Data.SummWeight);
                return liPath;
            }

            for (int i = 0; i < currentNode.Nodes.Count; i++)
                if (currentNode.Nodes[i].Forbidden == false)
                    FindBest(currentNode.Nodes[i], liPath);
            return liPath;
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
            nData.PowCalc();                                // подсчет степеней у нулевых элементов            
            nData.LogMatrix();                              // вывод в логи для отладки
            _greatParentNode = new BnBNode(null, nData);          // создаем первый родительский узел с данными

            GoDown(_greatParentNode);

            List<string> liPath = FindBest(_greatParentNode, new List<string>());
        }
        #endregion
    }
}
