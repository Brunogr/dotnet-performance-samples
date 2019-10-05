using BenchmarkDotNet.Attributes;
using PerfomanceTests.Models;

namespace PerfomanceTests.Samples
{
    [MemoryDiagnoser]
    public class StructClassAlocation
    {
        [Params(100, 1000)]
        public int AllocationCount;

        public NiceClass[] ClassList;
        public NiceStruct[] StructList;

        [GlobalSetup]
        public void Setup()
        {
            ClassList = new NiceClass[AllocationCount];
            StructList = new NiceStruct[AllocationCount];
        }

        [Benchmark(Baseline = true)]
        public void StructAlloc()
        {
            for (int i = 0; i < AllocationCount; i++)
                StructList[i] = new NiceStruct(i, i);
        }

        [Benchmark]
        public void ClassAlloc()
        {
            for (int i = 0; i < AllocationCount; i++)
                ClassList[i] = new NiceClass(i, i);
        }
    }

    // |      Method | AllocationCount |       Mean |      Error |     StdDev | Ratio | RatioSD |   Gen 0 | Gen 1 | Gen 2 | Allocated |
    // |------------ |---------------- |-----------:|-----------:|-----------:|------:|--------:|--------:|------:|------:|----------:|
    // | StructAlloc |             100 |   161.4 ns |   1.670 ns |   1.304 ns |  1.00 |    0.00 |       - |     - |     - |         - |
    // |  ClassAlloc |             100 |   878.6 ns |   6.467 ns |   5.733 ns |  5.46 |    0.04 |  1.5240 |     - |     - |    2400 B |
    // |             |                 |            |            |            |       |         |         |       |       |           |
    // | StructAlloc |            1000 | 1,574.3 ns |  30.536 ns |  33.941 ns |  1.00 |    0.00 |       - |     - |     - |         - |
    // |  ClassAlloc |            1000 | 9,388.6 ns | 204.998 ns | 331.034 ns |  6.01 |    0.26 | 15.2435 |     - |     - |   24000 B |
}