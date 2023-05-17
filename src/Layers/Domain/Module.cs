using Microsoft.Extensions.DependencyInjection;

using Domain.Services;

namespace Domain
{
    public static class Module
    { 
        /// <summary>
        /// Регистрирует типы в IoC-контейнере.
        /// </summary>
        /// <param name="container">Используемый IoC-контейнер</param>
        public static void Register(IServiceCollection container)
        {
            container.AddTransient(typeof(CarrierService));
            container.AddTransient(typeof(ShipperService));
            container.AddTransient(typeof(CarrierSlotService));
            container.AddTransient(typeof(ShipperSlotService));
            container.AddTransient(typeof(DijkstraService));
        }
    }
}