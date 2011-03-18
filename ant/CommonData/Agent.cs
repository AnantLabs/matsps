using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ant.CommonData
{
    class Agent
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
            for (int i = 0; i < Route.Cities.Count; i++)
            {
                _liTaboo.Add(false);// первоначальное заполнение нулями
            }
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
            get { return Route; }
        }



        #endregion

        #region Методы

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
        #endregion
    }
}
