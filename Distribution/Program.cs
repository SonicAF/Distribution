using System;
using System.Linq;

namespace Distribution
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Тип распределения - ");
            var type = Console.ReadLine().ToDistributionType();

            Console.Write("Сумма - ");
            var pool = Console.ReadLine().ToCurrency();

            Console.Write("Суммы - ");
            var numbers = Console.ReadLine()
                .Split(';')
                .Select(token => token.ToCurrency())
                .ToList();

            var result = DistributionService.Distribute(numbers, pool, type);

            result.ForEach(num => Console.Write(num + ";"));
            Console.WriteLine();
        }
    }
}