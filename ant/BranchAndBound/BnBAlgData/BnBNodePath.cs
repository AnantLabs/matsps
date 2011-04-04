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
            _liArc = new List<string>();
            _piePath = new List<string>();
        }

        public BnBNodePath(BnBNodePath path):this()             
        {
            for (int i = 0; i < path.Arc.Count; i++)
                _liArc[i] = path.Arc[i];
            for (int i = 0; i < path.PiePath.Count; i++)
                _piePath[i] = path.PiePath[i];
        }
        #endregion

        #region Поля
        /// <summary>
        /// Дуга "4-3"
        /// </summary>
        private List<string> _liArc;
        /// <summary>
        /// Кусок пути "3-2-1"
        /// </summary>
        private List<string> _piePath;

        /// <summary>
        /// Разделитель в дуге и куске пути
        /// </summary>
        static private char charSplit = '-';
        #endregion

        #region Свойства
        /// <summary>
        /// Список дуг (только для чтения)
        /// </summary>
        public List<string> Arc                     
        {
            get
            {
                return _liArc;
            }
        }

        /// <summary>
        /// Список кусков пути (только для чтения)
        /// </summary>
        public List<string> PiePath                 
        {
            get
            {
                return _piePath;
            }
        }  
        #endregion

        #region Методы (внешние)
        /// <summary>
        /// Добавляет дугу с индексами indexI,indexJ и возвращает запрещенную дугу from,to
        /// </summary>
        /// <param name="indexI">строка добавленной дуги</param>
        /// <param name="indexJ">столбец добавленной дуги</param>
        /// <param name="from">индекс начала запрещенной дуги</param>
        /// <param name="to">индекс конца запрещенной дуги</param>
        public void AddArc(int indexI, int indexJ, ref int from, ref int to)            
        {
            string strNewArc = indexI + charSplit.ToString() + indexJ;
            _liArc.Add(strNewArc);

            string strNewPie = "";
            if (_piePath.Count == 0) // лист кусков пути пустой
            {
                _piePath.Add(strNewArc);
                strNewPie = strNewArc;
            }
            else
            {
                // Цикл прохода по списку кусков
                for (int i = 0; i < _piePath.Count; i++)
                {
                    string[] strarrPies = _piePath[i].Split(charSplit);
                    if (strarrPies[0] == indexJ.ToString()) // пытаемся плюсануть в начало
                        _piePath[i] = indexI + charSplit.ToString() + _piePath[i];

                    if (strarrPies[strarrPies.Length - 1] == indexI.ToString()) // пытаемся плюсануть в конец
                        _piePath[i] = _piePath[i] + indexJ;
                    strNewArc = _piePath[i];

                    // Цикл на "сливание" кусочков пути
                    strarrPies = _piePath[i].Split(charSplit);
                    for (int k = 0; k < _piePath.Count; k++)
                    {
                        // Разделяем k-й кусок на элементы
                        string[] strarrK = _piePath[k].Split(charSplit);
                        if (strarrPies[0] == strarrK[strarrK.Length - 1])
                        {
                            _piePath[i] = _piePath[k] + _piePath[i];
                            _piePath[i].Replace(charSplit.ToString() + strarrPies[0] + charSplit.ToString() + strarrPies[0], charSplit.ToString() + strarrPies[0]);
                            strNewPie = _piePath[i];
                            _piePath.RemoveAt(k);

                        }
                        if (strarrPies[strarrPies.Length - 1] == strarrK[0])
                        {
                            _piePath[i] = _piePath[k] + _piePath[i];
                            _piePath[i].Replace(charSplit.ToString() + strarrK[0] + charSplit.ToString() + strarrK[0], charSplit.ToString() + strarrK[0]);
                            strNewPie = _piePath[i];
                            _piePath.RemoveAt(k);
                        }
                    }
                }
            }

            // Находим запрещенную дугу
            string[] forbiddenArc = strNewPie.Split(charSplit);
            from = Convert.ToInt32(forbiddenArc[0]);
            to = Convert.ToInt32(forbiddenArc[ forbiddenArc.Length - 1]);
        }
        #endregion
    }
}
