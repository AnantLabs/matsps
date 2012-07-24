using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Linq;

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
            return Items.Select(it => it.GetMilliage()).Sum();
        }
        /// <summary>
        /// Возвращает суммарную заполненность всех автомобилей
        /// </summary>
        /// <returns>Заполненность</returns>
        public double GetSumCapacity()
        {
            return Items.Select(it => it.GetCapacity() ).Sum();
        }
        /// <summary>
        /// Возвращает суммарное колличество городов, пройденных всеми машинами
        /// </summary>
        /// <returns>Количество городов</returns>
        public double GetSumPassedCitiesCount()
        {
            return Items.Select(it => it.Clients.Count).Sum();
        }
        #endregion
    }
}
