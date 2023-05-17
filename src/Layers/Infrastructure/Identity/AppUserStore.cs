using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using DataContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Identity
{
    /// <summary>
    /// Хранилище системных пользователей.
    /// </summary>
    public sealed class AppUserStore : UserStore<AppUser, IdentityRole<int>, DataContext, int>
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="AppUserStore"/>.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        /// <param name="describer"><see cref="IdentityErrorDescriber" />.</param>
        public AppUserStore(DataContext context, IdentityErrorDescriber? describer = null)
             : base(context, describer)
        { }
    }
}