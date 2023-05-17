using Microsoft.EntityFrameworkCore;

using Models.DataBase;

namespace Store.Decorators
{
    /// <summary>
    /// Расширения для запросов к <see cref="Carrier"/>.
    /// </summary>
    internal static class CarrierDecorator
    {
        /// <summary>
        /// Добавляет в выборку данные из <see cref="Carrier.CarrierSlots"/>.
        /// </summary>
        public static IQueryable<Carrier> IncludeCarrierSlots(this IQueryable<Carrier> query)
            => query.Include(x => x.CarrierSlots);

        /// <summary>
        /// Добавляет в выборку данные из <see cref="Carrier.ShipperSlots"/>.
        /// </summary>
        public static IQueryable<Carrier> IncludeShipperSlots(this IQueryable<Carrier> query)
            => query.Include(x => x.ShipperSlots);

        /// <summary>
        /// Добавляет в выборку все связные данные.
        /// </summary>
        public static IQueryable<Carrier> IncludeAll(this IQueryable<Carrier> query)
            => query.IncludeCarrierSlots()
                    .IncludeShipperSlots();
    }

    /// <summary>
    /// Расширения для запросов к <see cref="Shipper"/>.
    /// </summary>
    internal static class ShipperDecorator
    {
        /// <summary>
        /// Добавляет в выборку данные из <see cref="Shipper.CarrierSlots"/>.
        /// </summary>
        public static IQueryable<Shipper> IncludeCarrierSlots(this IQueryable<Shipper> query)
            => query.Include(x => x.CarrierSlots);

        /// <summary>
        /// Добавляет в выборку данные из <see cref="Shipper.ShipperSlots"/>.
        /// </summary>
        public static IQueryable<Shipper> IncludeShipperSlots(this IQueryable<Shipper> query)
            => query.Include(x => x.ShipperSlots);

        /// <summary>
        /// Добавляет в выборку все связные данные.
        /// </summary>
        public static IQueryable<Shipper> IncludeAll(this IQueryable<Shipper> query)
            => query.IncludeCarrierSlots()
                    .IncludeShipperSlots();
    }
}