using System;
using System.Collections.Generic;
using System.Text;

namespace ant.CommonData
{
    /// <summary>
    /// Хранит данные о координатах города
    /// </summary>
    class DataCity
    {
        #region Конструктор и Данные
        public DataCity():this(0,0)        
        {}
        public DataCity(int x, int y)      
        {
            X = x;
            Y = y;
        }

    
        /// <summary>
        /// Координата по горизонтали
        /// </summary>
        private int _x;
        /// <summary>
        /// Координата по вертикале
        /// </summary>
        private int _y;
        #endregion

        #region Свойства
        public int X            
        {
            set 
            {
                _x = value;
            }
            get 
            {
                return _x;
            }
        }
        public int Y            
        {
            set {
                _y = value;
            }
            get {
                return _y;
            }
        }

        /// <summary>
        /// Порядковый номер города в массиве
        /// </summary>
        public int Index        
        {
            set;
            get;
        }
        #endregion
    }
}
