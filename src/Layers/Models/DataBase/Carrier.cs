using Models.DataBase.Abstract;

namespace Models.DataBase
{
    /// <summary>
    /// Грузоперевозчик.
    /// </summary>
    public class Carrier : User
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Carrier"/>.
        /// </summary>
        /// <param name="lazyLoader">Отложенная загрука.</param>
        private Carrier(Action<object, string> lazyLoader)
             => this.Lazy = lazyLoader;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Carrier"/>.
        /// </summary>
        public Carrier() { }

        /// <summary>
        /// Заявки грузоперевозчика.
        /// </summary>
        public List<CarrierSlot>? CarrierSlots { get; set; }

        /// <summary>
        /// Заявки грузоотправителя.
        /// </summary>
        public List<ShipperSlot>? ShipperSlots { get; set; }

    }
}