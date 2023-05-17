using System.ComponentModel;

namespace ASP.Models.Views.ShipperSlot
{
    /// <summary>
    /// Модель создания.
    /// </summary>
    [DisplayName("ShipperSlot.New")]
    public sealed partial class New
    {
        /// <summary>
        /// Название груза.
        /// </summary>
        public string ShippingName { get; set; } = null!;

        /// <summary>
        /// Тип платежа.
        /// </summary>
        public string PaymentType { get; set; } = null!;

        /// <summary>
        /// Тип кузова.
        /// </summary>
        public string BodyType { get; set; } = null!;

        /// <summary>
        /// Дата.
        /// </summary>
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Откуда.
        /// </summary>
        public string From { get; set; } = null!;

        /// <summary>
        /// Куда.
        /// </summary>
        public string To { get; set; } = null!;

        /// <summary>
        /// Грузоподъемность.
        /// </summary>
        public int Carrying { get; set; }

        /// <summary>
        /// Объем.
        /// </summary>
        public int Volume { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        public int Rate { get; set; }
    }
}