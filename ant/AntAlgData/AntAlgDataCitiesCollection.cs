﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ant.AntAlgData
{
    /// <summary>
    /// Класс коллекции городов
    /// </summary>
    class AntAlgDataCitiesCollection
    {
        #region Конструкторы и Данные
        public AntAlgDataCitiesCollection() : this(0)                   
        { }
        public AntAlgDataCitiesCollection(int countCities)              
        {
            _iCount = countCities;
        }

        /// <summary>
        /// Заданное количество городов
        /// </summary>
        private int _iCount = 0;
        /// <summary>
        /// Список городов
        /// </summary>
        private List<AntAlgDataCity> _liCities = null;

        // Глобальные переменные
        /// <summary>
        /// Массив расстояний между городами
        /// </summary>
        public double[,] Distance;        
        #endregion

        #region Свойства
        /// <summary>
        /// Максимальное расстояние
        /// </summary>
        public int MaxDistance                      
        {
            set;
            get;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Заполняем координаты городов случайными значениями
        /// </summary>
        /// <param name="count">Количество городов</param>
        public void InitCitiesRandom(int count)     
        {
            _iCount = count;

            _liCities = new List<AntAlgDataCity>();
            Random rand = new Random();

            for (int i = 0; i < _iCount; i++)
            {
                int x = -1, y = -1;

                x = rand.Next(0, MaxDistance);
                y = rand.Next(0, MaxDistance);
                if( i!=0)
                {                    
                    for (int j = i - 1; j > 0; j--)
                    {
                        if (x == _liCities[j].X && y == _liCities[j].Y) // совпадение с уже существующими координатами
                        {
                            x = rand.Next(0, MaxDistance);
                            y = rand.Next(0, MaxDistance);

                            j = i - 1;
                        }
                    }
                }

                _liCities.Add( new AntAlgDataCity( x,y ) );
                _liCities[i].Index = i;
            }

            InitDistance();
        }
        public void InitCitiesRandom()              
        {
            InitCitiesRandom(_iCount);
        }

        /// <summary>
        /// Заполняем массивы расстояний и феромонов
        /// </summary>
        private void InitDistance()              
        {
            //InitPheromone = (1.0 / _iCount);
            Distance = new double[_iCount, _iCount];
            Distance.Initialize();

            for(int from = 0;  from< _iCount; from++)
                for (int to = 0; to < _iCount; to++)
                {
                    if (to != from && Distance[from,to] == 0.0)
                    {
                        int xd = Math.Abs(_liCities[from].X - _liCities[to].X);
                        int yd = Math.Abs(_liCities[from].Y - _liCities[to].Y);
                        Distance[from, to] = Math.Sqrt( xd * xd + yd * yd ) ;
                        Distance[to, from] = Distance[from, to];

                        if (Distance[from, to] == 0)
                        {
                            ;
                        }
                    }
                }
        }

        /// <summary>
        /// Возвращает ссылку на город
        /// </summary>
        /// <param name="index">Порядковый номер города</param>
        /// <returns></returns>
        public AntAlgDataCity this[int index]       
        {
            set { _liCities[index] = value; }
            get { return _liCities[index]; }
        }
        /// <summary>
        /// Количество городов в списке
        /// </summary>
        public int Count                            
        {
            get { return _liCities.Count; }
        }
        #endregion
    }
}
