using System;

namespace Distribution
{
    internal static class StringExt
    {
        private const int Signs = 2;

        /// <summary>
        /// Конвертирует строку в число и возвращает его с двумя знаками после запятой
        /// </summary>
        public static double ToCurrency(this string source)
        {
            return Math.Round(double.Parse(source, System.Globalization.NumberStyles.AllowDecimalPoint), Signs,
                MidpointRounding.AwayFromZero);
        }

        /// <summary>
        /// Возвращает тип распределения, если он содержится в строке
        /// </summary>
        public static DistributionType ToDistributionType(this string source)
        {
            if (source.Contains("ПРОП"))
                return DistributionType.Proportional;

            if (source.Contains("ПЕРВ"))
                return DistributionType.Ascending;

            if (source.Contains("ПОСЛ"))
                return DistributionType.Descending;

            throw new ArgumentOutOfRangeException("Тип распределения некорректен.");
        }
    }
}