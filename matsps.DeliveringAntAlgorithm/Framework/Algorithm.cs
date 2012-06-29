using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace matsps.DeliveringAntAlgorithm
{
    public class Algorithm
    {
        #region Поля
        private static Random _rnd = new Random();
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
        public Algorithm(IEnumerable<Car> carsCollection, IEnumerable<Client> clientsCollection, Parameters param)
        {
            Init(carsCollection, clientsCollection, param);
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
                    //ant.SetStartPoint();
                    for (int k = 0; k < Cars.Count; k++)
                    {
                        Car car = Cars[k]; //Текущая машина
                        for (int m = 0; m < Clients.Count; m++)
                        {

                            Client candidateClient = ant.NextCandidate();
                            bool result = car.TryAddCandidate(candidateClient);   // либо добавляет, тогда к следующей точке. либо не добавляет, тогда к след. машине
                            if (result)
                            {
                                // если успех, то удаляем из списка доступных в муравье. этот клиент уже добавлен в машину
                                ant.AddToTabu(candidateClient);
                            }
                            //ant.GoToNextCity(); //Муравей переходит в следующий выбранный город
                            //Car temp = ant.Cars[k]; //Создаем временную копию текущей машины
                            ////Расчет километража
                            //temp.Clients.Add(Clients[m]);
                            //temp.CalculateMilliage();
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

            
            // инициализация коллекции клиентов
            List<Client> liClient = new List<Client>();
            for (int i = 0; i < clientCount; i++)
            {
                Client currentClient = Client.CreateRandom();
                liClient.Add(currentClient);
            }


            // Расстояния между городами и точка старта
            PointD startRoutePoint = Client.CreateRandom().Position;
            Distance dist = new Distance();
            dist.CalcDistanceLine(liClient, startRoutePoint);

            // инициализация коллекции машин
            List<Car> liCar = new List<Car>();
            for (int i = 0; i < carCount; i++)
            {
                Car currentCar = Car.CreateRandom();
                currentCar.Distance = dist; // матрица расстояний в каждой машине
                liCar.Add(currentCar);
            }

            //Инициализация алгоритма значениями и параметрами
            this.Init(liCar, liClient, Params);
        }
        /// <summary>
        /// Выполняет инициализацию экземпляра алгоритма заданными параметрами и значениями
        /// </summary>
        /// <param name="carsCollection">Коллекция машин</param>
        /// <param name="clientsCollection">Коллекция клиентов</param>
        /// <param name="distance">Матрица расстояний между клиентами</param>
        /// <param name="param">Параметры алгоритма</param>
        public void Init(IEnumerable<Car> carsCollection, IEnumerable<Client> clientsCollection, Parameters param)
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
            //заполнение коллекции машин
            carsCollection.ToList().ForEach(delegate(Car item)
            {
                Cars.Add(item);
            });

            Pheromones = new Pheromones(Cars.Count, Clients.Count);

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
