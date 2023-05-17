namespace Models.CityGraph
{
    /// <summary>
    /// Расстояние до города (ребро графа).
    /// </summary>
    public class DistanceToCity
    {
        /// <summary>
        /// Связанный город.
        /// </summary>
        public City ConnectedCity { get; }

        /// <summary>
        /// Расстояние.
        /// </summary>
        public int Distance { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="connectedCity">Связанный город.</param>
        /// <param name="distance">Расстояние.</param>
        public DistanceToCity(City connectedCity, int distance)
        {
            ConnectedCity = connectedCity;
            Distance = distance;
        }
    }
}
