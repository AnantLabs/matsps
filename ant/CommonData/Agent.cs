using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace matsps.CommonData
{
    class Agent : IComparable<Agent>
    {
        #region Конструкторы и данные
        // Длина маршрута
        private double _dRouteLength;
        private Route _route;
        private List<bool> _liTaboo;

        public Agent()
        {
            _dRouteLength = 0;
            _route = null;
           // Маршрута-то ещё нет!
           // for (int i = 0; i < Route.Cities.Count; i++)
           // {
           //     _liTaboo.Add(false);// первоначальное заполнение нулями
           // }

        }

        #endregion

        #region Свойства
        /// <summary>
        /// Возвращает длину маршрута, задаётся свойством Route
        /// </summary>
        public double RouteLength
        {
            //set
            //{ _dRouteLength = value; }
            get
            { return _dRouteLength; }
        }

        /// <summary>
        /// Принимает(и задаёт длину) или возвращает маршрут
        /// </summary>
        public Route Route
        {
            set
            {
                _route = value;
                _dRouteLength = value.Length;// сразу устанавливаем длину маршрута
            }
            get { return _route; }
        }



        #endregion

        #region Методы

        /// <summary>
        /// Инициализировать список Табу
        /// </summary>
        public void TabooInit()
       {
            try
            {
                if(Route == null)
                { 
                    throw new Exception("В Агенте не задан маршрут!");
                }
                if (_liTaboo.Count > 0)
                { 
                    _liTaboo.Clear(); 
                }
                for (int i = 0; i < Route.Cities.Count; i++)
                {
                    _liTaboo.Add(false);// первоначальное заполнение нулями
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
        }

        /// <summary>
        /// Инициализировать список Табу
        /// </summary>
        /// <param name="CitiesCount">Количество городов</param>
        public void TabooInit(int CitiesCount)
        {
            try
            {
                //if (_liTaboo != null)
                //{
                //    if (_liTaboo.Count > 0)
                //    { _liTaboo.Clear(); }
                //}
                //else
                //{

                //}
                _liTaboo = new List<bool>();
                for (int i = 0; i < CitiesCount; i++)
                {
                    _liTaboo.Add(false);// первоначальное заполнение нулями
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
      }

        /// <summary>
        /// Установить Табу для определённого города
        /// </summary>
        /// <param name="index">Индекс</param>
        /// <param name="value">Значение табу(true значит, что город запрещён)</param>
        public void TabooSet(int index, bool value)
        {
            try { _liTaboo[index] = value; }
            catch (Exception ex)
            { throw new Exception(ex.Message + ex.StackTrace); }
        }

        /// <summary>
        ///  Узнать значение Табу для определённого города
        /// </summary>
        /// <param name="index">Индекс города</param>
        /// <returns>true - город запрещён, false - разрешён</returns>
        public bool TabooGet(int index)
        {
            try { return _liTaboo[index]; }
            catch (Exception ex)
            { throw new Exception(ex.Message + ex.StackTrace); }
        }

        /// <summary>
        /// IComparable interface implementation
        /// </summary>
        /// <param name="a">Agent to Compare</param>
        /// <returns>1 if current Route is better, -1 if not; otherwise - zero</returns>
        public int CompareTo(Agent a)
        {
            if (a.RouteLength > this.RouteLength)
            { return 1; }
            else if (a.RouteLength < this.RouteLength)
            { return -1; }
            return 0;
        }
        #endregion
    }
}
