﻿using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing; // для класса Color

using matsps.CommonData;

namespace matsps.CommonData
{
    /// <summary>
    /// параметры прорисовки маршрута
    /// </summary>
    struct Drawing
    {
        #region Свойства
        /// <summary>
        /// видимость маршрута
        /// </summary>
        public bool Visible             
        {
            set;
            get;
        }
        /// <summary>
        /// цвет прорисовки
        /// </summary>
        public Color Color              
        {
            set;
            get;
        }
        /// <summary>
        /// прозрачность
        /// </summary>
        public int Opacity              
        {
            set
            {
                _opacity = value;
                if (_opacity < 0 || _opacity > 100)
                    throw new Exception("неверно задана прозрачность");
            }
            get { return _opacity; }
        }
        #endregion

        #region Поля
        /// <summary>
        /// прозрачность
        /// </summary>
        private int _opacity;

        #endregion
    };

    class Route
    {
        #region Поля класса
        /// <summary>
        /// параметры прорисовки маршрута
        /// </summary>
        public Drawing Drawing;
        /// <summary>
        /// Коллекция городов
        /// </summary>
        private readonly CitiesCollection _cities = null;
        /// <summary>
        /// Длина маршрута
        /// </summary>
        private double _dlength = 0.0;
        private string _name;

        /// <summary>
        /// статичная переменная. хранит количество созданных экзампляров класса
        /// </summary>
        private static int _statCountUNu,ber = 0;

        private int _iUniqueNumber;
        #endregion

        #region Конструкторы
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="name"></param>
        public Route(string name)                                               
        {
            _name = name;
            _statCountUNu++;            // Уникальный номер экземпляра класса
            this._iUniqueNumber = _statCountUNu;
            Drawing.Color = Color.Blue;     // Цвет по умолчанию - синий
            Drawing.Visible = false;        // Видимость по умолчанию - скрытый
        }
        /// <summary>
        /// Конструктор
        /// </summary>
        public Route(CitiesCollection cities, string name):this(name)       
        {
            _cities = cities;
            LengthCalculation(); //расчет длинны маршрута
        }

        #endregion

        #region Свойства
        /// <summary>
        /// уникальный номер маршрута
        /// </summary>
        public int UniqueNum                            
        {
            get
            {
                return this._iUniqueNumber;
            }
        }
        /// <summary>
        /// комментарии к маршруту
        /// </summary>
        public string Comments                          
        {
            set;
            get;
        }
        /// <summary>
        /// время расчета
        /// </summary>
        public TimeSpan СalcTime                        
        {
            set;
            get;
        }
        /// <summary>
        /// Длина маршрута
        /// </summary>
        public double Length                            
        {
            get { return _dlength; }
        }
        /// <summary>
        /// Название алгоритма, которым просчитан данный маршрут(только чтение)
        /// </summary>
        public string Name                              
        {
            get { return _name; }
        }

        ///// <summary>
        ///// Доступ к городу маршрута по индексу
        ///// </summary>
        //public DataCity this[int index]                 
        //{
        //    set { _cities[index] = value; }
        //    get { return _cities[index]; }
        //}
        /// <summary>
        /// Количество городов в маршруте
        /// </summary>
        public int Count                                
        {
            get { return _cities.Count; }
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
        public CitiesCollection Cities              
        {
            get { return _cities; }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Вычисляет длину маршрута
        /// </summary>
        private void LengthCalculation()
        {            
            _dlength = 0.0;
            _cities.DistanceCalculate();
            
                for (int j = 1; j < _cities.Count; j++)
                {
                    _dlength += 
                        Math.Sqrt( (double)Math.Pow( (_cities[j-1].X - _cities[j].X),2) +
                            (double)Math.Pow((_cities[j - 1].Y - _cities[j].Y), 2)
                        );                    
                }
            //из первого в последний            
                _dlength +=
                    Math.Sqrt((double)Math.Pow((_cities[ _cities.Count - 1].X - _cities[0].X), 2) +
                                (double)Math.Pow((_cities[_cities.Count - 1].Y - _cities[0].Y), 2)
                            );
        }

        #endregion
    }
}

