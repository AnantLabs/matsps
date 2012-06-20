using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    public class Algorithm
    {
        #region Поля
        private double[,] _distance;
        #endregion Поля


        #region Свойства
        /// <summary>
        /// Матрица феромонов
        /// </summary>
        public Pheromones Pheromones
        {
            set;
            get;
        }
        /// <summary>
        /// Параметры алгоритма
        /// </summary>
        public Parameters Parameter 
        { set; get; }

        /// <summary>
        /// Задает или возвращает список муравьев
        /// </summary>
        public List<Ant> Ants
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
        /// Задает или возвращает коллекцию машин
        /// </summary>
        public CarsCollection Cars
        {
            set;
            get;
        }
        #endregion

        #region Конструкторы
        /// <summary>
        /// Создает новый экземпляр алгоритма с указанными параметрами
        /// </summary>
        /// <param name="param">Параметры</param>
        /// <param name="carsCollection"></param>
        /// <param name="clientsCollection"></param>
        public Algorithm(IEnumerable<Car> carsCollection, IEnumerable<Client> clientsCollection, double[,] distance, Parameters param)
        {
            Init(carsCollection, clientsCollection, distance, param);
        }

        public Algorithm() { }

        public void Init(IEnumerable<Car> carsCollection, IEnumerable<Client> clientsCollection, double[,] distance, Parameters param)
        {
            Ants = new List<Ant>();
            Clients = new ClientsCollection();
            Cars = new CarsCollection();

            // значения параметров по умолчанию
            Parameter = param;

            Ants = new List<Ant>();
            clientsCollection.ToList().ForEach(delegate(Client item)
            {
                Clients.Add(item);
            });
            Clients.Distancies = distance;

            carsCollection.ToList().ForEach(delegate(Car item)
            {
                Cars.Add(item);
            });

            Pheromones = new Pheromones(Cars.Count, Clients.Count);

            _distance = distance;
        }

        public void InitTestData()
        {
            int carCount = 5;
            int clientCount = 85; 

            // параметры
            Parameter = Parameters.Default;

            // машины
            List<Car> liCar = new List<Car>();
            for (int i = 0; i < carCount; i++)
            {
                Car currentCar = Car.Default;
                liCar.Add(currentCar);
            }

            // клиенты
            List<Client> liClient = new List<Client>();
            for (int i = 0; i < clientCount; i++)
            {
                Client currentClient = Client.CreateRandom();
                liClient.Add(currentClient);
            }

            // матрица расстояний
            double[,] dist = ClientsCollection.CalculateDistance(liClient);


            Init(liCar, liClient, dist, Parameter);
        }
        #endregion

        #region Открытые методы
        public void Start()
        {
            for (int i = 0; i < Parameter.IterationCount; i++)
            {
                for (int j = 0; j < Ants.Count; j++)
                {
                    for (int k = 0; k < Cars.Count; k++)
                    {
                        for (int m = 0; m < Clients.Count; m++)
                        {
                            //Получаем индекс следующего города
                            int to = Ants[j].GetNextClientIndex();
                            //Переход к следующему городу
                            Ants[j].GoToNextCity(to);
                            //Подсчет километража текущего автомобиля с учетом нового города

                            if (Cars[k].Miliage >= Cars[k].MaxMiliage ||
                                Cars[k].Capacity >= Cars[k].MaxCapacity ||
                                Cars[k].Clients.Count >= Cars[k].MaxPassedClientsCount)
                            {
                                break;
                            }
                            else
                            {
                                //Добавление города в машину
                                Cars[k].Clients.Add(Clients[m]);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
