namespace Common.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Возвращает номер телефона без форматирования (только цифры).
        /// </summary>
        /// <param name="phone">Рассматриваемая как номер телефона строка.</param>
        /// <returns>Номер телефона, содержащий только цифры.</returns>
        public static string ClearPhone(this string phone)
            => new(phone.Where(char.IsDigit).ToArray());
    }
}