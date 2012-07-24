using System;
using System.Collections.Generic;
using System.Text;

using matsps.CommonData;

namespace matsps.Parameters
{
    /// <summary>
    /// Класс с параметрами расчета алгоритма Муравья
    /// </summary>
    public class AntParameters : IParameters
    {
        #region Конструторы и Данные
        public AntParameters()                  
        {
            MaxCities = 50;
            MaxDistance = 100;

            _iMaxTour = MaxCities * MaxDistance;

            EndType = AntAlgorithmEndType.Iteration;
            CountRepeatCуcles = 20;
            CountConvergence = 4;
            _iMaxTime = CountRepeatCуcles * MaxCities;

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
                _iMaxTour = MaxCities * MaxDistance;
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
        /// <summary>
        /// Количество городов * Количество проходов алгоритма
        /// </summary>
        public int MaxTime                      
        {
            get
            {
                _iMaxTime = CountRepeatCуcles * MaxCities;
                return
                    _iMaxTime;
            }
        }

        /// <summary>=======================================================================
        ///                             Завершение алгоритма
        /// </summary>======================================================================
                
        public AntAlgorithmEndType EndType      
        {
            set;
            get;
        }   
        /// <summary>
        /// Количество проходов алгоритма
        /// </summary>
        public int CountRepeatCуcles            
        {
            set;
            get;
        }
        /// <summary>
        /// Количество повторений лучшего пути для обеспечения сходимости
        /// </summary>
        public int CountConvergence             
        {
            set;
            get;
        }
        ///=================================================================================

        /// <summary>
        /// Начальное количество феромонов на каждом городе (1/количество городов)
        /// </summary>
        public double InitPheromone             
        {
            get
            {
                return 1.0 / MaxCities;
            }
        }

        /// <summary>
        /// Коэфф. отвечает за учет фермента в вероятности прохода муравья в какой-то город
        /// </summary>
        public double ALPHA                     
        {
            set;
            get;
        }
        /// <summary>
        /// Коэфф. отвечает за учет длины груни в вероятности прохода муравья в какой-то город
        /// </summary>
        public double BETA                      
        {
            set;
            get;
        }

        /// <summary>
        /// Коэфф. интенсивности / испарение феромона (рекомендуется >0.5)
        /// </summary>
        public double RHO                       
        {
            set;
            get;
        }

        /// <summary>
        /// Количество фермента, оставленного на пути
        /// </summary>  
        public int QVAL                         
        {
            set;
            get;
        }
        #endregion
    }

    /// <summary>
    /// Тип завершения Алгоритма муравья
    /// </summary>
    public enum AntAlgorithmEndType                    
    {
        /// <summary>
        /// По количеству проходов цикла
        /// </summary>
        Iteration,
        /// <summary>
        /// По количеству элементов для схождения
        /// </summary>
        Convergence
    };
}
