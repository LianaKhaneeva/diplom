using System.ComponentModel;

namespace ASP.Models.Views.CarrierSlot
{
    /// <summary>
    /// Модель создания.
    /// </summary>
    [DisplayName("CarrierSlot.New")]
    public sealed partial class New
    {
        /// <summary>
        /// Тип загрузки.
        /// </summary>
        public string LoadingType { get; set; } = null!;

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