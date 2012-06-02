using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.CommonData
{
    /// <summary>
    /// Хранит данные о координатах города
    /// </summary>
    public class City : ICloneable
    {
        #region Конструктор и Данные
        public City():this(0,0)        
        {}
        public City(int x, int y)      
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

        #region interfaces implementation
        public object Clone()
        {
            City tmpcity = new City();
            tmpcity._x = this._x;
            tmpcity._y = this._y;
            tmpcity.Index = this.Index;
            return tmpcity;
        }
        #endregion interfaces implementation
    }
}
