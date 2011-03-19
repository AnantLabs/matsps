using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using matsps.AntAlgData;               // данные алгоритма муравья
using matsps.AntAlgLogic;              // логика алгоритма Муравья
using matsps.CommonData;               // совместные данные
using matsps.Parameters;               // параметры алгоритмов

namespace matsps
{
    /// <summary>
    /// Процесс расчета методом Муравьиной колонии
    /// </summary>
    class ProcessAnt : IProcessAlgorithm
    {
        #region Конструкторы и Данные

        public delegate void ProgressChanged(int value);        
        public event ProgressChanged eventProgressChanged;     

        public ProcessAnt() 
        {
        }
        /// <summary>
        /// Переменная, в которой происходит алгоритм
        /// </summary>
        private AntAlgTravelSalesman travelSalesmanAnt;
        /// <summary>
        /// Стандартные результаты рассчета
        /// </summary>
        private List<string> _liResult = null;
        private Route _bestPath = null;
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


        #region Методы (внутренние)
        /// <summary>
        /// Инициализируем данные
        /// </summary>
        private void Init(CitiesCollection cities, IParameters parameters)         
        {
            if (cities == null)
                throw new Exception("В алгоритме на определены города");
            if (parameters == null)
                throw new Exception("В алгоритме на определены параметры расчета");

            try
            {
                Parameters = (AntParameters)parameters;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            travelSalesmanAnt = new AntAlgTravelSalesman(cities, (AntParameters)Parameters);
            travelSalesmanAnt.eventProgressChanged += new EventHandler<AntAlgChangesEventArgs>(ProgressChange);
            travelSalesmanAnt.eventFinally += new EventHandler<EventArgs>(Finally);
        }
        #endregion

        #region Методы (внешние)
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
            Init(cities, parameters);

            timeStart = DateTime.Now;

            travelSalesmanAnt.Calculate();
        }

        /// <summary>
        /// Продолжить алгоритм расчета
        /// </summary>
        public void Continue()                                                                  
        {
            travelSalesmanAnt.CalculateContinue();
        }
        #endregion

        #region Обработчики событий

        private void ProgressChange(object sender, AntAlgLogic.AntAlgChangesEventArgs e)         
        {
                //пересылка сообщения
                if (eventProgressChanged != null) //проверяем наличие подписчиков
                    eventProgressChanged((int)e.Percent);
        }

        private void Finally(object sender, EventArgs e)                                         
        {
            // Результаты
            _liResult = travelSalesmanAnt.ListTimeRoute;
            _bestPath = travelSalesmanAnt.BestPath;            

            _tsProcessTime = DateTime.Now - timeStart;
            _bestPath.СalcTime = _tsProcessTime;

            OnFinallyCalculate(new EventArgs());
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
