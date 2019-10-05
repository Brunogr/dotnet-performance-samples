using BenchmarkDotNet.Attributes;

namespace PerfomanceTests.Samples
{
    [MemoryDiagnoser]
    public class StructBoxing
    {
        #region .: Normal Struct :.

        public class StructBox
        {
            public readonly int A;
            public readonly int B;

            public StructBox(int a, int b)
            {
                A = a;
                B = b;
            }
        }

        #endregion

        #region .: Struct With Override :.

        public class StructBoxOverride
        {
            public readonly int A;
            public readonly int B;

            public StructBoxOverride(int a, int b)
            {
                A = a;
                B = b;
            }

            public override string ToString()
            {
                return (A + B).ToString();
            }
        }

        #endregion

        #region .: Normal Class :.

        public class ClassNoBox
        {
            public readonly int A;
            public readonly int B;

            public ClassNoBox(int a, int b)
            {
                A = a;
                B = b;
            }
        }

        #endregion

        public StructBox ValueType { get; set; }
        public ClassNoBox ReferenceType { get; set; }
        public StructBoxOverride ValueTypeOverride { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            ValueType = new StructBox(10, 6);
            ReferenceType = new ClassNoBox(10, 6);
            ValueTypeOverride = new StructBoxOverride(10, 6);
        }

        [Benchmark(Baseline = true, OperationsPerInvoke = 100)]
        public void RunOnStruct() =>
            ValueType.ToString();

        [Benchmark(OperationsPerInvoke = 100)]
        public void RunOnStructWithOverride() =>
            ValueTypeOverride.ToString();

        [Benchmark(OperationsPerInvoke = 100)]
        public void RunOnClass() =>
            ReferenceType.ToString();
    }
}