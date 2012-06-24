using System;
using System.Collections.Generic;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Представляет точку
    /// </summary>
    public class Client
    {
        #region Поля
        private static Random r = new Random();     
        #endregion Поля

        #region Свойства
        /// <summary>
        /// Координаты 
        /// </summary>
        public PointD Position
        {
            set;
            get;
        }
        /// <summary>
        /// Уникальный индекс
        /// </summary>
        public int Index
        {
            set;
            get;
        }
        /// <summary>
        /// Вес(количество товара, которое нужно доставить клиенту)
        /// </summary>
        public double Weight
        {
            set;
            get;
        }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает новый экземпляр клиента
        /// </summary>
        /// <param name="position">Координаты</param>
        /// <param name="weight">Вес(количество товара, которое нужно доставить в данный город)</param>
        public Client(PointD position, double weight)
        {
            Position = position;
            Weight = weight;
        }
        #endregion

        #region Открытые методы
        /// <summary>
        /// Генерирует экземпляр случайного города
        /// </summary>
        /// <returns></returns>
        public static Client CreateRandom()
        {
            double maxDistanceValue = 350.0;
            int  minWeight = 7;
            int maxWeight = 15;
 
            double x, y, w;
            x =  maxDistanceValue * r.NextDouble();
            y =  maxDistanceValue * r.NextDouble();
            w = (double)r.Next( minWeight, maxWeight) + r.NextDouble();

            PointD point =new PointD( x, y );
            Client current = new Client(point, w);

            return current;
        }
        public Client FullClone()
        {
            Client clone = new Client(this.Position, this.Weight);
            clone.Index = this.Index;

            return clone;
        }
        #endregion

    }
}
