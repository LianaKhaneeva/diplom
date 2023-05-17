using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ASP.Models
{
    /// <summary>
    /// Определяет метод работы с ответами сервера.
    /// </summary>
    /// <typeparam name="T">Модель данных.</typeparam>
    public sealed class Responce<T> : IActionResult
    {
        /// <summary>
        /// Список ошибок.
        /// </summary>
        public List<Error>? Errors { get; set; }

        /// <summary>
        /// Мета-данные.
        /// </summary>
        public Meta Meta { get; set; } = null!;

        /// <summary>
        /// Данные ответа.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Асинхронно выполняет операцию результата метода действия.
        /// </summary>
        /// <param name="context">Объект контекста для выполнения действия, выбранного в рамках HTTP-запроса.</param>
        public async Task ExecuteResultAsync(ActionContext context)
        {
            var result = new ObjectResult(this);

            await result.ExecuteResultAsync(context);
        }
    }

    /// <summary>
    /// Модель мета-данных.
    /// </summary>
    public sealed class Meta
    {
        /// <summary>
        /// Статус.
        /// </summary>
        public int Status { get; init; }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Meta"/>.
        /// </summary>
        /// <param name="status">Статус.</param>
        public Meta(int status)
        {
            this.Status = status;
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="Meta"/>.
        /// </summary>
        public Meta() : this(StatusCodes.Status200OK){}
    }

    /// <summary>
    /// Модель ошибки.
    /// </summary>
    public sealed class Error
    {
        /// <summary>
        /// Текст ошибки.
        /// </summary>
        public string Message { get; set; } = null!;
    }
}
