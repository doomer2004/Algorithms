using System;
using System.Diagnostics;

namespace DirectSort
{
    static class Program
    {
        static void Main(string[] args)
        {
            var fileName = "L16.dat";
            var generator = new Generator();
            Stopwatch sw = new Stopwatch();
            var sort = new DirectSort();
            var size = generator.Generate(16 , fileName);
            var shareSize = size / 8;
            sw.Start();
            sort.Sort(size, shareSize, fileName);
            sw.Stop();
            Console.WriteLine($"Elapsed: {sw.Elapsed}");
            Generator.ShowContent("Output.dat", 10);
            Console.ReadKey();
        }
    }
}
