using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Manager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.Write("Начало диапазона (a): ");
            if (!int.TryParse(Console.ReadLine(), out int a))
            {
                Console.WriteLine("некорректный ввод ");
                return;
            }

            Console.Write("Конец диапазона (b): ");
            if (!int.TryParse(Console.ReadLine(), out int b))
            {
                Console.WriteLine("некорректный ввод ");
                return;
            }

            Console.Write("Количество процессов N = ");
            if (!int.TryParse(Console.ReadLine(), out int n) || n <= 0)
            {
                Console.WriteLine("некорректный ввод ");
                return;
            }
            Console.WriteLine($"\nДиапазон: [{a}; {b}], процессов: {n}\n");

            int total = b - a + 1;
            int chunk = total / n;

            List<Process> processes = new List<Process>();
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < n; i++)
            {
                int start = a + i * chunk;
                int end = (i == n - 1) ? b : start + chunk - 1;

                var psi = new ProcessStartInfo
                {
                    FileName = "Worker.exe",
                    Arguments = $"{start} {end}",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process p = new Process();
                p.StartInfo = psi;
                p.Start();
                processes.Add(p);
                Console.WriteLine($"Запущен Worker: {start}-{end}");
            }

            int primeCount = 0;
            foreach (Process p in processes)
            {
                if (p.WaitForExit(10_000))
                {
                    string result = p.StandardOutput.ReadToEnd();

                    if (p.ExitCode == 0)
                    {
                        primeCount += int.Parse(result);
                    }
                }
                else
                {
                    Console.WriteLine("Завершение...");
                    p.Kill();
                }
            }
            sw.Stop();

            Console.WriteLine($"\nПростых чисел: {primeCount}");
            Console.WriteLine($"Время: {sw.ElapsedMilliseconds} ms");
        }
    }
}
