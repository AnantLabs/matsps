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
    class Process
    {
        #region Конструкторы и Данные
        public Process() { }

        /// <summary>
        /// Города
        /// </summary>
        private AntAlgData.AntAlgDataCitiesCollection Cities;

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
        #endregion



        #region Методы
        /// <summary>
        /// Инициализируем данные
        /// </summary>
        private void Init()         
        {
            // Параметры расчета
            AntAlgData.AntAlgDataParameters param = new ant.AntAlgData.AntAlgDataParameters();

            // Создаем Города
            Cities = new ant.AntAlgData.AntAlgDataCitiesCollection(param.MaxCities);
            //Cities.InitPheromone = param.InitPheromone;
            Cities.MaxDistance = param.MaxDistance;
            Cities.InitCitiesRandom();

            travelSalesmanAnt = new AntAlgTravelSalesman(
                Cities, param);
            
        }
        /// <summary>
        /// Начать алгоритм расчета
        /// </summary>
        public void Start()         
        {
            Init();

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
