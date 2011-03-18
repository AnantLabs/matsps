using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ant.CommonData;

namespace ant.AntAlgData
{
    class AntAlgDataAntsCollection
    {
        #region Конструторы и Данные
        public AntAlgDataAntsCollection(int countAnts)          
        {
            _liAnts = new List<AntAlgDataAnt>();

            // Создаем необходимое количество пустых муравьев
            for(int i = 0; i < countAnts; i++)
                _liAnts.Add(new AntAlgDataAnt());
        }

        private List<AntAlgDataAnt> _liAnts = null;
        #endregion

        #region Свойства

        /// <summary>
        /// Количество муравьев
        /// </summary>
        public int Count                                            
        {
            get
            {
                return _liAnts.Count;
            }
        }

        /// <summary>
        /// Возвращает ссылку на муравья
        /// </summary>
        /// <param name="index">Порядковый номер муравья в коллекции</param>
        /// <returns></returns>
        public AntAlgDataAnt this[int index]                        
        {
            set { _liAnts[index] = value; }
            get { return _liAnts[index]; }
        }
        #endregion

        #region Функции
        public void InitAnts(DataCitiesCollection cities)     
        {
            if (_liAnts != null)
            {
                int to = 0;
                for (int i = 0; i < _liAnts.Count; i++)
                {
                    // Распределяем муравьев по городам равномерно
                    if (to == cities.Count)
                        to = 0;
                    _liAnts[i].CurrentCity = cities[to++];

                    _liAnts[i].TabuInit(cities.Count);
                    _liAnts[i].PathInit(cities.Count);

                    _liAnts[i].PathIndex = 1;
                    _liAnts[i].PathSet(0, _liAnts[i].CurrentCity);
                    _liAnts[i].NextCity = null;
                    _liAnts[i].TourLength = 0.0;

                    // Помещаем исходный город, в котором находится муравей в список Табу
                    _liAnts[i].TabuSet(_liAnts[i].CurrentCity.Index, 1);
                }
            }
        }
        #endregion
    }
}
