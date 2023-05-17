namespace Models.CityGraph
{
    /// <summary>
    /// Граф городов.
    /// </summary>
    public class CityGraph
    {
        /// <summary>
        /// Список городов.
        /// </summary>
        public List<City> Cities { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        public CityGraph()
        {
            Cities = new List<City>();
        }

        /// <summary>
        /// Добавление города.
        /// </summary>
        /// <param name="cityName">Имя города.</param>
        public void AddCity(string cityName)
        {
            Cities.Add(new City(cityName));
        }

        /// <summary>
        /// Поиск города.
        /// </summary>
        /// <param name="cityName">Название города.</param>
        /// <returns>Найденный город.</returns>
        public City? FindCity(string cityName)
        {
            foreach (var city in Cities)
            {
                if (city.Name.Equals(cityName))
                {
                    return city;
                }
            }

            return null;
        }

        /// <summary>
        /// Добавление расстояния.
        /// </summary>
        /// <param name="firstName">Имя первого города.</param>
        /// <param name="secondName">Имя второго города.</param>
        /// <param name="distance">Расстояние между ними.</param>
        public void AddDistance(string firstName, string secondName, int distance)
        {
            var firstCity = FindCity(firstName);
            var secondCity = FindCity(secondName);
            if (secondCity != null && firstCity != null)
            {
                firstCity.AddDistance(secondCity, distance);
                secondCity.AddDistance(firstCity, distance);
            }
        }
    }
}