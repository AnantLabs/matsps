using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ant.AntAlgData;
using ant.AntAlgLogic;

namespace ant
{
    /// <summary>
    /// Процесс расчета
    /// </summary>
    class ProcessAnt
    {
        #region Конструкторы и Данные
        public ProcessAnt() { }

        /// <summary>
        /// Переменная, в которой происходит алгоритм
        /// </summary>
        private AntAlgTravelSalesman travelSalesmanAnt;

        /// <summary>
        /// Стандартные результаты рассчета
        /// </summary>
        private List<string> _liResult = null;
        private AntAlgDataCitiesCollection _bestPath = null;
        /// <summary>
        /// Время рачета алгоритма
        /// </summary>
        private TimeSpan _tsProcessTime; 
        #endregion



        #region Свойства
        /// <summary>
        /// Возвращает лист с дефолтными результатами расчета
        /// </summary>
        public List<string> ResultList                  
        {
            get {
                return _liResult;
            }
        }

        /// <summary>
        /// Возвращает коллекцию городов, расположенных в порядке лучшего пути
        /// </summary>
        public AntAlgDataCitiesCollection ResultPath    
        {
            get
            {
                return _bestPath;
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

        /// <summary>
        /// Задает или возвращает параметры алгоритма
        /// </summary>
        public AntAlgDataParameters Parameters          
        {
            set;
            get;
        }

        /// <summary>
        /// Задает или возвращает колличество городов
        /// </summary>
        public AntAlgDataCitiesCollection Cities        
        {
            set;
            get;
        }
        #endregion



        #region Методы
        /// <summary>
        /// Инициализируем данные
        /// </summary>
        private void Init(AntAlgDataCitiesCollection cities, AntAlgDataParameters parameters)         
        {
            if (cities == null)
                throw new Exception("В алгоритме на определены города");
            if (parameters == null)
                throw new Exception("В алгоритме на определены параметры расчета");
            travelSalesmanAnt = new AntAlgTravelSalesman(
                cities, parameters);            
        }

        /// <summary>
        /// Начать алгоритм расчета
        /// </summary>
        public void Start()                                                                                 
        {
            this.Start(Cities, Parameters);
        }
        /// <summary>
        /// Начать алгоритм расчета
        /// </summary>
        /// <param name="cities">Коллекция городов</param>
        /// <param name="parameters">Параметры расчета</param>
        public void Start(AntAlgDataCitiesCollection cities, AntAlgDataParameters parameters)               
        {
            Init(cities, parameters);

            DateTime timeStart = DateTime.Now;

            travelSalesmanAnt.Calculate();
            // Результаты
            _liResult = travelSalesmanAnt.ListTimeRoute;
            _bestPath = travelSalesmanAnt.BestPath;

            _tsProcessTime = DateTime.Now - timeStart;
        }
        #endregion
    }
}
