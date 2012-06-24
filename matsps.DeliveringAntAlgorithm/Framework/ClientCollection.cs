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
        public Distance Distancies
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
            Distancies = new Distance();
            Distancies.CalcDistanceLine(this.Items, null);
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
            citiesCollection.Distancies = new Distance();

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
                citiesCollection.Items.Add(new Client(new PointD(x, y), x + y));
            }

            citiesCollection.UpdateDistancies();
            return citiesCollection;
        }

        /// <summary>
        /// Полное клонирование коллекции
        /// </summary>
        /// <returns></returns>
        public ClientsCollection FullClone()
        {
            ClientsCollection clone = new ClientsCollection();
            foreach (Client item in this.Items)
            {
                clone.Add(item.FullClone());
            }


        }
        #endregion

        #region Закрытые методы
        /// <summary>
        /// Выполняет обновление матрицы расстояний
        /// </summary>
        private void UpdateDistancies()
        {
            Distancies.CalcDistanceLine(this.Items, null);
        }
        #endregion


    }
}
