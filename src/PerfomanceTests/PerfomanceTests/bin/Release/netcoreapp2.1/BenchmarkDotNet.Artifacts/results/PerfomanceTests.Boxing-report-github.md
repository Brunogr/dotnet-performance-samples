``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.17134.1006 (1803/April2018Update/Redstone4)
Intel Core i7-7700HQ CPU 2.80GHz (Kaby Lake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview3-010431
  [Host]     : .NET Core 2.1.13 (CoreCLR 4.6.28008.01, CoreFX 4.6.28008.01), 64bit RyuJIT
  DefaultJob : .NET Core 2.1.13 (CoreCLR 4.6.28008.01, CoreFX 4.6.28008.01), 64bit RyuJIT


```
|                           Method |     Mean |    Error |   StdDev |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------------------------- |---------:|---------:|---------:|-------:|------:|------:|----------:|
|           StringConcatWithBoxing | 215.6 ns | 2.476 ns | 2.195 ns | 0.1676 |     - |     - |     528 B |
|        StringConcatWithoutBoxing | 162.7 ns | 1.200 ns | 1.122 ns | 0.1092 |     - |     - |     344 B |
| StringInterpolationWithoutBoxing | 172.7 ns | 2.157 ns | 1.912 ns | 0.1142 |     - |     - |     360 B |
|    StringInterpolationWithBoxing | 473.0 ns | 8.983 ns | 8.403 ns | 0.0982 |     - |     - |     312 B |
|           StrinPlusWithoutBoxing | 160.1 ns | 2.206 ns | 2.063 ns | 0.1092 |     - |     - |     344 B |
|             StringPlusWithBoxing | 210.8 ns | 1.635 ns | 1.529 ns | 0.1600 |     - |     - |     504 B |
