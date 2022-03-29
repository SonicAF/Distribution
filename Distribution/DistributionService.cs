using System;
using System.Collections.Generic;
using System.Linq;

namespace Distribution
{
    public static class DistributionService
    {
        private const int Signs = 2;

        private const double Unit = 0.01;

        private const double Delta = 0.0001;

        public static List<double> Distribute(IEnumerable<double> numbers, double pool, DistributionType type)
        {
            switch (type)
            {
                case DistributionType.Ascending:
                    return DistributeAscending(numbers, pool);
                case DistributionType.Descending:
                    return DistributeDescending(numbers, pool);
                case DistributionType.Proportional:
                    return DistributeProportional(numbers, pool);
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

        private static List<double> DistributeProportional(IEnumerable<double> source, double pool)
        {
            var result = new List<double>();

            var numbers = source.ToList();

            var accumulated = 0d;

            var proportion = pool / numbers.Sum();

            var distributions = numbers.Select(number => number * proportion);

            foreach (var number in distributions)
            {
                var rounded = Math.Round(number, Signs);

                accumulated += number - rounded;

                if (Math.Abs(Math.Abs(accumulated) - Unit) < Delta)
                {
                    if (accumulated > 0)
                    {
                        accumulated -= 0.01;

                        rounded += 0.01;
                    }
                }

                result.Add(rounded);
            }

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

    public enum DistributionType
    {
        Ascending,
        Descending,
        Proportional
    }
}