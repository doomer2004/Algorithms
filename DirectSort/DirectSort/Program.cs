using System;
using System.Diagnostics;

namespace DirectSort
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fileName = "L16.txt";
            var generator = new Generator();
            Stopwatch sw = new Stopwatch();
            var sort = new DirectSort();
            var size = generator.Generate(10, fileName);
            sw.Start();
            sort.Sort(size, fileName);
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.Elapsed}");
            Console.ReadKey();
        }
    }
}
