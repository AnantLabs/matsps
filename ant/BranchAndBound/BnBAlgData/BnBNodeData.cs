using System;
using System.Collections.Generic;
using System.Text;

using matsps.CommonData;

using log4net;
using log4net.Config;

namespace matsps.BranchAndBound.BnBAlgData
{
    /// <summary>
    /// Данные узла (матрица расстояний, горизонтальные и вертикальные индексы, уже рассчитаннй путь)
    /// </summary>
    class BnBNodeData : ICloneable
    {
        #region Конструкторы
        public BnBNodeData()                                                            
        {
        }
        /// <summary>
        /// Переводит данные из коллекции городов в тип данные узла
        /// </summary>
        /// <param name="cities">коллекция городов</param>
        public BnBNodeData(CitiesCollection cities)                                         
        {
            try
            {
                DataFromOther(null, cities, -1, -1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }                  
        }
        /// <summary>
        /// Копирует из данных узла
        /// </summary>
        /// <param name="nodeData">данные узла</param>   
        public BnBNodeData(BnBNodeData nodeData)                                            
        {
            try
            {
                DataFromOther(nodeData, null, -1, -1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        /// <summary>
        /// Копирует из данных узла, исключая определенную строку и столбец
        /// </summary>
        /// <param name="nodeData">Данные узла дерева, из которых нужно получить копию</param>
        /// <param name="indexRow">Индекс строки, которую НЕ нужно копировать</param>
        /// <param name="indexColumn">Индекс столбца, который НЕ нужно копировать</param>
        public BnBNodeData(BnBNodeData nodeData, int indexRow, int indexColumn)             
        {
            if (indexColumn < 0 || indexRow < 0)
                throw new Exception("Индексы удаляемых строки и столбца должны быть положительными");
            try
            {
                DataFromOther(nodeData, null, indexRow, indexColumn);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }            
        }
        #endregion

        #region Поля
        /// <summary>
        /// Путь
        /// </summary>
        private BnBNodePath _path;

        /// <summary>
        /// Данные в ячейке матрицы расстояний
        /// </summary>
        private CellData[,] _arrDistance;
        /// <summary>
        /// Число строк/столбцов в матрице расстояний
        /// </summary>
        private int _iDistLength;
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
        private double _dSumWeight;


        private static ILog log = LogManager.GetLogger(string.Empty);
        #endregion

        #region Свойства
        /// <summary>
        /// Размерность матрицы расстояний. Число строк или число столбцом. (только для чтения)
        /// </summary>
        public int Length                   
        {
            get
            {
                return _iDistLength;
            }
        }

        /// <summary>
        /// Массив расстояний (только для чтения)
        /// </summary>
        public CellData[,] Distance         
        {
            get
            {
                return _arrDistance;
            }
        }

        /// <summary>
        /// Сформированный путь (только для чтения)
        /// </summary>
        public BnBNodePath Path             
        {
            get
            {
                return _path;
            }
        }

        /// <summary>
        /// Вертикальные индексы (только для чтения)
        /// </summary>
        public int[] VerIndexes             
        {
            get
            {
                return _iArrVerIndexes;
            }
        }
        /// <summary>
        /// Горизонтальные индексы (только для чтения)
        /// </summary>
        public int[] HorIndexes             
        {
            get
            {
                return _iArrHorIndexes;
            }
        }
        /// <summary>
        /// Возвращает суммарный вес матрицы в данном узле (только для чтения)
        /// </summary>
        public double SummWeight            
        {
            get
            {
                return _dSumWeight;
            }
        }
        #endregion

        #region Методы (внутренние)
        /// <summary>
        /// Заполнить поля класса данными из NodeData ИЛИ CitiesCollection
        /// </summary>
        /// <param name="nodeData">Узел, копию данных которого создает функция</param>
        /// <param name="cities">Коллекция городов, из которой можно получить данные</param>
        /// <param name="withoutRow">Исключая строку с данным индексом. Значение -1 значит, что без исключений</param>
        /// <param name="withoutColumn">Исключая столбец с данным индексом. Значение -1 значит, что без исключений</param>
        private void DataFromOther(BnBNodeData nodeData, CitiesCollection cities, int withoutRow, int withoutColumn)
        {
            if (nodeData != null && cities != null)
            {
                throw new Exception("Получать данные можно либо из NodeData, либо из CitiesCollection");
            }
            BnBNodePath path = new BnBNodePath();

            // Выделяем память для массива дистанций
            if (cities != null)
                _iDistLength = (int)Math.Sqrt((double)cities.Distance.Length);
            else
                _iDistLength = nodeData.Length;

            if (withoutRow >= 0 && withoutColumn < 0
                ||
                withoutRow < 0 && withoutColumn >= 0) // задан для исключения только столбец или только строка
                throw new Exception("Задан для исключения только столбец или только строка");

            if (withoutRow >= 0 && withoutColumn >= 0) // исключаем строку и столбец. длина сокращается на 1
                _iDistLength--;
            _arrDistance = new CellData[Length, Length];

            // Массивы горизонтальных и вертикальных индексов
            _iArrHorIndexes = new int[Length];
            _iArrVerIndexes = new int[Length];


            int i,j,k, l;   // индексы исходных данных, откуда происходит компирвоание
            for (i = 0,k = 0; i < Length; i++, k++)    // проход по строкам массива дистанции
            {
                if (withoutRow == i) // пропускаем строку в исходных данных
                    k++;
                if (cities != null)
                {
                    // Заполняем массивы индексом значениями по умолчанию (1,2,3...n)            
                    _iArrVerIndexes[i] = i;
                }
                else
                {
                        _iArrVerIndexes[i] = nodeData.VerIndexes[k];
                }
                for (j = 0, l =0; j < Length; j++, l++) // проход по столбцам массива дистанции
                {
                    if (withoutColumn == j) // пропускаем строку в исходных данных
                        l++;
                    CellData cell = new CellData();
                    if (cities != null)
                    {
                        _iArrHorIndexes[i] = i;
                        if (i == j) // главная дагональ
                            cell.Value = double.PositiveInfinity;
                        else
                            cell.Value = Convert.ToDouble(cities.Distance[i, j]);
                        cell.Pow = double.PositiveInfinity;
                    }
                    else
                    {
                        _iArrHorIndexes[j] = nodeData.HorIndexes[l];
                        cell.Value = nodeData.Distance[k, l].Value;
                        cell.Pow = nodeData.Distance[k, l].Pow;
                    }


                    _arrDistance[i, j] = cell;
                }
            }

            if (cities != null)
            {
                _dSumWeight = 0.0;
                _path = new BnBNodePath();
            }
            else
            {
                _dSumWeight = nodeData.SummWeight;
                _path = new BnBNodePath(nodeData.Path);
            }
        }

        #endregion

        #region Методы (внешние)
        /// <summary>
        /// Приведение матрицы (сокращение на определенную величину по строкам и столбцам)
        /// </summary>
        public void ReductedMatrix()                                        
        {
            // Находим минимальных элемент в каждой строке и сокращаем на него всю строку. Сумму запоминаем
            double summ = 0;            
            for (int i = 0; i < Length; i++)
            {
                double min = double.MaxValue;
                // Ищем мин в СТРОКЕ
                for (int j = 0; j < Length; j++)
                    if (_arrDistance[i, j].Value < min)
                    {
                        min = _arrDistance[i, j].Value;
                    }
                if (min != 0)
                {
                    // сокращаем СТРОКУ на мин
                    for (int j = 0; j < Length; j++)
                    {
                        _arrDistance[i, j].Value -= min;
                    }
                    // прибавляем мин сумме, чтобы накопить суммарный вес
                    summ += min;
                }
            }

            // сокращаем по столбцам
            for (int j = 0; j < Length; j++)
            {
                double min = double.MaxValue;
                // Ищем мин в СТОЛБЦЕ
                for (int i = 0; i < Length; i++)
                    if (_arrDistance[i, j].Value < min)
                    {
                        min = _arrDistance[i, j].Value;
                    }

                if (min != 0)
                {
                    // сокращаем СТОЛБЕЦ на мин
                    for (int i = 0; i < Length; i++)
                    {
                        _arrDistance[i, j].Value -= min;
                    }
                    // прибавляем мин сумме, чтобы накопить суммарный вес
                    summ += min;
                }
            }

            // прибавляем вес сокращения к Общему суммарному весу
            _dSumWeight += summ;   
        }

        /// <summary>
        /// Подсчет степеней у матрицы
        /// </summary>
        public void PowCalc()                                               
        {
            for (int i = 0; i < Length; i++)
                for(int j =0; j < Length; j++)
                    if (_arrDistance[i, j].Value == 0) // нашли нулевой элемент
                    {
                        double minI = double.MaxValue;
                        double minJ = double.MaxValue;

                        for (int k = 0; k < Length; k++) // минимум по строке
                            if (k != j && _arrDistance[i, k].Value < minJ)
                                minJ = _arrDistance[i, k].Value;
                        for (int l = 0; l < Length; l++) // минимум по столбцу
                            if (l != i && _arrDistance[l, j].Value < minI)
                                minI = _arrDistance[l, j].Value;

                        _arrDistance[i, j].Pow = (minI + minJ);
                    }
        }

        /// <summary>
        /// Возвращает значение максимальной степени в матрице
        /// </summary>
        public double GetMaxPowValue()                                      
        {
            double max = double.MinValue;
            for(int  i=0; i < Length; i++)
                for(int j=0; j < Length; j++)
                    if(_arrDistance[i,j].Pow > max && !double.IsInfinity( _arrDistance[i,j].Pow ))
                    {
                        max = _arrDistance[i,j].Pow;
                    }
            return max;
        }

        /// <summary>
        /// Убирает закольцованность(выставляет бесконечность), учитывая индексы строки и столбца
        /// </summary>
        /// <param name="col">номер столбца</param>
        /// <param name="row">номер строки</param>
        public void RemoveLoopback(int indexRow, int indexCol)              
        {
            for (int i = 0; i < HorIndexes.Length; i++)
                for (int j = 0; j < VerIndexes.Length; j++)
                    if (indexRow == VerIndexes[i] && indexCol == HorIndexes[j])
                    {
                        _arrDistance[i, j].Value = double.PositiveInfinity;
                        _arrDistance[i, j].Pow = double.PositiveInfinity;
                    }
        }

        /// <summary>
        /// Пересчитывает путь c учетом вхождения дуги i,j , а также запрещает некоторые дуги
        /// </summary>
        /// <param name="indexRow">индекс строки новой дуги</param>
        /// <param name="indexCol">индекс столбца новой дуги</param>
        public void RecalcPath(int indexRow, int indexCol)                  
        {
            RemoveLoopback(indexCol, indexRow);
            RemoveLoopback(indexRow, indexCol);
            int row2 = indexRow, col2 =indexCol;          // вторая дуга
            _path.AddArc(indexRow, indexCol, ref row2, ref col2);

            if (row2 != indexRow || col2 != indexCol)
            {
                RemoveLoopback(row2, col2);
                RemoveLoopback(col2, row2);
            }
        }

        /// <summary>
        /// Получить копию данного экземпляра класса
        /// </summary>
        /// <returns>Объект NodeData, заполненный данными как в текущем экземпляре класса</returns>
        public object Clone()                                               
        {
            BnBNodeData cloneData = new BnBNodeData();
            try
            {
                cloneData.DataFromOther(this, null, -1, -1);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cloneData;
        }

        /// <summary>
        /// Напечатать матрицу в лог-файл
        /// </summary>
        public void LogMatrix()                                             
        {
            // Печать индексов столбцов
            log.Debug("------------------value------------");
            string strColIndexes = "   ";
            for (int i = 0; i < Length; i++)
                strColIndexes += String.Format("  {0:00}  ", _iArrHorIndexes[i]);
            log.Debug(strColIndexes);

                for (int i = 0; i < Length; i++)
                {
                    string strRow = string.Format("{0:00} ", _iArrVerIndexes[i]);
                    for (int j = 0; j < Length; j++)
                        strRow += (double.IsInfinity(_arrDistance[i, j].Value)) ? "----- " : String.Format("{0:00.00} ", _arrDistance[i, j].Value) ;
                    log.Debug(strRow);
                }

                log.Debug("\tДуги:");
                for (int i = 0; i > _path.Arc.Count; i++)
                    log.Debug("\t" + (i+1).ToString() + _path.Arc[i] );
                log.Debug("\tКуски пути:");
                for (int i = 0; i > _path.PiePath.Count; i++)
                    log.Debug("\t" + (i + 1).ToString() + _path.PiePath[i]);

            log.Debug("-----------------pow---------------");
            strColIndexes = "   ";
            for (int i = 0; i < Length; i++)
                strColIndexes += String.Format("  {0:00}  ", _iArrHorIndexes[i]);
            log.Debug(strColIndexes);

            for (int i = 0; i < Length; i++)
            {
                string strRow = string.Format("{0:00} ", _iArrVerIndexes[i]);
                for (int j = 0; j < Length; j++)
                    strRow += (double.IsInfinity(_arrDistance[i, j].Pow)) ? "----- " : String.Format("{0:00.00} ", _arrDistance[i, j].Pow);
                log.Debug(strRow);
            }
            log.DebugFormat("Общий вес {0:0000.00}",_dSumWeight);
            log.Debug(" ");
        }

        /// <summary>
        /// Лист с отладочной информацией
        /// </summary>
        /// <returns></returns>
        public List<string> ListInfo()                                      
        {
            List<string> liStr = new List<string>();

            // Печать индексов столбцов
            liStr.Add("------------------value------------");
            string strColIndexes = "   ";
            for (int i = 0; i < Length; i++)
                strColIndexes += String.Format("  {0:00}  ", _iArrHorIndexes[i]);
            liStr.Add(strColIndexes);

            for (int i = 0; i < Length; i++)
            {
                string strRow = string.Format("{0:00} ", _iArrVerIndexes[i]);
                for (int j = 0; j < Length; j++)
                    strRow += (double.IsInfinity(_arrDistance[i, j].Value)) ? "----- " : String.Format("{0:00.00} ", _arrDistance[i, j].Value);
                liStr.Add(strRow);
            }

            liStr.Add("\tДуги:");
            for (int i = 0; i > _path.Arc.Count; i++)
                liStr.Add("\t" + (i + 1).ToString() + _path.Arc[i]);
            liStr.Add("\tКуски пути:");
            for (int i = 0; i > _path.PiePath.Count; i++)
                liStr.Add("\t" + (i + 1).ToString() + _path.PiePath[i]);

            liStr.Add(String.Format( "Общий вес {0:0000.00}", _dSumWeight));
            liStr.Add(" ");

            return liStr;
        }
        #endregion
    }
}
