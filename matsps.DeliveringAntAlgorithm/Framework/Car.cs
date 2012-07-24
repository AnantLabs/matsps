using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Data;

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
        /// Максимально допустимый пробег
        /// </summary>
        public Double MaxMiliage
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
        /// Матрица расстояний между клиентами и точка старта (финиша)
        /// </summary>
        public Distance Distance
        {
            set; get;
        }

        /// <summary>
        /// Возвращает экземпляр машины по умолчанию
        /// </summary>
        public static Car Default
        {
            get 
            {
                Car car = new Car();
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
        public Double GetMilliage()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Пересчитывает заполненность атомобиля по коллекции клиентов
        /// </summary>
        /// <returns></returns>
        public Double GetCapacity()
        {
            return Clients.Select( c=> c.Weight ).Sum();
        }

        /// <summary>
        /// Проверяет на добавление клиента к машине. Пытается добавить, если получается
        /// </summary>
        /// <param name="candidateClient">Кандидат на добавление</param>
        /// <returns>true - добавить удалось, false - добавить не удалось. Машина заполнена</returns>
        public bool TryAddCandidate(Client candidateClient)
        {
            // проверка на добавление точки
            // делаем копию текущей машины
            Car copyCar = this.FullClone();

            copyCar.Clients.Add(candidateClient);
            // 1. Проверка по количеству
            if (copyCar.Clients.Count > copyCar.MaxPassedClientsCount)
                return false;

            // 2. Проверка по заполненности
            if (copyCar.GetCapacity() > MaxCapacity)
                return false;

            // 3. Проверка на километраж


            throw new NotImplementedException();
        }

        /// <summary>
        /// Полное клонирование машины
        /// </summary>
        /// <returns></returns>
        private Car FullClone()
        {
            Car clone = new Car();
            clone.Clients = this.Clients;
            clone.Distance = this.Distance;

            clone.MaxCapacity = this.MaxCapacity;
            clone.MaxMiliage = this.MaxMiliage;
            clone.MaxPassedClientsCount = this.MaxPassedClientsCount;

            return clone;
        }
        #endregion

    }
}
