using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Models.DataBase;

namespace Store.Setups
{
    /// <summary>
    /// Определяет базовый метод конфигурации для грузоперевозчика.
    /// </summary>
    internal sealed class CarrierSetup : IEntityTypeConfiguration<Carrier>
    {
        /// <summary>
        /// Настраивает объект типа <see cref="Carrier" />.
        /// </summary>
        /// <param name="self">Строитель, который будет использоваться для настройки <see cref="Carrier" />.</param>
        public void Configure(EntityTypeBuilder<Carrier> self) 
        {
        }
    }

    /// <summary>
    /// Определяет базовый метод конфигурации для грузоотправителя.
    /// </summary>
    internal sealed class ShipperSetup : IEntityTypeConfiguration<Shipper>
    {
        /// <summary>
        /// Настраивает объект типа <see cref="Shipper" />.
        /// </summary>
        /// <param name="self">Строитель, который будет использоваться для настройки <see cref="Shipper" />.</param>
        public void Configure(EntityTypeBuilder<Shipper> self) 
        {
        }
    }
}