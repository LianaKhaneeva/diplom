using System.Net;

namespace Common
{
    /// <summary>
    /// Ошибка веб-приложения.
    /// </summary>
    public class WebAppException : Exception
    {
        /// <summary>
        /// Код статуса HTTP.
        /// </summary>
        public HttpStatusCode Code { get; }

        /// <summary>
        /// Ошибка.
        /// </summary>
        public string? Errors { get; set; }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="WebAppException"/>.
        /// </summary>
        /// <param name="code">Код статуса HTTP.</param>
        /// <param name="errors">Ошибка.</param>
        public WebAppException(HttpStatusCode code, string? errors = null)
        {
            this.Code   = code;
            this.Errors = errors;
        }
    }
}