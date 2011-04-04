using System;
using System.Collections.Generic;
using System.Text;

using matsps.CommonData;                   // классы совместрых данных
using matsps.BranchAndBound.BnBAlgData;    // классы с данными алгоритма
using matsps.Parameters;                   // классы параметров


namespace matsps.BranchAndBound.BnBAlgLogic
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
        /// Экземпляр класса алгоритма
        /// </summary>
        private BranchAndBoundTravelSalesman _travelSalesmanBnB;

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
        public CitiesCollection Cities              
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
        private void Init(CitiesCollection cities, IParameters parameters)      
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

            _travelSalesmanBnB = new BranchAndBoundTravelSalesman(cities, (BnBParameters)parameters);
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
        public void Start(CitiesCollection cities, IParameters parameters)          
        {
            Init(cities, parameters);

            timeStart = DateTime.Now;
            // Зпуск алгоритма
            _travelSalesmanBnB.Calculate();
        }

        #endregion
    }
}
