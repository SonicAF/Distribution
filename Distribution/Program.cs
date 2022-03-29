using System;
using System.Collections.Generic;
using System.Linq;

namespace Distribution
{
    public class Program
    {
        static void Main(string[] args)
        {
            var pool = Console.ReadLine().ToCurrency();

            var numbers = Console.ReadLine()
                .Split(';')
                .Select(token => token.ToCurrency())
                .ToList();

            var type = Console.ReadLine().ToDistributionType();

            var result = DistributionService.Distribute(numbers, pool, type);


            Console.WriteLine("Hello World!");
        }
    }

    public static class DistributionService
    {
        public static List<double> Distribute(IEnumerable<double> numbers, double pool, DistributionType type)
        {
            switch (type)
            {
                case DistributionType.Ascending:
                    return DistributeAscending(numbers, pool);
                case DistributionType.Descending:
                    return DistributeDescending(numbers, pool);
                case DistributionType.Proportional:
                    break;
            }

            return new List<double>();
        }

        private static List<double> DistributeAscending(IEnumerable<double> numbers, double pool)
        {
            var remaining = pool;

            return numbers.Select(number => Extract(number, remaining, out remaining)).ToList();
        }

        private static List<double> DistributeDescending(IEnumerable<double> numbers, double pool)
        {
            var result = DistributeAscending(numbers.Reverse(), pool);

            result.Reverse();

            return result;
        }

        private static double Extract(double numberToExtract, double pool, out double remaining)
        {
            remaining = pool - numberToExtract;

            if (remaining < 0)
            {
                var result = numberToExtract + remaining;

                remaining = 0;

                return result;
            }

            return numberToExtract;
        }
    }

    internal static class StringExt
    {
        public static double ToCurrency(this string source)
        {
            return Math.Round(double.Parse(source, System.Globalization.NumberStyles.AllowDecimalPoint), 2);
        }

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

    public enum DistributionType
    {
        Ascending,
        Descending,
        Proportional
    }
}