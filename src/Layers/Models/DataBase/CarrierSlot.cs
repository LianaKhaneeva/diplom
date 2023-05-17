using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Models.DataBase.Abstract;

namespace Models.DataBase
{
    /// <summary>
    /// Заявка грузоперевозчика.
    /// </summary>
    public class CarrierSlot: BaseSlot
    {
        /// <summary>
        /// Тип загрузки.
        /// </summary>
        [Required]
        public string LoadingType { get; set; } = null!;

        /// <summary>
        /// Грузоперевозчик.
        /// </summary>
        [Required,
            ForeignKey("CarrierId")]
        public Carrier Carrier { get; set; } = null!;

        /// <summary>
        /// Идентификатор грузоперевозчика.
        /// </summary>
        [Required,
            ForeignKey("CarrierId")]
        public int CarrierId { get; set; }
        
        /// <summary>
        /// Грузоотправитель.
        /// </summary>
        [ForeignKey("ShipperId")]
        public Shipper? Shipper { get; set; }

        /// <summary>
        /// Идентификатор грузоотправителя.
        /// </summary>
        [ForeignKey("ShipperId")]
        public int? ShipperId { get; set; }
    }
}