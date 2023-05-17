namespace Models.CityGraph
{
    /// <summary>
    /// Город (вершина графа).
    /// </summary>
    public class City
    {
        /// <summary>
        /// Название города.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Список расстояний до городов.
        /// </summary>
        public List<DistanceToCity> Distances { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="cityName">Название города</param>
        public City(string cityName)
        {
            Name = cityName;
            Distances = new List<DistanceToCity>();
        }

        /// <summary>
        /// Добавить расстояние до города.
        /// </summary>
        /// <param name="city">Город.</param>
        /// <param name="distance">Расстояние до него.</param>
        public void AddDistance(City city, int distance)
        {
            Distances.Add(new DistanceToCity(city, distance));
        }

        /// <summary>
        /// Преобразование в строку.
        /// </summary>
        /// <returns>Имя города.</returns>
        public override string ToString() => Name;
    }
}