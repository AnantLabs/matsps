using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Представляет машину
    /// </summary>
    public class Car
    {
        #region Поля
        private static Random r = new Random();
        #endregion Поля

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
        public Double MaxCapacity
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

        /// <summary>
        /// Возвращает экземпляр машины по умолчанию
        /// </summary>
        public static Car Default
        {
            get 
            {
                Car car = new Car();
                    car.Capacity = 0;
                    car.MaxCapacity = 940;
                    car.MaxMiliage = 200;
                    //car.Miliage = 0;
                    car.MaxPassedClientsCount = 20;
                return car;
            }
        }
        #endregion

        #region Конструкторы
        public Car()
        {
        }
        #endregion

        #region Открытые методы
        /// <summary>
        /// Генерирует экземпляр случайной машины
        /// </summary>
        /// <returns></returns>
        public static Car CreateRandom()
        {
            Double maxCapacityValue = Default.MaxCapacity;
            Double maxMilliageValue = Default.MaxMiliage;
            int maxPassedClientsCountValue = Default.MaxPassedClientsCount;

            Double c, m;
            int p;
            c = maxCapacityValue * r.NextDouble();
            m = maxCapacityValue * r.NextDouble();
            p = r.Next(1, maxPassedClientsCountValue);

            Car current = new Car();
            current.MaxCapacity = c;
            current.MaxMiliage = m;
            current.MaxPassedClientsCount = p;

            return current;
        }
        /// <summary>
        /// Выполняет перерасчет пробега машины
        /// </summary>
        /// <returns></returns>
        public Double CalculateMilliage()
        {
            return 0;
        }
        #endregion
    }
}
