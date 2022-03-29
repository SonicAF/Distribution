using System.Collections.Generic;
using System.Threading;
using Distribution;
using NUnit.Framework;

namespace Tests
{
    public class DistributionServiceTests
    {
        private readonly List<double> _numbers = new() { 1000, 2000, 3000, 5000, 8000, 5000 };

        private const double Sum = 10000;

        [Test]
        public void TestAscendingDistribution()
        {
            var expectation = new List<double> { 1000, 2000, 3000, 4000, 0, 0 };

            var result = DistributionService.Distribute(_numbers, Sum, DistributionType.Ascending);

            Assert.AreEqual(expectation, result);
        }

        [Test]
        public void TestDescendingDistribution()
        {
            var expectation = new List<double> { 0, 0, 0, 0, 5000, 5000 };

            var result = DistributionService.Distribute(_numbers, Sum, DistributionType.Descending);

            Assert.AreEqual(expectation, result);
        }

        [Test]
        public void TestProportionalDistribution()
        {
            var expectation = new List<double> { 416.67, 833.33, 1250, 2083.33, 3333.33, 2083.34 };

            var result = DistributionService.Distribute(_numbers, Sum, DistributionType.Proportional);

            Assert.AreEqual(expectation, result);
        }
    }
}