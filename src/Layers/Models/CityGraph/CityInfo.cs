namespace Models.CityGraph
{
    /// <summary>
    /// Информация о городе.
    /// </summary>
    public class CityInfo
    {
        /// <summary>
        /// Город.
        /// </summary>
        public City City { get; }

        /// <summary>
        /// Не посещенный город.
        /// </summary>
        public bool IsUnvisited { get; set; }

        /// <summary>
        /// Сумма расстояний.
        /// </summary>
        public int DistancesSum { get; set; }

        /// <summary>
        /// Предыдущий город
        /// </summary>
        public City? PreviousCity { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="cityName">Название города</param>
        public CityInfo(City city)
        {
            City = city;
            IsUnvisited = true;
            DistancesSum = int.MaxValue;
            PreviousCity = null;
        }
    }
}