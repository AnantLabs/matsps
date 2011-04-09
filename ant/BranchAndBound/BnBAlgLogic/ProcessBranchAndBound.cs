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
            Drawing = new Drawing();
            Drawing.Color = System.Drawing.Color.Orange;
        }
        #endregion

        #region Поля
        /// <summary>
        /// параметры прорисовки маршрута
        /// </summary>
        public Drawing Drawing;

        public delegate void ProgressChanged(object sender,int value, string label);
        public event ProgressChanged eventProgressChanged;     

        /// <summary>
        /// Экземпляр класса алгоритма
        /// </summary>
        private BranchAndBoundTravelSalesman _travelSalesmanBnB;

        /// <summary>
        /// Лист с отладочной информацией
        /// </summary>
        private List<string> _liStrInfo;

        /// <summary>
        /// Время начала расчета
        /// </summary>
        private DateTime timeStart;

        private Route _bestPath = null;
        private List<Route> _liBestPath = null;
        /// <summary>
        /// Время рачета алгоритма
        /// </summary>
        private TimeSpan _tsProcessTime;
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
                return _bestPath;
            }
        }
        /// <summary>
        /// Список лучших путей (только для чтения)
        /// </summary>
        public List<Route> ResultPathList       
        {
            get
            {
                return _liBestPath;
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
            _travelSalesmanBnB.eventProgressChanged += new EventHandler<BnBAlgChangesEventArgs>(ProgressChange);
            _travelSalesmanBnB.eventFinally += new EventHandler<EventArgs>(Finally);
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

        #region Обработчики событий

        private void ProgressChange(object sender, BnBAlgLogic.BnBAlgChangesEventArgs e)    
        {
            //пересылка сообщения
            if (eventProgressChanged != null) //проверяем наличие подписчиков
                eventProgressChanged(this, (int)e.Percent, e.Label);
        }

        private void Finally(object sender, EventArgs e)                                    
        {
            // Результаты            
            Route path = new Route( _travelSalesmanBnB.BestPath, "ветвей и границ");
            _liBestPath = new List<Route>();            
            _bestPath = path;
            List<CitiesCollection> ct = _travelSalesmanBnB.BestPathList;
            foreach (CitiesCollection singleCollection in ct)
            {
                Route r = new Route(singleCollection, "ветвей и границ");
                r.Drawing.Color = this.Drawing.Color;
                _liBestPath.Add( r );
            }

            _liStrInfo = new List<string>();
            _liStrInfo.Add(" ");
            //_bestPath.Drawing = this.Drawing;

            //Копирование параметров прорисовки
            _bestPath.Drawing.Color = this.Drawing.Color;
            _bestPath.Drawing.Opacity = this.Drawing.Opacity;
            _bestPath.Drawing.Visible = this.Drawing.Visible;
            //

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
