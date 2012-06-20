using System;
using System.Collections.Generic;
using System.Drawing;

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
        public PointF Position
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
        public Client(PointF position, double weight)
        {
            Position = position;
            Weight = weight;
        }
        #endregion

        public static Client CreateRandom()
        {
            double maxDistanceValue = 350.0;
            int  minWeight = 7;
            int maxWeight = 15;
 
            double x, y, w;
            x =  maxDistanceValue * r.NextDouble();
            y =  maxDistanceValue * r.NextDouble();
            w = (double)r.Next( minWeight, maxWeight) + r.NextDouble();

            PointF point =new PointF( (float)x, (float)y );
            Client current = new Client(point, w);

            return current;
        }
    }
}
