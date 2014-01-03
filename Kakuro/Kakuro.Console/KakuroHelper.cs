namespace Kakuro.Console
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Kakuro;

    public sealed class KakuroHelper
    {
        private static void Main(string[] args)
        {
            var desiredSum = short.Parse(args[0]);
            var spaces = short.Parse(args[1]);

            var combinations = new KakuroCombinationCollection(desiredSum, spaces);

            foreach (var combo in combinations)
            {
                foreach (var s in combo)
                {
                    Console.Write(s);
                }
                Console.Write(Environment.NewLine);
            }

            Console.ReadKey();
        }
    }
}
