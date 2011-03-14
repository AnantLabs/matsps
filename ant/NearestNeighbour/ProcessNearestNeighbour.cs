using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ant.AntAlgData;
using ant.NearestNeighbour;
using ant.CommonData;

namespace ant
{
    class ProcessNearestNeighbour
    {


        #region Конструкторы и Данные

        public delegate void ProgressChanged(int value);
        public event ProgressChanged eventProgressChanged;
  
        public ProcessNearestNeighbour() { }

        /// <summary>
        /// Переменная, в которой происходит алгоритм
        /// </summary>
        private NearestNeighbourTravelSalesman travelSalesmanNN;

        /// <summary>
        /// Стандартные результаты рассчета
        /// </summary>
        private List<string> _liResult = null;
        private Route _bestPath = null;
        /// <summary>
        /// Время рачета алгоритма
        /// </summary>
        private TimeSpan _tsProcessTime;

        private DateTime timeStart;
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
        public Route ResultPath    
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
        public DataCitiesCollection Cities        
        {
            set;
            get;
        }
        #endregion

        #region Методы
        /// <summary>
        /// Инициализируем данные
        /// </summary>
        private void Init(DataCitiesCollection cities, AntAlgDataParameters parameters)
        {
            if (cities == null)
                throw new Exception("В алгоритме на определены города");
            if (parameters == null)
                throw new Exception("В алгоритме на определены параметры расчета");
            travelSalesmanNN = new NearestNeighbourTravelSalesman(cities,parameters);
            travelSalesmanNN.eventProgressChanged += new EventHandler<NearestNeighbourChangesEventArgs>(ProgressChange);
            travelSalesmanNN.eventFinally += new EventHandler<EventArgs>(Finally);
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
        public void Start(DataCitiesCollection cities, AntAlgDataParameters parameters)
        {
            Init(cities, parameters);

            timeStart = DateTime.Now;
            travelSalesmanNN.Calculate();

           // travelSalesmanNN.SetParameters(parameters);
            //travelSalesmanNN.InitNeighbours(cities);
            //travelSalesmanNN.Calculate();
            // Результаты

        }
        #endregion

        #region Обработчики событий

        private void ProgressChange(object sender, NearestNeighbourChangesEventArgs e)
        {
            //пересылка сообщения
            if (eventProgressChanged != null) //проверяем наличие подписчиков
                eventProgressChanged((int)e.Percent);
        }

        private void Finally(object sender, EventArgs e)
        {
            // Результаты
            _liResult = travelSalesmanNN.ListTimeRoute;
            Route path = new Route( travelSalesmanNN.BestPath );
            _bestPath = path;

            _tsProcessTime = DateTime.Now - timeStart;

            OnFinallyCalculate(EventArgs.Empty);
        }

        /// <summary>
        /// Событие завершения вычислений
        /// </summary>
        public event EventHandler<EventArgs> eventFinally;
        protected virtual void OnFinallyCalculate(EventArgs e)
        {
            EventHandler<EventArgs> tmp = eventFinally;

            if (tmp != null)
                tmp(this, e);
        }

        #endregion
    }


}
