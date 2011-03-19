using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using matsps.CommonData;
using matsps.AntAlgData;
using matsps.Parameters;



namespace matsps.GeneticAlgorithm
{
    class ProcessGeneticAlgorithm: IProcessAlgorithm
    {
        #region Конструкторы и Данные

        public delegate void ProgressChanged(int value);
        public event ProgressChanged eventProgressChanged;

        public ProcessGeneticAlgorithm() { }

        /// <summary>
        /// Переменная, в которой происходит алгоритм
        /// </summary>
        private GeneticAlgorithmTravelSalesman travelSalesmanGA;

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
        public List<string> ResultInfo                  
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
        #endregion

        #region Методы
        /// <summary>
        /// Инициализируем данные
        /// </summary>
        private void Init(CitiesCollection cities, IParameters parameters)
        {
            if (cities == null)
                throw new Exception("В алгоритме на определены города");
            if (parameters == null)
                throw new Exception("В алгоритме на определены параметры расчета");
            travelSalesmanGA = new GeneticAlgorithmTravelSalesman(cities, (AntParameters)parameters);
            travelSalesmanGA.eventProgressChanged += new EventHandler<GeneticAlgorithmChangesEventArgs>(ProgressChange);
            travelSalesmanGA.eventFinally += new EventHandler<EventArgs>(Finally);
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
        public void Start(CitiesCollection cities, IParameters parameters)
        {
            this.Init(cities, parameters);

            timeStart = DateTime.Now;
            travelSalesmanGA.Calculate();

           // travelSalesmanNN.SetParameters(parameters);
            //travelSalesmanNN.InitNeighbours(cities);
            //travelSalesmanNN.Calculate();
            // Результаты

        }
        #endregion

        #region Обработчики событий

        private void ProgressChange(object sender, GeneticAlgorithmChangesEventArgs e)
        {
            //пересылка сообщения
            if (eventProgressChanged != null) //проверяем наличие подписчиков
                eventProgressChanged((int)e.Percent);
        }

        private void Finally(object sender, EventArgs e)
        {
            // Результаты
            _liResult = travelSalesmanGA.ListTimeRoute;
           // Route path = new Route(travelSalesmanGA.BestPath,"ближайшего соседа");
            _bestPath = travelSalesmanGA.BestPath();
            //_bestPath.Name = "генетический алгоритм";

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
