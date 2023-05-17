using System.Net;

using Utf8Json;

namespace WebApp.Middleware
{
    /// <summary>
    /// Представляет промежуточное ПО, которое обрабатывает исключения, возникающие при обработке запросов.
    /// </summary>
    public sealed class ErrorHandling
    {
        /// <summary>
        /// Делегат, представляющий следующее ПО промежуточного слоя в конвейере запросов.
        /// </summary>
        private readonly RequestDelegate next;

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="ErrorHandling"/>.
        /// </summary>
        /// <param name="next">Делегат, представляющий следующее ПО промежуточного слоя в конвейере запросов.</param>
        public ErrorHandling(RequestDelegate next)
        {
            this.next = next;
        }

        /// <summary>
        /// Выполняет промежуточное ПО в асинхронном режиме.
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> для текущего запроса.</param>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        /// <summary>
        /// Обрабатывает исключение в асинхронном режиме.
        /// </summary>
        /// <param name="context"><see cref="HttpContext"/> для текущего запроса.</param>
        /// <param name="exception">Представляет ошибки, возникающие во время выполнения приложения.</param>
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var errors =  string.IsNullOrWhiteSpace(exception.Message) ? "Ошибка сервера. Попробуйте позже." : exception.Message;
            
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            context.Response.ContentType = "appliation/json";

            var result = JsonSerializer.ToJsonString(new
            {
                errors
            });

            await context.Response.WriteAsync(result);
        }
    }
}
