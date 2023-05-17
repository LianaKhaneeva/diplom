using System.Net;

namespace Common.Exceptions
{
    /// <summary>
    /// Ошибка "Не найдено" (404).
    /// </summary>
    public sealed class NotFoundException : WebAppException
    {
        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NotFoundException"/> с шаблонным сообщением об ошибке.
        /// </summary>
        /// <param name="id">Идентификатор необнаруженной сущности.</param>
        public NotFoundException(int id)
             : base(HttpStatusCode.NotFound, $"Объект с ключом {id} не найден.")
        {
        }

        /// <summary>
        /// Инициализирует экземпляр класса <see cref="NotFoundException"/> с произвольным сообщением об ошибке.
        /// </summary>
        /// <param name="code">Код статуса HTTP.</param>
        /// <param name="errors">Ошибка.</param>
        public NotFoundException(string? errors = null)
             : base(HttpStatusCode.NotFound, errors)
        {
        }
    }
}