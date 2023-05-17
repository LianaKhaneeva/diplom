using ASP.Models.Views.Abstractions;

using data = Models.DataBase;

namespace ASP.Models.Views.CarrierSlot
{
    /// <summary>
    /// Модель создания.
    /// </summary>
    public sealed partial class New
    {
        /// <summary>
        /// Карта преобразования.
        /// </summary>
        public sealed class Map : BaseMap
        {
            public Map()
            {
                // To VM
                this.CreateMap<New, data.CarrierSlot>()

                    .ForMember(destination => destination.LoadingType,
                        x => x.MapFrom(src => src.LoadingType))

                    .ForMember(destination => destination.PaymentType,
                        x => x.MapFrom(src => src.PaymentType))

                    .ForMember(destination => destination.BodyType,
                        x => x.MapFrom(src => src.BodyType))

                    .ForMember(destination => destination.FromDate,
                        x => x.MapFrom(src => src.FromDate))

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