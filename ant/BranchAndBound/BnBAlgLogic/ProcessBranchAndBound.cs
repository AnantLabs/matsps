using System;
using System.Collections.Generic;
using System.Text;

using ant.CommonData;                   // классы совместрых данных
using ant.BranchAndBound.BnBAlgData;    // классы с данными алгоритма
using ant.Parameters;                   // классы параметров


namespace ant.BranchAndBound.BnBAlgLogic
{
    /// <summary>
    /// Процесс расчета методом Ветвей и границ
    /// </summary>
    class ProcessBranchAndBound : IProcessAlgorithm
    {
        #region Конструкторы
        public ProcessBranchAndBound()
        {
        }
        #endregion

        #region Поля
        /// <summary>
        /// Лист с отладочной информацией
        /// </summary>
        private List<string> _liStrInfo;

        /// <summary>
        /// Время рачета алгоритма
        /// </summary>
        private TimeSpan _tsProcessTime;

        /// <summary>
        /// Время начала расчета
        /// </summary>
        private DateTime timeStart;
        #endregion

        #region Свойства
        /// <summary>
        /// Задает или возвращает параметры алгоритма
        /// </summary>
        public IParameters Parameters               
        {
            set;
            get;
        }
        /// <summary>
        /// Задает или возвращает колличество городов
        /// </summary>
        public DataCitiesCollection Cities          
        {
            set;
            get;
        }


        /// <summary>
        /// Возвращает лист с отладочной информацией расчета
        /// </summary>
        public List<string> ResultInfo              
        {
            get
            {
                return _liStrInfo;
            }
        }

        /// <summary>
        /// Возвращает коллекцию городов, расположенных в порядке лучшего пути
        /// </summary>
        public Route ResultPath                     
        {
            get
            {
                return null;
            }
        }

        /// <summary>
        /// Возвращает время расчета алгоритма
        /// </summary>
        public TimeSpan ProcessTime                 
        {
            get
            {
                return _tsProcessTime;
            }
        }
        #endregion

        #region Методы (внутренние)
        private void Init(DataCitiesCollection cities, IParameters parameters)      
        {
            if (cities == null)
                throw new Exception("В алгоритме на определены города");
            if (parameters == null)
                throw new Exception("В алгоритме на определены параметры расчета");

            try
            {
                Parameters = (BnBParameters)parameters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


        }
        #endregion

        #region Методы (внешние)
        /// <summary>
        /// Запуск алгоритма Ветвей и границ
        /// </summary>
        public void Start()                                                             
        {
            this.Start(Cities, Parameters);
        }
        public void Start(DataCitiesCollection cities, IParameters parameters)          
        {
            Init(cities, parameters);

            timeStart = DateTime.Now;
            // Зпуск алгоритма
            ;
        }

        #endregion
    }
}
