using System.ComponentModel.DataAnnotations;

namespace Models.DataBase.Abstract
{
    /// <summary>
    /// Базовый класс заявки.
    /// </summary>
    public class BaseSlot: Entity
    {
        /// <summary>
        /// Тип платежа.
        /// </summary>
        [Required]
        public string PaymentType { get; set; } = null!;

        /// <summary>
        /// Тип кузова.
        /// </summary>
        [Required]
        public string BodyType { get; set; } = null!;

        /// <summary>
        /// Дата.
        /// </summary>
        [Required]
        public DateTime FromDate { get; set; }

        /// <summary>
        /// Откуда.
        /// </summary>
        [Required,
            StringLength(50)]
        public string From { get; set; } = null!;

        /// <summary>
        /// Куда.
        /// </summary>
        [Required,
            StringLength(50)]
        public string To { get; set; } = null!;

        /// <summary>
        /// Выполнена ли заявка.
        /// </summary>
        [Required]
        public bool IsComplete { get; set; }

        /// <summary>
        /// В работе ли заявка.
        /// </summary>
        [Required]
        public bool IsInWork { get; set; }

        /// <summary>
        /// Грузоподъемность.
        /// </summary>
        [Required]
        public int Carrying { get; set; }

        /// <summary>
        /// Объем.
        /// </summary>
        [Required]
        public int Volume { get; set; }

        /// <summary>
        /// Оценка.
        /// </summary>
        [Required]
        public int Rate { get; set; }
    }
}