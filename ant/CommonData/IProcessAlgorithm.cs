using System;
using System.Collections.Generic;
using System.Text;

using matsps.Parameters;

namespace matsps.CommonData
{
    /// <summary>
    /// Интерфейс обработчиков алгоритма
    /// </summary>
    internal interface IProcessAlgorithm
    {
        #region Свойства
        /// <summary>
        /// Коллекция городов для алгоритма
        /// </summary>
        CitiesCollection Cities             
        {
            set;
            get;
        }

        /// <summary>
        /// Параметры алгоритма
        /// </summary>
        IParameters Parameters                  
        {
            set;
            get;
        }


        /// <summary>
        /// Возвращает лист с отладочной информацией расчета
        /// </summary>
        List<string> ResultInfo          
        {
            get;
        }

        /// <summary>
        /// Возвращает коллекцию городов, расположенных в порядке лучшего пути
        /// </summary>
        Route ResultPath                 
        {
            get;
        }

        /// <summary>
        /// Возвращает время расчета алгоритма
        /// </summary>
        TimeSpan ProcessTime             
        {
            get;
        }
        #endregion

        #region Методы (внешние)

        /// <summary>
        /// Запуск алгоритма
        /// </summary>
        void Start();
        /// <summary>
        /// Запуск алгоритма с переопределением параметров
        /// </summary>
        /// <param name="cities">Коллекция городов</param>
        /// <param name="parameters">Параметры алгоиртма</param>
        void Start(CitiesCollection cities, IParameters parameters);
                
        #endregion
    }
}
