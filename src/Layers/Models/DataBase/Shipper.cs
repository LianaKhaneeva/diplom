using Models.DataBase.Abstract;

namespace Models.DataBase
{
    /// <summary>
    /// Грузоотправитель.
    /// </summary>
    public class Shipper : User
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Shipper"/>.
        /// </summary>
        /// <param name="lazyLoader">Отложенная загрука.</param>
        private Shipper(Action<object, string> lazyLoader)
             => this.Lazy = lazyLoader;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Shipper"/>.
        /// </summary>
        public Shipper() { }

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