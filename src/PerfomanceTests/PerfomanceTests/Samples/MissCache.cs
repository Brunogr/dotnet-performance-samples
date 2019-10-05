using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Diagnosers;
using PerfomanceTests.Models;
using System.Linq;

namespace PerfomanceTests.Samples
{
    [HardwareCounters(HardwareCounter.CacheMisses)]
    public class MissCache
    {
        [Params(100, 10_000)]
        public int AllocationCount;

        public NiceClass[] ClassList;
        public NiceStruct[] StructList;

        [GlobalSetup]
        public void Setup()
        {
            ClassList = Enumerable.Repeat(0, AllocationCount)
                                  .Select((s, i) => new NiceClass(i, i))
                                  .ToArray();

            StructList = Enumerable.Repeat(0, AllocationCount)
                                  .Select((s, i) => new NiceStruct(i, i))
                                  .ToArray();
        }

        [Benchmark(Baseline = true)]
        public void StructsSum()
        {
            int itemASum = 0, itemBSum = 0;
            for (int i = 0; i < StructList.Length; i++)
            {
                ref NiceStruct reference = ref StructList[i];
                itemASum += reference.NumberA;
                itemBSum += reference.NumberB;
            }
        }

        [Benchmark]
        public void ClassesSum()
        {
            int itemASum = 0, itemBSum = 0;
            for (int i = 0; i < ClassList.Length; i++)
            {
                ref NiceClass reference = ref ClassList[i];
                itemASum += reference.NumberA;
                itemBSum += reference.NumberB;
            }
        }
    }

    // |     Method | AllocationCount |        Mean |      Error |     StdDev | Ratio | RatioSD | CacheMisses/Op |
    // |----------- |---------------- |------------:|-----------:|-----------:|------:|--------:|---------------:|
    // | StructsSum |             100 |    134.1 ns |   1.602 ns |   1.338 ns |  1.00 |    0.00 |              0 |
    // | ClassesSum |             100 |    320.1 ns |   3.119 ns |   2.918 ns |  2.39 |    0.03 |              0 |
    // |            |                 |             |            |            |       |         |                |
    // | StructsSum |           10000 | 12,406.8 ns | 105.205 ns |  93.261 ns |  1.00 |    0.00 |              0 |
    // | ClassesSum |           10000 | 30,947.7 ns | 140.836 ns | 131.738 ns |  2.49 |    0.02 |              1 |
}