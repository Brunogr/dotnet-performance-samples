``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.1006 (1803/April2018Update/Redstone4)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 2.1.13 (CoreCLR 4.6.28008.01, CoreFX 4.6.28008.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.13 (CoreCLR 4.6.28008.01, CoreFX 4.6.28008.01), 64bit RyuJIT


```
|                  Method |      Mean |     Error |    StdDev | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------------ |----------:|----------:|----------:|------:|--------:|-------:|------:|------:|----------:|
|             RunOnStruct | 0.0969 ns | 0.0014 ns | 0.0013 ns |  1.00 |    0.00 |      - |     - |     - |         - |
| RunOnStructWithOverride | 0.2076 ns | 0.0034 ns | 0.0032 ns |  2.14 |    0.03 | 0.0001 |     - |     - |         - |
|              RunOnClass | 0.0970 ns | 0.0016 ns | 0.0013 ns |  1.00 |    0.02 |      - |     - |     - |         - |
