using Models.CityGraph;
using Models.DataBase.Abstract;

namespace Domain.Services
{
    /// <summary>
    /// Сервис работы с алгоритмом Дейкстры.
    /// </summary>
    public sealed class DijkstraService
    {
        /// <summary>
        /// Граф городов.
        /// </summary>
        private CityGraph CitiesGraph = null!;

        /// <summary>
        /// Информация о городах.
        /// </summary>
        private List<CityInfo> Cities = null!;

        /// <summary>
        /// Конструктор.
        /// </summary>
        public DijkstraService()
        {
            this.CitiesGraph = new CityGraph();

            this.CitiesGraph.AddCity("Санкт-Петербург");
            this.CitiesGraph.AddCity("Великий Новгород");
            this.CitiesGraph.AddCity("Тверь");
            this.CitiesGraph.AddCity("Москва");
            this.CitiesGraph.AddCity("Вологда");
            this.CitiesGraph.AddCity("Ярославль");
            this.CitiesGraph.AddCity("Иваново");
            this.CitiesGraph.AddCity("Владимир");
            this.CitiesGraph.AddCity("Рязань");
            this.CitiesGraph.AddCity("Нижний Новгород");
            this.CitiesGraph.AddCity("Саранск");
            this.CitiesGraph.AddCity("Пенза");
            this.CitiesGraph.AddCity("Чебоксары");
            this.CitiesGraph.AddCity("Ульяновск");
            this.CitiesGraph.AddCity("Самара");
            this.CitiesGraph.AddCity("Казань");

            this.CitiesGraph.AddDistance("Санкт-Петербург", "Великий Новгород", 151);
            this.CitiesGraph.AddDistance("Санкт-Петербург", "Вологда", 545);
            this.CitiesGraph.AddDistance("Великий Новгород", "Тверь", 323);
            this.CitiesGraph.AddDistance("Вологда", "Ярославль", 174);
            this.CitiesGraph.AddDistance("Тверь", "Москва", 156);
            this.CitiesGraph.AddDistance("Ярославль", "Москва", 246);
            this.CitiesGraph.AddDistance("Ярославль", "Иваново", 100);
            this.CitiesGraph.AddDistance("Москва", "Владимир", 184);
            this.CitiesGraph.AddDistance("Иваново", "Владимир", 83);
            this.CitiesGraph.AddDistance("Владимир", "Нижний Новгород", 224);
            this.CitiesGraph.AddDistance("Владимир", "Рязань", 192);
            this.CitiesGraph.AddDistance("Нижний Новгород", "Саранск", 217);
            this.CitiesGraph.AddDistance("Нижний Новгород", "Чебоксары", 209);
            this.CitiesGraph.AddDistance("Рязань", "Пенза", 382);
            this.CitiesGraph.AddDistance("Пенза", "Саранск", 111);
            this.CitiesGraph.AddDistance("Пенза", "Ульяновск", 251);
            this.CitiesGraph.AddDistance("Пенза", "Самара", 342);
            this.CitiesGraph.AddDistance("Ульяновск", "Самара", 157);
            this.CitiesGraph.AddDistance("Ульяновск", "Казань", 180);
            this.CitiesGraph.AddDistance("Чебоксары", "Казань", 116);
        }

        /// <summary>
        /// Инициализация информации
        /// </summary>
        void InitInfo()
        {
            Cities = new List<CityInfo>();
            foreach (var c in CitiesGraph.Cities)
            {
                Cities.Add(new CityInfo(c));
            }
        }

        /// <summary>
        /// Получение информации о городе
        /// </summary>
        /// <param name="city">Город</param>
        /// <returns>Информация о городе</returns>
        CityInfo GetCityInfo(City city)
        {
            foreach (var i in Cities)
            {
                if (i.City.Equals(city))
                {
                    return i;
                }
            }

            return null;
        }

        /// <summary>
        /// Поиск непосещенного города с минимальным значением суммы.
        /// </summary>
        /// <returns>Информация о городе.</returns>
        public CityInfo FindUnvisitedCityWithMinSum()
        {
            var minValue = int.MaxValue;
            CityInfo minCity = null;

            foreach (var city in this.Cities)
            {
                if (city.IsUnvisited && city.DistancesSum < minValue)
                {
                    minCity = city;
                    minValue = city.DistancesSum;
                }
            }

            return minCity;
        }

        /// <summary>
        /// Поиск кратчайшего пути по городам.
        /// </summary>
        /// <param name="slot">Заявка.</param>
        /// <returns> Кратчайший путь.</returns>
        public string FindShortestPath(BaseSlot slot)
        {
            InitInfo();

            var startCity = this.CitiesGraph.FindCity(slot.From);
            var finishCity = this.CitiesGraph.FindCity(slot.To);

            if (startCity == null)
            {
                throw
                  new ArgumentNullException("Такого города отправки нет в нашей базе");
            }
            
            if (finishCity == null)
            {
                throw
                  new ArgumentNullException("Такого города прибытия нет в нашей базе");
            }

            var first = GetCityInfo(startCity);

            first.DistancesSum = 0;

            while (true)
            {
                var current = FindUnvisitedCityWithMinSum();

                if (current == null)
                {
                    break;
                }

                SetSumToNextCity(current);
            }

            return GetPath(startCity, finishCity);
        }

        /// <summary>
        /// Вычисление суммы дистанций для следующего города.
        /// </summary>
        /// <param name="cityInfo">Информация о текущем городе.</param>
        void SetSumToNextCity(CityInfo cityInfo)
        {
            cityInfo.IsUnvisited = false;
            foreach (var d in cityInfo.City.Distances)
            {
                var nextCity = GetCityInfo(d.ConnectedCity); ;

                var sum = cityInfo.DistancesSum + d.Distance;

                if (sum < nextCity.DistancesSum)
                {
                    nextCity.DistancesSum = sum;
                    nextCity.PreviousCity = cityInfo.City;
                }
            }
        }

        /// <summary>
        /// Формирование пути.
        /// </summary>
        /// <param name="start">Начальный город.</param>
        /// <param name="finish">Конечный город.</param>
        /// <returns>Путь.</returns>
        string GetPath(City start, City finish)
        {
            var path = finish.ToString();

            var distantion = GetCityInfo(finish).DistancesSum;

            while (start != finish)
            {
                finish = GetCityInfo(finish).PreviousCity;

                path = finish.ToString() + " => " + path;
            }

            return $"Найденный маршурт по алгоритму Дейикстры: {path}. Длина маршрута: {distantion} км.";
        }
    }
}
