using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Представляет машину
    /// </summary>
    public class Car
    {
        #region Свойства
        /// <summary>
        /// Текущий пробег
        /// </summary>
        public Double Miliage
        {
            set;
            get;
        }
        /// <summary>
        /// Максимально допустимый пробег
        /// </summary>
        public Double MaxMiliage
        {
            set;
            get;
        }
        /// <summary>
        /// Тукущая заполненность
        /// </summary>
        public Double Capacity
        {
            set;
            get;
        }
        /// <summary>
        /// Максимально допустимая заполненность
        /// </summary>
        public int MaxCapacity
        {
            set;
            get;
        }
        /// <summary>
        /// Максимально допустимое количество пройденных клиентов
        /// </summary>
        public int MaxPassedClientsCount
        {
            set;
            get;
        }

        /// <summary>
        /// Задает или возвращает коллекцию клиентов, привязанную к машине
        /// </summary>
        public ClientsCollection Clients
        {
            set;
            get;
        }

        public static Car Default
        {
            get 
            {
                Car car = new Car();
                    car.Capacity = 0;
                    car.MaxCapacity = 940;
                    car.MaxMiliage = 200;
                    car.Miliage = 0;
                    car.MaxPassedClientsCount = 20;
                return car;
            }
        }
        #endregion
    }
}
