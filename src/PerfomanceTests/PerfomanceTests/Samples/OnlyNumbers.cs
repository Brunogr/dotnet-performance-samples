using BenchmarkDotNet.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PerfomanceTests.Samples
{
    [MemoryDiagnoser]
    public class OnlyNumbers
    {
        [Params("Text 123 and 456 numbers 789", "123456789", "Only Text: bla bla bla")]
        public string Text { get; set; }


        private const string ONLY_NUMBERS_REGEX = "[^0-9]";

        [Benchmark(Baseline = true)]
        public string RegexReplace() =>
            Regex.Replace(Text, ONLY_NUMBERS_REGEX, string.Empty);

        /*
        |                   Method |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        |             RegexReplace |            123456789 |   297.87 ns |   7.0558 ns |   19.785 ns |   292.03 ns |  1.00 |    0.00 | 0.0329 |     - |     - |     104 B |
        |             RegexReplace | Only (...)a bla [22] | 5,692.11 ns | 113.7007 ns |  227.072 ns | 5,631.33 ns | 1.000 |    0.00 | 1.4801 |     - |     - |    4680 B |
        |             RegexReplace | Text (...)s 789 [28] | 5,411.21 ns | 109.6562 ns |  268.989 ns | 5,392.42 ns |  1.00 |    0.00 | 1.2970 |     - |     - |    4104 B |
        */

        [Benchmark]
        public string Safe()
        {
            List<char> output = new List<char>();

            foreach (var @char in Text)
            {
                if (!char.IsNumber(@char))
                    continue;

                output.Add(@char);
            }

            return new string(output.ToArray(), 0, output.Count());
        }

        /*
        |                   Method |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        |                     Safe |            123456789 |   194.67 ns |   3.9818 ns |    8.399 ns |   193.33 ns |  0.65 |    0.06 | 0.0837 |     - |     - |     264 B |
        |                     Safe | Only (...)a bla [22] |    87.22 ns |   1.8015 ns |    2.212 ns |    86.99 ns | 0.015 |    0.00 | 0.0126 |     - |     - |      40 B |
        |                     Safe | Text (...)s 789 [28] |   245.43 ns |   4.9125 ns |   10.362 ns |   244.47 ns |  0.05 |    0.00 | 0.0834 |     - |     - |     264 B |
         */

        [Benchmark]
        public unsafe string Unsafe()
        {
            char* output = stackalloc char[Text.Length];
            char* current = output;
            int count = 0;

            foreach (var @char in Text)
            {
                if (!char.IsNumber(@char))
                    continue;

                *current++ = @char;
                count++;
            }

            return new string(output, 0, count);
        }

        /*
        |                   Method |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        |                   Unsafe |            123456789 |    36.60 ns |   0.8085 ns |    1.395 ns |    36.55 ns |  0.12 |    0.01 | 0.0152 |     - |     - |      48 B |
        |                   Unsafe | Only (...)a bla [22] |    56.31 ns |   1.1788 ns |    1.728 ns |    55.99 ns | 0.010 |    0.00 |      - |     - |     - |         - |
        |                   Unsafe | Text (...)s 789 [28] |    85.57 ns |   1.7737 ns |    2.368 ns |    84.80 ns |  0.02 |    0.00 | 0.0151 |     - |     - |      48 B |
         */

        [Benchmark]
        public string NoRegex()
        {
            int newStringLenght = 0;
            for (int i = 0; i < Text.Length; i++)
                if (char.IsNumber(Text[i]))
                    newStringLenght++;

            var newString = new char[newStringLenght];
            for (int i = 0, j = 0; i < Text.Length; i++)
                if (char.IsNumber(Text[i]))
                    newString[j++] = Text[i];

            return new string(newString);
        }

        /*
        |                   Method |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        |                  NoRegex |            123456789 |    71.05 ns |   1.5068 ns |    3.808 ns |    70.88 ns |  0.24 |    0.02 | 0.0304 |     - |     - |      96 B |
        |                  NoRegex | Only (...)a bla [22] |   118.14 ns |   2.4351 ns |    5.189 ns |   117.59 ns | 0.021 |    0.00 | 0.0074 |     - |     - |      24 B |
        |                  NoRegex | Text (...)s 789 [28] |   165.60 ns |   3.4731 ns |    4.636 ns |   164.52 ns |  0.03 |    0.00 | 0.0303 |     - |     - |      96 B |
         */

        [Benchmark]
        public string NoRegex_WithSpan()
        {

            int newStringLenght = 0;
            for (int i = 0; i < Text.Length; i++)
                if (char.IsNumber(Text[i]))
                    newStringLenght++;

            Span<char> newString = stackalloc char[newStringLenght];
            for (int i = 0, j = 0; i < Text.Length; i++)
                if (char.IsNumber(Text[i]))
                    newString[j++] = Text[i];

            return newString.ToString();
        }

        /*
        |                   Method |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        |         NoRegex_WithSpan |            123456789 |    74.14 ns |   1.5659 ns |    2.195 ns |    74.34 ns |  0.24 |    0.02 | 0.0151 |     - |     - |      48 B |
        |         NoRegex_WithSpan | Only (...)a bla [22] |   135.02 ns |   2.7847 ns |    4.950 ns |   134.93 ns | 0.024 |    0.00 |      - |     - |     - |         - |
        |         NoRegex_WithSpan | Text (...)s 789 [28] |   177.63 ns |   3.7799 ns |    7.461 ns |   176.26 ns |  0.03 |    0.00 | 0.0150 |     - |     - |      48 B |
         */

        [Benchmark]
        public string NoRegex_WithStringCreate()
        {
            int newStringLenght = 0;
            for (int i = 0; i < Text.Length; i++)
                if (char.IsNumber(Text[i]))
                    newStringLenght++;

            return string.Create(newStringLenght, Text, (newString, oldString) =>
            {
                for (int i = 0, j = 0; i < oldString.Length; i++)
                    if (char.IsNumber(oldString[i]))
                        newString[j++] = oldString[i];
            });
        }

        /*
        |                   Method |                 Text |        Mean |       Error |      StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|------------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        | NoRegex_WithStringCreate |            123456789 |    55.66 ns |   1.1914 ns |    1.746 ns |    55.44 ns |  0.18 |    0.01 | 0.0151 |     - |     - |      48 B |
        | NoRegex_WithStringCreate | Only (...)a bla [22] |    70.05 ns |   1.4855 ns |    1.525 ns |    70.01 ns | 0.012 |    0.00 |      - |     - |     - |         - |
        | NoRegex_WithStringCreate | Text (...)s 789 [28] |   163.94 ns |   3.2997 ns |    5.513 ns |   163.60 ns |  0.03 |    0.00 | 0.0150 |     - |     - |      48 B |
         */


        /*
        |                   Method |                 Text |        Mean |       Error |     StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
        |------------------------- |--------------------- |------------:|------------:|-----------:|------------:|------:|--------:|-------:|------:|------:|----------:|
        |             RegexReplace |            123456789 |   297.87 ns |   7.0558 ns |  19.785 ns |   292.03 ns |  1.00 |    0.00 | 0.0329 |     - |     - |     104 B |
        |                     Safe |            123456789 |   194.67 ns |   3.9818 ns |   8.399 ns |   193.33 ns |  0.65 |    0.06 | 0.0837 |     - |     - |     264 B |
        |                   Unsafe |            123456789 |    36.60 ns |   0.8085 ns |   1.395 ns |    36.55 ns |  0.12 |    0.01 | 0.0152 |     - |     - |      48 B |
        |                  NoRegex |            123456789 |    71.05 ns |   1.5068 ns |   3.808 ns |    70.88 ns |  0.24 |    0.02 | 0.0304 |     - |     - |      96 B |
        |         NoRegex_WithSpan |            123456789 |    74.14 ns |   1.5659 ns |   2.195 ns |    74.34 ns |  0.24 |    0.02 | 0.0151 |     - |     - |      48 B |
        | NoRegex_WithStringCreate |            123456789 |    55.66 ns |   1.1914 ns |   1.746 ns |    55.44 ns |  0.18 |    0.01 | 0.0151 |     - |     - |      48 B |
        |                          |                      |             |             |            |             |       |         |        |       |       |           |
        |             RegexReplace | Only (...)a bla [22] | 5,692.11 ns | 113.7007 ns | 227.072 ns | 5,631.33 ns | 1.000 |    0.00 | 1.4801 |     - |     - |    4680 B |
        |                     Safe | Only (...)a bla [22] |    87.22 ns |   1.8015 ns |   2.212 ns |    86.99 ns | 0.015 |    0.00 | 0.0126 |     - |     - |      40 B |
        |                   Unsafe | Only (...)a bla [22] |    56.31 ns |   1.1788 ns |   1.728 ns |    55.99 ns | 0.010 |    0.00 |      - |     - |     - |         - |
        |                  NoRegex | Only (...)a bla [22] |   118.14 ns |   2.4351 ns |   5.189 ns |   117.59 ns | 0.021 |    0.00 | 0.0074 |     - |     - |      24 B |
        |         NoRegex_WithSpan | Only (...)a bla [22] |   135.02 ns |   2.7847 ns |   4.950 ns |   134.93 ns | 0.024 |    0.00 |      - |     - |     - |         - |
        | NoRegex_WithStringCreate | Only (...)a bla [22] |    70.05 ns |   1.4855 ns |   1.525 ns |    70.01 ns | 0.012 |    0.00 |      - |     - |     - |         - |
        |                          |                      |             |             |            |             |       |         |        |       |       |           |
        |             RegexReplace | Text (...)s 789 [28] | 5,411.21 ns | 109.6562 ns | 268.989 ns | 5,392.42 ns |  1.00 |    0.00 | 1.2970 |     - |     - |    4104 B |
        |                     Safe | Text (...)s 789 [28] |   245.43 ns |   4.9125 ns |  10.362 ns |   244.47 ns |  0.05 |    0.00 | 0.0834 |     - |     - |     264 B |
        |                   Unsafe | Text (...)s 789 [28] |    85.57 ns |   1.7737 ns |   2.368 ns |    84.80 ns |  0.02 |    0.00 | 0.0151 |     - |     - |      48 B |
        |                  NoRegex | Text (...)s 789 [28] |   165.60 ns |   3.4731 ns |   4.636 ns |   164.52 ns |  0.03 |    0.00 | 0.0303 |     - |     - |      96 B |
        |         NoRegex_WithSpan | Text (...)s 789 [28] |   177.63 ns |   3.7799 ns |   7.461 ns |   176.26 ns |  0.03 |    0.00 | 0.0150 |     - |     - |      48 B |
        | NoRegex_WithStringCreate | Text (...)s 789 [28] |   163.94 ns |   3.2997 ns |   5.513 ns |   163.60 ns |  0.03 |    0.00 | 0.0150 |     - |     - |      48 B |
        */
    }
}
