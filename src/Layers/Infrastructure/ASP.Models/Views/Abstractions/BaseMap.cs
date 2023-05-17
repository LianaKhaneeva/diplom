using AutoMapper;

using Contracts.DataBase;
using Contracts.DataBase.Extensions;

using user = Models.DataBase.Abstract.User;

namespace ASP.Models.Views.Abstractions
{
    public abstract class BaseMap : Profile
    {
        /// <summary>
        /// Формирует пару "Идентификатор сущности - имя".
        /// </summary>
        /// <param name="entity">Сущность.</param>
        /// <returns>Возвращает <see cref="KeyValuePair{TKey, string}"/>, где первым параметром является Идентификатор сущности, второй -  его имя. В случае если в качестве параметра был передан <see cref="null"/>, то вернет значение по умолчанию.</returns>
        protected KeyValuePair<int, string> ValueOrDefault<TEntity>(TEntity? entity) where TEntity : IHaveId, IHaveName
            => entity != null
               ? new KeyValuePair<int, string>(entity.Id, entity.Name)
               : default;

        /// <summary>
        /// Формирует пару "Идентификатор пользователя - Фамилия И. О.".
        /// </summary>
        /// <param name="user">Пользователь.</param>
        /// <returns>Возвращает <see cref="KeyValuePair{Guid, string}"/>, где первым параметром является Идентификатор пользователя, второй -  "Фамилия И. О.". В случае если в качестве параметра был передан <see cref="null"/>, то вернет значение по умолчанию.</returns>
        protected KeyValuePair<int, string> UserOrDefault(user? user)
            => user != null
                ? new KeyValuePair<int, string>(user.Id, user.ToShortName())
                : default;
    }
}
