﻿using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

using matsps.CommonData;
using matsps.AntAlgorithm.AntAlgData;
using matsps.Parameters;



namespace matsps.GeneticAlgorithm
{
    class ProcessGeneticAlgorithm: IProcessAlgorithm
    {
        #region Конструкторы и Данные

        public delegate void ProgressChanged(object sender, int value, string label);
        public event ProgressChanged eventProgressChanged;
        public Drawing Drawing;

        public ProcessGeneticAlgorithm() 
        {
            Drawing = new Drawing();
            Drawing.Color = System.Drawing.Color.Chocolate;
        }

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
            travelSalesmanGA = new GeneticAlgorithmTravelSalesman(cities, (GAParameters)parameters);
            travelSalesmanGA.eventProgressChanged += new EventHandler<GeneticAlgorithmChangesEventArgs>(ProgressChange);
            travelSalesmanGA.eventFinally += new EventHandler<EventArgs>(Finally);
        }

        /// <summary>
        /// Начать алгоритм расчета
        /// </summary>
        public void StartAsync()
        {
            this.StartAsync(Cities, Parameters);
        }
        /// <summary>
        /// Начать алгоритм расчета
        /// </summary>
        /// <param name="cities">Коллекция городов</param>
        /// <param name="parameters">Параметры расчета</param>
        public void StartAsync(CitiesCollection cities, IParameters parameters)
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
                eventProgressChanged(this, (int)e.Percent, "%");
        }

        private void Finally(object sender, EventArgs e)
        {
            // Результаты
            _liResult = travelSalesmanGA.ListTimeRoute;
           // Route path = new Route(travelSalesmanGA.BestPath,"ближайшего соседа");
            _bestPath = travelSalesmanGA.BestPath();
            //_bestPath.Name = "генетический алгоритм";
            
            _tsProcessTime = DateTime.Now - timeStart;

            travelSalesmanGA.eventProgressChanged -= new EventHandler<GeneticAlgorithmChangesEventArgs>(ProgressChange);

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
