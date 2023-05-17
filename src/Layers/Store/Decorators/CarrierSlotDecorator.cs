using Microsoft.EntityFrameworkCore;

using Models.DataBase;

namespace Store.Decorators
{
    /// <summary>
    /// Расширения для запросов к <see cref="CarrierSlot"/>.
    /// </summary>
    internal static class CarrierSlotDecorator
    {
        /// <summary>
        /// Добавляет в выборку данные из <see cref="CarrierSlot.Carrier"/>.
        /// </summary>
        public static IQueryable<CarrierSlot> IncludeCarrier(this IQueryable<CarrierSlot> query)
            => query.Include(x => x.Carrier);

        /// <summary>
        /// Добавляет в выборку данные из <see cref="CarrierSlot.Shipper"/>.
        /// </summary>
        public static IQueryable<CarrierSlot> IncludeShipper(this IQueryable<CarrierSlot> query)
            => query.Include(x => x.Shipper);

        /// <summary>
        /// Добавляет в выборку все связные данные.
        /// </summary>
        public static IQueryable<CarrierSlot> IncludeAll(this IQueryable<CarrierSlot> query)
            => query.IncludeCarrier()
                    .IncludeShipper();
    }
}