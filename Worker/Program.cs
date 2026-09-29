using System;

namespace Worker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int a = 0;
            int b = 0;

            if (args.Length != 2)
            {
                Console.WriteLine("ERROR: нужно передать два аргумента");
                Environment.Exit(1);
            }
            if (!int.TryParse(args[0], out a) ||
                !int.TryParse(args[1], out b))
            {
                Console.WriteLine("ERROR: аргументы должны быть целыми числами");
                Environment.Exit(1);
            }

            int count = 0;
            for (int i = Math.Max(2, a); i <= b; i++)
            {
                if (IsPrime(i))
                    count++;
            }
            Console.WriteLine(count);
            Environment.Exit(0);
        }
        static bool IsPrime(int num)
        {
            if (num < 2) return false;
            if (num == 2 || num == 3) return true;
            if (num % 2 == 0) return false;

            int limit = (int)Math.Sqrt(num);
            for (int i = 3; i <= limit; i += 2)
            {
                if (num % i == 0)
                    return false;
            }
            return true;
        }
    }
}
