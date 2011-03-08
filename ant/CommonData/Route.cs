using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ant.CommonData;

namespace ant.CommonData
{
    class Route
    {
        #region Поля класса
        /// <summary>
        /// Коллекция городов
        /// </summary>
        private readonly DataCitiesCollection _cities = null;
        /// <summary>
        /// Длина маршрута
        /// </summary>
        private double _dlength = 0.0;
        #endregion

        #region Конструкторы

        public Route()
        {         
        }
        public Route(DataCitiesCollection cities)
        {
            _cities = cities;
            Length(); //расчет длинны маршрута
        }
        public Route(DataCitiesCollection cities, double length)
        {         
        }



        #endregion

        #region Свойства
        /// <summary>
        /// Длина маршрута
        /// </summary>
        public double length
        {
            get { return _dlength; }
        }
        /// <summary>
        /// Доступ к городу маршрута по индексу
        /// </summary>
        public DataCity this[int index]
        {
            set { _cities[index] = value; }
            get { return _cities[index]; }
        }
        /// <summary>
        /// Количество городов в маршруте
        /// </summary>
        public int Count
        {
            get {return _cities.Count;}
        }
        /// <summary>
        /// Максимальная дистанция
        /// </summary>
        public int MaxDistance
        {
            get { return _cities.MaxDistance; }
        }
        /// <summary>
        /// Список городов в маршруте
        /// </summary>
        public DataCitiesCollection Cities
        {
            get { return _cities; }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Вычисляет длину маршрута
        /// </summary>
        private void Length()
        {
            //int i = 0;
            _dlength = 0;
            for (int j = 1; j < _cities.Count; j++)
            {
                //_dlength += _cities.Distance[i, j];
                _dlength += _cities.Distance[_cities[j - 1].Index, _cities[j].Index];
                //i++;
            }
            //из первого в последний
            //_dlength += _cities.Distance[0, _cities.Count-1];
            _dlength += _cities.Distance[_cities[_cities.Count - 1].Index, _cities[ 0 ].Index];
        }

        #endregion
    }
}
