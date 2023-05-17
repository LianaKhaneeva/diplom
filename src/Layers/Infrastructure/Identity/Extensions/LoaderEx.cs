using System.Runtime.CompilerServices;

namespace Identity.Extensions
{
    public static class LoaderEx
    {
        public static TRelated Load<TRelated>(
                this Action<object, string> loader,
              object entity,
                 ref TRelated navigation,
           [CallerMemberName] string? name = null)
               where TRelated : class
        {
            loader?.Invoke(entity, name!);

            return navigation;
        }
    }
}