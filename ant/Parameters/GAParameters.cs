using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.Parameters
{
    /// <summary>
    ///  Параметры генетического алгоритма
    /// </summary>
    class GAParameters: IParameters
    {
        #region Конструторы

        public GAParameters()
        {
            MaxDistance = 100;
            GenerationsCount = 1000;
            Population = 100;
            _iMutationPercent = 5;
            SurviversPercent = 50;
            _iCitySwitchMutationProbability = 1;
            _iIsolatedChainMutationProbability = 1;
            _iNewAgentMutationProbability = 1;
            //AllowAllMutations = true;
        }
        public GAParameters(int CCount) 
            :this()
        {
            this.CitiesCount = CCount;
        }

        #endregion

        #region Переменные

        private int _iMutationPercent;
        private int _iSurviversPercent;
        private int _iCitySwitchMutationProbability;
        private int _iIsolatedChainMutationProbability;
        private int _iNewAgentMutationProbability;

        #endregion Переменные

        #region Свойства
        /// <summary>
        ///  Максимальное расстояние между городами
        /// </summary>
        public int MaxDistance                  
        {
            set;
            get;
        }

        /// <summary>
        /// Количество создаваемых поколений
        /// </summary>
        public int GenerationsCount
        {
            set;
            get;
        }

        /// <summary>
        /// Количество городов
        /// </summary>
        public int CitiesCount
        {
            set;
            get;
        }

        /// <summary>
        /// Популяция(количество Агентов)
        /// </summary>
        public int Population
        {
            set;
            get;
        }

        public int SurviversCount
        {
            set;
            get;
        }

        /// <summary>
        /// Процент выживающих агентов в поколении
        /// </summary>
        public int SurviversPercent
        {
            set
            {
                if (value <= 0 || value >= 100)
                {
                    _iSurviversPercent = 50;
                    throw new Exception("Неверное значение выживающих агентов. Будет выставлено значение по умолчанию(50).");
                }
                else
                {
                    _iSurviversPercent = value;
                }
                SurviversCount = (int)(Population * ((double)_iSurviversPercent / 100.0));
            }
            get
            {
                return _iSurviversPercent;
            }
        }

        #endregion

        #region Свойства мутаций
        /*
        public bool AllowAllMutations
        {
            set;
            get;
        }
        */
        
        // Процент мутации особей в поколении
        public int MutationPercent
        {
            set
            {
                if (value < 0 || value > 100)
                {
                    _iMutationPercent = 5;
                    throw new Exception("Неверное значение мутации. Будет выставлено значение по умолчанию(5).");
                }
                else
                {
                    _iMutationPercent = value;
                }
            }
            get
            {
                return _iMutationPercent;
            }
        }

        public int CitySwitchMutationProbability
        {
            set
            {
                if (value < 0)
                    value = 0;
                _iCitySwitchMutationProbability = value;
            }
            get
            {
                return _iCitySwitchMutationProbability;
            }
        }

        public int IsolatedChainMutationProbability
        {
            set
            {
                if (value < 0)
                    value = 0;
                _iIsolatedChainMutationProbability = value;
            }
            get
            {
                return _iIsolatedChainMutationProbability;
            }
        }

        public int NewAgentMutationProbability
        {
            set
            {
                if (value < 0)
                    value = 0;
                _iNewAgentMutationProbability = value;
            }
            get
            {
                return _iNewAgentMutationProbability;
            }
        }

        #endregion Свойства мутаций
    }
}
