using ASP.Models.Views.Abstractions;

using data = Models.DataBase;

namespace ASP.Models.Views.CarrierSlot
{
    /// <summary>
    /// Модель отображения списка.
    /// </summary>
    public sealed partial class Item
    {
        /// <summary>
        /// Карта преобразования.
        /// </summary>
        public sealed class Map : BaseMap
        {
            public Map()
            {
                // To VM
                this.CreateMap<data.CarrierSlot, Item>()

                    .ForMember(destination => destination.Carrier,
                        x => x.MapFrom(src => this.UserOrDefault(src.Carrier)))

                    .ForMember(destination => destination.Shipper,
                        x => x.MapFrom(src => this.UserOrDefault(src.Shipper)))

                    .ForMember(destination => destination.LoadingType,
                        x => x.MapFrom(src => src.LoadingType))

                    .ForMember(destination => destination.PaymentType,
                        x => x.MapFrom(src => src.PaymentType))

                    .ForMember(destination => destination.IsInWork,
                        x => x.MapFrom(src => src.IsInWork ? "Да" : "Нет"))

                    .ForMember(destination => destination.IsComplete,
                        x => x.MapFrom(src => src.IsComplete ? "Да" : "Нет"))

                    .ForMember(destination => destination.BodyType,
                        x => x.MapFrom(src => src.BodyType))

                    .ForMember(destination => destination.FromDate,
                        x => x.MapFrom(src => src.FromDate.ToLongDateString()))

                    .ForMember(destination => destination.From,
                        x => x.MapFrom(src => src.From))

                    .ForMember(destination => destination.To,
                        x => x.MapFrom(src => src.To))

                    .ForMember(destination => destination.Carrying,
                        x => x.MapFrom(src => src.Carrying))

                    .ForMember(destination => destination.Volume,
                        x => x.MapFrom(src => src.Volume))

                    .ForMember(destination => destination.Rate,
                        x => x.MapFrom(src => src.Rate));
            }
        }
    }
}
