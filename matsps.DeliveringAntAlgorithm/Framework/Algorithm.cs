using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace matsps.DeliveringAntAlgorithm
{
    public class Algorithm
    {
        #region Поля
        /// <summary>
        /// Матрица расстояний
        /// </summary>
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
        public Parameters Params
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
        /// <param name="param">Параметры алгоритма</param>
        /// <param name="carsCollection">Коллекция машин</param>
        /// <param name="clientsCollection">Коллекция клиентов</param>
        public Algorithm(IEnumerable<Car> carsCollection, IEnumerable<Client> clientsCollection, double[,] distance, Parameters param)
        {
            Init(carsCollection, clientsCollection, distance, param);
        }
        /// <summary>
        /// Создает экземпляр алгоритма с настройками по умолчанию
        /// </summary>
        public Algorithm() { }
        #endregion
         
        #region Открытые методы
        /// <summary>
        /// Выполняет запуск алгоритма
        /// </summary>
        public void Start()
        {
            for (int i = 0; i < Params.IterationCount; i++)
            {
                for (int j = 0; j < Ants.Count; j++)
                {
                    Ant ant = Ants[j]; //Текущий муравей
                    for (int k = 0; k < Cars.Count; k++)
                    {
                        Car car = Cars[k]; //Текущая машина
                        for (int m = 0; m < Clients.Count; m++)
                        {
                            ant.GoToNextCity(); //Муравей переходит в следующий выбранный город
                            Car temp = ant.Cars[k]; //Создаем временную копию текущей машины
                            //Расчет километража
                            temp.Clients.Add(Clients[m]);
                            temp.CalculateMilliage();
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Выполняет инициализацию алгоритма тестовыми значениями
        /// </summary>
        public void InitTestData()
        {
            int carCount = 5;      //размер коллекции машин
            int clientCount = 85;  //размер коллеции клиентов

            // задание параметров алгоритма по умолчанию
            Params = Parameters.Default;

            // инициализация коллекции машин
            List<Car> liCar = new List<Car>();
            for (int i = 0; i < carCount; i++)
            {
                Car currentCar = Car.CreateRandom();
                liCar.Add(currentCar);
            }

            // инициализация коллекции клиентов
            List<Client> liClient = new List<Client>();
            for (int i = 0; i < clientCount; i++)
            {
                Client currentClient = Client.CreateRandom();
                liClient.Add(currentClient);
            }

            // инициализация матрицы расстояний
            double[,] dist = ClientsCollection.CalculateDistance(liClient);

            //Инициализация алгоритма значениями и параметрами
            this.Init(liCar, liClient, dist, Params);
        }
        /// <summary>
        /// Выполняет инициализацию экземпляра алгоритма заданными параметрами и значениями
        /// </summary>
        /// <param name="carsCollection">Коллекция машин</param>
        /// <param name="clientsCollection">Коллекция клиентов</param>
        /// <param name="distance">Матрица расстояний между клиентами</param>
        /// <param name="param">Параметры алгоритма</param>
        public void Init(IEnumerable<Car> carsCollection, IEnumerable<Client> clientsCollection, double[,] distance, Parameters param)
        {
            Ants = new List<Ant>();
            Clients = new ClientsCollection();
            Cars = new CarsCollection();

            // значения параметров по умолчанию
            Params = param;

            //заполнение коллекции клиентов
            clientsCollection.ToList().ForEach(delegate(Client item)
            {
                Clients.Add(item);
            });
            Clients.Distancies = distance;
            //заполнение коллекции машин
            carsCollection.ToList().ForEach(delegate(Car item)
            {
                Cars.Add(item);
            });

            Pheromones = new Pheromones(Cars.Count, Clients.Count);
            _distance = distance;

            //заполнение коллекции муравьев
            Ants = new List<Ant>();
            for (int i = 0; i < param.AntCount; i++)
            {
                Ants.Add(new Ant(Cars,Clients,Pheromones,Params));
            }
        }
        #endregion
    }
}
