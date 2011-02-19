using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ant.AntAlgData
{
    /// <summary>
    /// Класс с параметрами расчета
    /// </summary>
    class AntAlgDataParameters
    {
        #region Конструторы и Данные
        public AntAlgDataParameters()
        {
            MaxCities = 50;
            MaxDistance = 100;

            _iMaxTour = MaxCities * MaxDistance;
            int iMaxTours = 20;
            _iMaxTime = iMaxTours * MaxCities;

            InitPheromone = 1.0 / MaxCities;
            MaxAnts = 50;

            ALPHA = 1.0;
            BETA = 5.0;

            RHO = 0.5;
            QVAL = 100;
        }
        #endregion

        #region Свойства
        public int MaxDistance                  
        {
            set;
            get;
        }
        public int MaxCities                    
        {
            set;
            get;
        }
        private int _iMaxTour = 0;
        /// <summary>
        /// Максимально возможный тур (Кол. городов * Макс. дистанцию)
        /// </summary>
        public int MaxTour                      
        {
            get
            {
                return
                    _iMaxTour;
            }
        }        
        public int MaxAnts                      
        {
            set;
            get;
        }
        private int _iMaxTime = 0;
        public int MaxTime                      
        {
            get
            {
                return
                    _iMaxTime;
            }
        }

        public double InitPheromone             
        {
            set;
            get;
        }

        public double ALPHA                     
        {
            set;
            get;
        }
        public double BETA                      
        {
            set;
            get;
        }

        /// <summary>
        /// Интенсивность / испарение
        /// </summary>
        public double RHO                       
        {
            set;
            get;
        }

        /// <summary>
        /// Количество фермента, иставленного на пути
        /// </summary>  
        public int QVAL                         
        {
            set;
            get;
        }
        #endregion
    }
}
