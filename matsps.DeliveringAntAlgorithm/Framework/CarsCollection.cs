using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Представляет коллекцию машин
    /// </summary>
    public class CarsCollection : Collection<Car>
    {
        #region Конструкторы
        /// <summary>
        /// Создает новый экземпляр коллекции машин по умолчанию
        /// </summary>
        public CarsCollection()
        {
        }
        #endregion

        #region Открытые методы
        /// <summary>
        /// Возвращает суммарный пробег всех машин в коллекции
        /// </summary>
        /// <returns>Пробег</returns>
        public Double GetSumMilliage()
        {
            Double sumMilliage = 0;
            foreach(Car car in Items)
                sumMilliage += car.Miliage;
            return sumMilliage;
        }
        /// <summary>
        /// Возвращает суммарную заполненность всех автомобилей
        /// </summary>
        /// <returns>Заполненность</returns>
        public double GetSumCapacity()
        {
            double sumCapacity = 0;
            foreach (Car car in Items)
                sumCapacity += car.Capacity;
            return sumCapacity;
        }
        /// <summary>
        /// Возвращает суммарное колличество городов, пройденных всеми машинами
        /// </summary>
        /// <returns>Количество городов</returns>
        public double GetSumPassedCitiesCount()
        {
            double sumPassedCitiesCount = 0;
            foreach (Car car in Items)
                sumPassedCitiesCount += car.Capacity;
            return sumPassedCitiesCount;
        }
        #endregion
    }
}
