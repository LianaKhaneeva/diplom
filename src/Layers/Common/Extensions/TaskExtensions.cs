namespace Common.Extensions
{
    /// <summary>
    /// Содержит методы расширения для работы с асинхронными операциями.
    /// </summary>
    public static class TaskExtensions
    {
        /// <summary>
        /// Создает продолжение, которое выполняется асинхронно после завершения выполнения целевой задачи.
        /// </summary>
        /// <typeparam name="T">Тип результата целевой задачи</typeparam>
        /// <param name="source">Целевая задача.</param>
        /// <param name="action">Метод, исполняемый в рамках продолжающей задачи.</param>
        /// <returns>Продолжающая задача, определяемая <paramref name="action"/>.</returns>

        public static Task Continue<T>(this Task<T> source, Func<Task<T>, Task> action)
            => source.ContinueWith(x => action.Invoke(x))
                     .Unwrap();
    }
}