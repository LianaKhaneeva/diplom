using Microsoft.EntityFrameworkCore;

using Models.DataBase;

namespace Store.Decorators
{
    /// <summary>
    /// Расширения для запросов к <see cref="ShipperSlot"/>.
    /// </summary>
    internal static class ShipperSlotDecorator
    {
        /// <summary>
        /// Добавляет в выборку данные из <see cref="ShipperSlot.Carrier"/>.
        /// </summary>
        public static IQueryable<ShipperSlot> IncludeCarrier(this IQueryable<ShipperSlot> query)
            => query.Include(x => x.Carrier);

        /// <summary>
        /// Добавляет в выборку данные из <see cref="ShipperSlot.Shipper"/>.
        /// </summary>
        public static IQueryable<ShipperSlot> IncludeShipper(this IQueryable<ShipperSlot> query)
            => query.Include(x => x.Shipper);

        /// <summary>
        /// Добавляет в выборку все связные данные.
        /// </summary>
        public static IQueryable<ShipperSlot> IncludeAll(this IQueryable<ShipperSlot> query)
            => query.IncludeCarrier()
                    .IncludeShipper();
    }
}