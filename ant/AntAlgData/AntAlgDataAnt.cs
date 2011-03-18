using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ant.CommonData;

namespace ant.AntAlgData
{
    /// <summary>
    /// Хранит данные о проходе одного муравья
    /// </summary>
    class AntAlgDataAnt
    {
        #region Конструкторы и Данные
        public AntAlgDataAnt() { }

        private DataCity _currCity;
        private DataCity _nextCity;

        private List<int> _liTabuIndexes;
        private int _iPathIndex;
        private List<DataCity> _liCitiesPath;

        private double _dTourLength;
        #endregion

        #region Свойства
        /// <summary>
        /// Текущий город, где находится сейчас муравей
        /// </summary>
        public DataCity CurrentCity            
        {
            set
            {
                _currCity = value;
            }
            get { return _currCity; }
        }

        /// <summary>
        /// Следующий город, который должен посетить муравей
        /// </summary>
        public DataCity NextCity               
        {
            set
            {
                _nextCity = value;
            }
            get
            {
                return _nextCity;
            }
        }

        /// <summary>
        /// ...
        /// </summary>
        public int PathIndex                         
        {
            set { _iPathIndex = value; }
            get { return _iPathIndex; }
        }

        /// <summary>
        /// Длина пройденного муравьем пути
        /// </summary>
        public double TourLength                    
        {
            set { _dTourLength = value; }
            get { return _dTourLength; }
        }
        #endregion

        #region Функции для списка Табу
        /// <summary>
        /// НАчальное заполнение списка ТАБУ нулями
        /// </summary>
        /// <param name="countCities">Количество городов</param>
        public void TabuInit(int countCities)               
        {
            _liTabuIndexes = new List<int>();
            for (int i = 0; i < countCities; i++)
            {
                _liTabuIndexes.Add( 0 );
            }
        }

        /// <summary>
        /// Устанавлиеваем значение для списка ТАБУ
        /// </summary>
        /// <param name="indexTabu">Индекс города</param>
        /// <param name="value">Значение</param>
        public void TabuSet(int indexTabu, int value)       
        {
            try
            {
                _liTabuIndexes[indexTabu] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Возвращаем значение из списка ТАБУ
        /// </summary>
        /// <param name="index">Индекс города</param>
        /// <returns></returns>
        public int TabuGet(int index)                       
        {
            try
            {
                return _liTabuIndexes[index];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Функции для списка Пути
        /// <summary>
        /// Начальное заполнение списка ПУТИ нулями
        /// </summary>
        /// <param name="countCities">Количество городов</param>
        public void PathInit(int countCities)                           
        {
            _liCitiesPath = new List<DataCity>();
            for (int i = 0; i < countCities; i++)
            {
                _liCitiesPath.Add(null);
            }
        }

        /// <summary>
        /// Устанавлиеваем значение для списка ПУТИ
        /// </summary>
        /// <param name="indexTabu">Индекс города</param>
        /// <param name="value">Ссылка на город</param>
        public void PathSet(int indexPath, DataCity value)        
        {
            try
            {
                _liCitiesPath[indexPath] = value;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Вернуть из списка пути
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public DataCity PathGet(int index)                        
        {
            try
            {
                return _liCitiesPath[index];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
