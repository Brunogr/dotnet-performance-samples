using BenchmarkDotNet.Attributes;

namespace PerfomanceTests.Samples
{
    [MemoryDiagnoser]
    public class Boxing
    {
        [Benchmark(Baseline = true)]
        public string StringConcatWithBoxing() =>
            string.Concat("Hi .Net Conf! Day: ", 5, " Month: ", 10, " Year: ", 2019, " Already Started: ", true);
        /* 
            |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
            |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
            |           StringConcatWithBoxing | 215.6 ns | 2.476 ns | 2.195 ns | 0.1676 |     - |     - |     528 B |
        
            |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
            |--------------------------------- |---------:|-------:|------:|------:|
            |           StringConcatWithBoxing |   227 ms |    167 |     - |     - |

        */

        [Benchmark]
        public string StringConcatWithoutBoxing() =>
            string.Concat("Hi .Net Conf! Day: ", 5.ToString(), " Month: ", 10.ToString(), " Year: ", 2019.ToString(), " Already Started: ", true.ToString());

        /* 
           |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
           |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
           |        StringConcatWithoutBoxing | 162.7 ns | 1.200 ns | 1.122 ns | 0.1092 |     - |     - |     344 B |

           |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
           |--------------------------------- |---------:|-------:|------:|------:|
           |        StringConcatWithoutBoxing |   174 ms |    109 |     - |     - |

       */

        [Benchmark]
        public string StringInterpolationWithoutBoxing() =>
            $"Hi .Net Conf! Day: {5.ToString()}, Month: {10.ToString()}, Year: {2019.ToString()}, Already Started: {true.ToString()})";

        /* 
           |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
           |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
           | StringInterpolationWithoutBoxing | 172.7 ns | 2.157 ns | 1.912 ns | 0.1142 |     - |     - |     360 B |

           |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
           |--------------------------------- |---------:|-------:|------:|------:|
           | StringInterpolationWithoutBoxing |   183 ms |    114 |     - |     - |

       */

        [Benchmark]
        public string StringInterpolationWithBoxing() =>
            $"Hi .Net Conf! Day: {5}, Month: {10}, Year: {2019}, Already Started: {true})";

        /* 
           |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
           |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
           |    StringInterpolationWithBoxing | 473.0 ns | 8.983 ns | 8.403 ns | 0.0982 |     - |     - |     312 B |

           |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
           |--------------------------------- |---------:|-------:|------:|------:|
           |    StringInterpolationWithBoxing |   501 ms |     99 |     - |     - |
       */

        [Benchmark]
        public string StrinPlusWithoutBoxing() =>
            "Hi .Net Conf! Day: " + 5.ToString() + " Month: " + 10.ToString() + " Year: " + 2019.ToString() + " Already Started: " + true.ToString();

        /* 
           |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
           |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
           |           StrinPlusWithoutBoxing | 160.1 ns | 2.206 ns | 2.063 ns | 0.1092 |     - |     - |     344 B |

           |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
           |--------------------------------- |---------:|-------:|------:|------:|
           |           StrinPlusWithoutBoxing |   169 ms |    109 |     - |     - |
       */

        [Benchmark]
        public string StringPlusWithBoxing() =>
            "Hi .Net Conf! Day: " + 5 + " Month: " + 10 + " Year: " + 2019 + " Already Started: " + true;

        /* 
           |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
           |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
           |             StringPlusWithBoxing | 210.8 ns | 1.635 ns | 1.529 ns | 0.1600 |     - |     - |     504 B | 

           |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
           |--------------------------------- |---------:|-------:|------:|------:|
           |             StringPlusWithBoxing |   243 ms |    160 |     - |     - |
       */
    }

    /* 
        Benchmark
        |                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
        |           StringConcatWithBoxing | 215.6 ns | 2.476 ns | 2.195 ns | 0.1676 |     - |     - |     528 B |
        |        StringConcatWithoutBoxing | 162.7 ns | 1.200 ns | 1.122 ns | 0.1092 |     - |     - |     344 B |
        | StringInterpolationWithoutBoxing | 172.7 ns | 2.157 ns | 1.912 ns | 0.1142 |     - |     - |     360 B |
        |    StringInterpolationWithBoxing | 473.0 ns | 8.983 ns | 8.403 ns | 0.0982 |     - |     - |     312 B |
        |           StrinPlusWithoutBoxing | 160.1 ns | 2.206 ns | 2.063 ns | 0.1092 |     - |     - |     344 B |
        |             StringPlusWithBoxing | 210.8 ns | 1.635 ns | 1.529 ns | 0.1600 |     - |     - |     504 B | 
    
        StopWatch and GC class (1M operations)
        |                           Method |     Time |  Gen 0 | Gen 1 | Gen 2 |
        |--------------------------------- |---------:|-------:|------:|------:|
        |           StringConcatWithBoxing |   227 ms |    167 |     - |     - |
        |        StringConcatWithoutBoxing |   174 ms |    109 |     - |     - |
        | StringInterpolationWithoutBoxing |   183 ms |    114 |     - |     - |
        |    StringInterpolationWithBoxing |   501 ms |     99 |     - |     - |
        |           StrinPlusWithoutBoxing |   169 ms |    109 |     - |     - |
        |             StringPlusWithBoxing |   243 ms |    160 |     - |     - |

    */
}
