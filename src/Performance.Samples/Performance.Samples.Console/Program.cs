using Performance.Samples.Data;
using System;
using System.Diagnostics;

namespace Performance.Samples
{
    class Program
    {
        static void Main(string[] args)
        {
            //OnlyNumbers();

            StringGen2();

            Console.ReadKey();
        }

        private static void StringGen2()
        {
            Console.WriteLine("------- GET 1000x -------");

            var data = new SamplesData();


            var sw = new Stopwatch();
            var before2 = GC.CollectionCount(2);
            var before1 = GC.CollectionCount(1);
            var before0 = GC.CollectionCount(0);

            sw.Start();
            for (int i = 0; i < 100; i++)
            {
                var values = data.GetAllAsync().GetAwaiter().GetResult();
            }

            sw.Stop();


            var gen0 = GC.CollectionCount(0) - before0;
            var gen1 = GC.CollectionCount(1) - before1;
            var gen2 = GC.CollectionCount(2) - before2;

            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"GC Gen #2  : {gen2}");
            Console.WriteLine($"GC Gen #1  : {gen1}");
            Console.WriteLine($"GC Gen #0  : {gen0}");
            Console.WriteLine("Done!");

        }

        private static void OnlyNumbersTest()
        {

            Console.WriteLine("------- Regex -------");
            OnlyNumbersRegex();

            GC.Collect();

            Console.WriteLine("------- Algorithm -------");
            OnlyNumbersSafe();

            GC.Collect();

            Console.WriteLine("------- Unsafe -------");
            OnlyNumbersUnsafe();

            GC.Collect();
        }


        private static void OnlyNumbersSafe()
        {
            string onlyNumbers = "(234)224@2342354!bcd3445";

            var sw = new Stopwatch();
            var before2 = GC.CollectionCount(2);
            var before1 = GC.CollectionCount(1);
            var before0 = GC.CollectionCount(0);

            sw.Start();
            for (int i = 0; i < 1_000_000; i++)
            {
                var algorithm = OnlyNumbers.Safe(onlyNumbers);
            }
            sw.Stop();


            var gen0 = GC.CollectionCount(0) - before0;
            var gen1 = GC.CollectionCount(1) - before1;
            var gen2 = GC.CollectionCount(2) - before2;

            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"GC Gen #2  : {gen2}");
            Console.WriteLine($"GC Gen #1  : {gen1}");
            Console.WriteLine($"GC Gen #0  : {gen0}");
            Console.WriteLine("Done!");
        }

        private static void OnlyNumbersUnsafe()
        {
            string onlyNumbers = "(234)224@2342354!bcd3445";

            var sw = new Stopwatch();
            var before2 = GC.CollectionCount(2);
            var before1 = GC.CollectionCount(1);
            var before0 = GC.CollectionCount(0);

            sw.Start();
            for (int i = 0; i < 1_000_000; i++)
            {
                var @unsafe = OnlyNumbers.Unsafe(onlyNumbers);
            }
            sw.Stop();


            var gen0 = GC.CollectionCount(0) - before0;
            var gen1 = GC.CollectionCount(1) - before1;
            var gen2 = GC.CollectionCount(2) - before2;

            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"GC Gen #2  : {gen2}");
            Console.WriteLine($"GC Gen #1  : {gen1}");
            Console.WriteLine($"GC Gen #0  : {gen0}");
            Console.WriteLine("Done!");
        }

        private static void OnlyNumbersRegex()
        {
            string onlyNumbers = "(234)224@2342354!bcd3445";

            var sw = new Stopwatch();
            var before2 = GC.CollectionCount(2);
            var before1 = GC.CollectionCount(1);
            var before0 = GC.CollectionCount(0);

            sw.Start();
            for (int i = 0; i < 1_000_000; i++)
            {
                var regex = OnlyNumbers.Regex(onlyNumbers);
            }
            sw.Stop();


            var gen0 = GC.CollectionCount(0) - before0;
            var gen1 = GC.CollectionCount(1) - before1;
            var gen2 = GC.CollectionCount(2) - before2;

            Console.WriteLine($"Tempo total: {sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"GC Gen #2  : {gen2}");
            Console.WriteLine($"GC Gen #1  : {gen1}");
            Console.WriteLine($"GC Gen #0  : {gen0}");
            Console.WriteLine("Done!");
        }
    }
}
