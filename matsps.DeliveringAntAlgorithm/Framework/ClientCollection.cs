using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Collections.ObjectModel;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Представляет коллекцию клиентов
    /// </summary>
    public class ClientsCollection : Collection<Client>
    {
        #region Cвойства
        /// <summary>
        /// Задает или возвращает матрицу расстояний мужду городами
        /// </summary>
        public Double[,] Distancies
        {
            set;
            get;
        }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает новый экземпляр коллекции городов по умолчанию
        /// </summary>
        public ClientsCollection()
        {
            Distancies = new Double[0, 0];
            Distancies.Initialize();
        }
        #endregion

        #region Переопределение методов коллекции
        protected override void InsertItem(int index, Client item)
        {
            base.InsertItem(index, item);
            UpdateDistancies();
        }

        protected override void SetItem(int index, Client newItem)
        {
            base.SetItem(index, newItem);
            UpdateDistancies();
        }


        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            UpdateDistancies();
        }


        protected override void ClearItems()
        {
            base.ClearItems();
            UpdateDistancies();
        }
        #endregion Переопределение методов коллекции

        #region Открытые методы
        /// <summary>
        /// Выполняет генерацию коллекции случайных городов
        /// </summary>
        /// <param name="count">Колличество городов</param>
        /// <param name="maxDistance">Максимальное расстояние между городами</param>
        /// <returns>Коллекция городов</returns>
        public static ClientsCollection GenerateRandom(int count, int maxDistance)
        {
            ClientsCollection citiesCollection = new ClientsCollection();
            citiesCollection.Distancies = new Double[0, 0];
            citiesCollection.Distancies.Initialize();

            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                int x = -1, y = -1;
                x = rand.Next(0, maxDistance);
                y = rand.Next(0, maxDistance);
                if (i != 0)
                {
                    for (int j = i - 1; j > 0; j--)
                    {
                        // совпадение с уже существующими координатами
                        if (x == citiesCollection.Items[j].Position.X && y == citiesCollection.Items[j].Position.Y)
                        {
                            x = rand.Next(0, maxDistance);
                            y = rand.Next(0, maxDistance);
                            j = i - 1;
                        }
                    }
                }
                citiesCollection.Items.Add(new Client(new Point(x, y), x + y));
            }
            citiesCollection.UpdateDistancies();
            return citiesCollection;
        }

        /// <summary>
        /// Выполняет рассчет матрицы расстояний для заданной коллекции клиентов
        /// </summary>
        /// <param name="clients">Коллекция клиентов</param>
        /// <returns>Матрица растояний</returns>
        public static double[,] CalculateDistance(IList<Client> clients)
        {
            int count = clients.Count;
            double[,] dist = new double[count, count];
            for (int from = 0; from < count; from++)
                for (int to = 0; to < count; to++)
                {
                    if (to != from && dist[from, to] == 0.0)
                    {
                        double xd = Math.Abs(clients[from].Position.X - clients[to].Position.X);
                        double yd = Math.Abs(clients[from].Position.Y - clients[to].Position.Y);
                        dist[from, to] = Math.Sqrt(xd * xd + yd * yd);
                        dist[to, from] = dist[from, to];
                    }
                }
            return dist;
        }
        #endregion

        #region Закрытые методы
        /// <summary>
        /// Выполняет обновление матрицы расстояний
        /// </summary>
        private void UpdateDistancies()
        {
            Distancies = new double[Items.Count, Items.Count];
            Distancies.Initialize();

            Distancies = CalculateDistance(Items);
        }
        #endregion

    }
}
