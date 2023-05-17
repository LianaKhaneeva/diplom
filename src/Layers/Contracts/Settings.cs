namespace Contracts
{
    /// <summary>
    /// Определяет настройки.
    /// </summary>
    public sealed class Settings
    {
        /// <summary>
        /// Настройки аутентификации.
        /// </summary>
        public sealed class Authentication
        {
            /// <summary>
            /// Ключ.
            /// </summary>
            public const string Key = "Authentication";

            /// <summary>
            /// Секретный ключ.
            /// </summary>
            public string Secret { get; set; } = null!;

            /// <summary>
            /// Сколько дней действует токен авторизации.
            /// </summary>
            public uint ExpiresInDays { get; set; }
        }
    }
}