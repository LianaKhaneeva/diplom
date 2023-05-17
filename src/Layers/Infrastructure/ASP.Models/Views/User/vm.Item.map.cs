using ASP.Models.Views.Abstractions;

using Contracts.DataBase.Extensions;

using data = Models.DataBase.Abstract;

namespace ASP.Models.Views.User
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
                this.CreateMap<data.User, Item>()

                    .ForMember(destination => destination.FullName,
                        x => x.MapFrom(src => src.ToFullName()))

                    .ForMember(destination => destination.MiddleName,
                        x => x.MapFrom(src => src.MiddleName))

                    .ForMember(destination => destination.LastName,
                        x => x.MapFrom(src => src.LastName))

                    .ForMember(destination => destination.FirstName,
                        x => x.MapFrom(src => src.FirstName))

                    .ForMember(destination => destination.Phone,
                        x => x.MapFrom(src => src.Phone));
            }
        }
    }
}
