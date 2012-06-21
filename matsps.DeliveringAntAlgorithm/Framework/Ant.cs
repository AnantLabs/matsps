using System;
using System.Collections.Generic;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    /// <summary>
    /// Представляет муравья
    /// </summary>
    public class Ant
    {
        #region Свойства
        /// <summary>
        /// Задает или возвращает коллекцию машин
        /// </summary>
        public CarsCollection Cars
        {
            set;
            get;
        }
        /// <summary>
        /// Задает или возвращает коллекцию городов
        /// </summary>
        public ClientsCollection Clients
        {
            set;
            get;
        }
        /// <summary>
        /// Задает или возвращает индекс текущего города, в котором сецчас муравей
        /// </summary>
        /// 
        public int CurrenClientIndex
        {
            set;
            get;
        }
        /// <summary>
        /// Задает или возвращает индекс текущей машины
        /// </summary>
        public int CurrentCarIndex
        {
            set;
            get;
        }
        /// <summary>
        /// Задает или возвращает матрицу феромонов
        /// </summary>
        public Pheromones Pheromones
        {
            set;
            get;
        }
        /// <summary>
        /// Задает или возвращает список-табу(содержит индексы городов)
        /// </summary>
        public List<int> TabuIndexes
        {
            set;
            get;
        }
        /// <summary>
        /// Индекс текущего клиента
        /// </summary>
        public int CurrentClientIndex
        {
            set;
            get;
        }
        /// <summary>
        /// Параметры алгоритма
        /// </summary>
        public Parameters Parameters
        {
            set;
            get;
        }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает новый экземпляр муравья с заданными параметрами
        /// </summary>
        /// <param name="carsCollection">Ссылка на коллекцию машин</param>
        /// <param name="citiesCollection">Ссылка на коллекцию городов</param>
        public Ant(CarsCollection carsCollection, ClientsCollection citiesCollection, Pheromones pheromones, Parameters parameters)
        {
            Cars = carsCollection;
            Clients = citiesCollection;
            Pheromones = pheromones;
            Parameters = parameters;
        }
        #endregion

        #region Закрытые методы
        /// <summary>
        /// Вычисляет вероятность перехода из одного города в другой
        /// </summary>
        /// <param name="from">Откуда</param>
        /// <param name="to">Куда</param>
        /// <returns>Значение вероятности</returns>
        private Double TransitionProbability(int from, int to)
        {
            //Рассчет суммы феромонов на всех ребрах
            Double sumPheromone = 0;
            for (int i = 0; i < Cars.Count; i++)
            {
                for (int j = 0; j < Clients.Count; j++)
                    sumPheromone += Pheromones.PheromonesMatrix[i, j];
            }
            //Расчет вероятности перехода
            Double probability = Math.Pow(Pheromones.PheromonesMatrix[CurrentCarIndex, to], Parameters.Alpha) * (1 / Math.Pow(Clients[to].Weight, Parameters.Beta));
                   probability = probability / sumPheromone * (1 / Math.Pow(Clients[to].Weight, Parameters.Beta));
            return probability;
        }
        
        private double GetSRand(int x)
        {
            Random rnd = new Random();
            return (double)rnd.Next() / (double)Int32.MaxValue;
        }
        #endregion

        #region Открытые методы

        /// <summary>
        /// Определяет индекс следующего города, в который перейдет муравей
        /// </summary>
        /// <returns>Индекс города</returns>
        public int GetNextClientIndex()
        {
            int to = 0; //Индекс города, в который переходим
            do
            {
                Double rand = GetSRand(1);
                //Вычисление вероятности перехода из текущего города в следующий
                Double probability = TransitionProbability(CurrentClientIndex, to);
                if (rand < probability)
                    return to;
                to++;
            }
            while (true);
        }
        /// <summary>
        /// Выполняет переход у городу с указанным индексом
        /// </summary>
        public void GoToNextCity()
        {
            //Делаем следующий выбранный город текущим
            CurrenClientIndex = GetNextClientIndex();
        }
        #endregion       
    }
}
