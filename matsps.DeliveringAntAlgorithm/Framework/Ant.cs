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
        #region Поля
        public static Random _rnd = new Random();

        /// <summary>
        /// Индекс кандидата на добавление (для вставки, если кандидат не проходит)
        /// </summary>
        private int _lastCandidateClientIndex = -1;
        #endregion  Поля

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
        /// Список доступных для добавления клиентов
        /// </summary>
        public ClientsCollection EnableClients
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
            EnableClients = Clients.FullClone();
            TabuIndexes = new List<int>();
            Pheromones = pheromones;
            Parameters = parameters;
        }
        #endregion

        #region Закрытые методы
        /// <summary>
        /// Вычисляет вероятность перехода из одного города в другой
        /// </summary>
        /// <param name="car">Откуда</param>
        /// <param name="client">Куда</param>
        /// <returns>Значение вероятности</returns>
        private Double TransitionProbability(int car, int client)
        {
            //Рассчет суммы феромонов на всех ребрах
            Double sumPheromone = 0;
            for (int i = 0; i < Cars.Count; i++)
            {
                for (int j = 0; j < Clients.Count; j++)
                    sumPheromone += Pheromones.PheromonesMatrix[i, j] * Clients[j].Weight;
            }
            //Расчет вероятности перехода
            Double probability = Math.Pow(Pheromones.PheromonesMatrix[CurrentCarIndex, client], Parameters.Alpha) * Math.Pow(Clients[client].Weight, Parameters.Beta);
                   probability = probability / sumPheromone;
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
        /// Выполняет переход к городу с указанным индексом
        /// </summary>
        //public void GoToNextCity()
        //{
        //    //Делаем следующий выбранный город текущим
        //    CurrenClientIndex = GetNextClientIndex();
        //}

        public void SetStartPoint()
        {
            int countClient = Clients.Count;
            CurrentClientIndex = _rnd.Next(0, countClient - 1);
            TabuIndexes.Add(CurrentClientIndex);
        }

        /// <summary>
        /// Возвращает случайного клиента из списка доступных клиентов. Клиент не удаляется из коллекции
        /// </summary>
        /// <returns></returns>
        public Client NextCandidate()
        {
            int enableClientCount = EnableClients.Count;
            int rndIndex = _rnd.Next(0, enableClientCount);

            return EnableClients[rndIndex];
        }

        public void AddToTabu(Client candidateClient)
        {
            EnableClients.Remove(candidateClient);
        }
        #endregion       
    
    
    }
}
