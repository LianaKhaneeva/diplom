using Microsoft.AspNetCore.Mvc;

using ASP.Models;

namespace WebApp.Controllers.Abstractions
{
    public partial class BaseController : ControllerBase
    {
        /// <summary>
        /// Возвращает код ответа 200.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        /// <param name="data">Данные ответа.</param>
        protected Responce<TModel> Ok<TModel>(TModel data)
          => new()
          {
              Meta = new Meta(),
              Data = data
          };

        /// <summary>
        /// Возвращает ответ с указанным кодом ошибки.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        /// <param name="status">Статус ответа.</param>
        protected Responce<TModel> Error<TModel>(int status)
           => new()
           {
               Meta = new Meta(status)
           };

        /// <summary>
        /// Возвращает код ответа 404.
        /// </summary>
        /// <typeparam name="TModel">Тип модели.</typeparam>
        protected Responce<TModel> NotFound<TModel>()
           => new()
           {
               Meta = new Meta(StatusCodes.Status404NotFound)
           };
    }
}