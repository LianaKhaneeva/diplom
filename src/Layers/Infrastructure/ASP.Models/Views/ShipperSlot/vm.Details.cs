using System.ComponentModel;

namespace ASP.Models.Views.ShipperSlot
{
    /// <summary>
    /// Модель отображения детализации.
    /// </summary>
    [DisplayName("ShipperSlot.Details")]
    public sealed partial class Details
    {
        /// <summary>
        /// Грузоперевозчик. 
        /// </summary>
        public KeyValuePair<int, string>? Carrier { get; set; }

        /// <summary>
        /// Грузоотправитель.
        /// </summary>
        public KeyValuePair<int, string> Shipper { get; set; }

        /// <summary>
        /// Название груза.
        /// </summary>
        public string ShippingName { get; set; } = null!;

        /// <summary>
        /// Тип платежа.
        /// </summary>
        public string PaymentType { get; set; } = null!;

        /// <summary>
        /// Выполнена ли заявка.
        /// </summary>
        public string IsComplete { get; set; } = null!;

        /// <summary>
        /// В работе ли заявка.
        /// </summary>
        public string IsInWork { get; set; } = null!;

        /// <summary>
        /// Тип кузова.
        /// </summary>
        public string BodyType { get; set; } = null!;

        /// <summary>
        /// Дата.
        /// </summary>
        public string FromDate { get; set; } = null!;

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