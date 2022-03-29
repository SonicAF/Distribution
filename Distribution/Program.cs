using System;
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
}