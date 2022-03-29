using System;
using System.Collections.Generic;
using System.Linq;

namespace Distribution
{
    public static class DistributionService
    {
        private const int Signs = 2;

        public static List<double> Distribute(IEnumerable<double> numbers, double pool, DistributionType type)
        {
            return type switch
            {
                DistributionType.Ascending => DistributeAscending(numbers, pool),
                DistributionType.Descending => DistributeDescending(numbers, pool),
                DistributionType.Proportional => DistributeProportional(numbers, pool),
                _ => new List<double>()
            };
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
            var numbers = source.ToList();

            var proportion = pool / numbers.Sum();

            var accumulator = new RoundingAccumulator(Signs);

            return numbers.Select(number => number * proportion).Select(number => accumulator.Round(number)).ToList();
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