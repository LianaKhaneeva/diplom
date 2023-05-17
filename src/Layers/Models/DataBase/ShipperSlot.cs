using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Models.DataBase.Abstract;

namespace Models.DataBase
{
    /// <summary>
    /// Заявка от грузоотправителя.
    /// </summary>
    public class ShipperSlot : BaseSlot
    {
        /// <summary>
        /// Грузоотправитель.
        /// </summary>
        [Required,
            ForeignKey("ShipperId")]
        public Shipper Shipper { get; set; } = null!;

        /// <summary>
        /// Идентификатор грузоотправителя.
        /// </summary>
        [Required,
            ForeignKey("ShipperId")]
        public int ShipperId { get; set; }

        /// <summary>
        /// Грузоперевозчик.
        /// </summary>
        [ForeignKey("CarrierId")]
        public Carrier? Carrier { get; set; }

        /// <summary>
        /// Идентификатор грузоперевозчика.
        /// </summary>
        [ForeignKey("CarrierId")]
        public int? CarrierId { get; set; }

        /// <summary>
        /// Наименование товара.
        /// </summary>
        [Required]
        public string ShippingName { get; set; } = null!;
    }
}