using BenchmarkDotNet.Running;
using PerfomanceTests.Samples;
using System;

namespace PerfomanceTests
{
    class Program
    {
        static void Main(string[] args)
        {
            // var summary = BenchmarkRunner.Run<Boxing>();
            var summary = BenchmarkRunner.Run<OnlyNumbers>();
            // var summary = BenchmarkRunner.Run<StructBoxing>();
            // var summary = BenchmarkRunner.Run<StructClassAlocation>();
            //var summary = BenchmarkRunner.Run<MissCache>();
        }

    }
}
