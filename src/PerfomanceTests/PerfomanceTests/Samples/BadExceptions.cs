using BenchmarkDotNet.Attributes;
using PerfomanceTests.Exceptions;
using PerfomanceTests.Models;

namespace PerfomanceTests.Samples
{
    [MemoryDiagnoser]
    public class BadExceptions
    {
        #region .:: Exceptions ::.

        [Benchmark]
        public void Codigo_Com_Exceptions()
        {
            for (int i = 0; i < 100_000; i++)
                try
                {
                    if (i % 2 == 0)
                         ProcessarPagamentoEx(string.Empty, 150M);
                    else ProcessarPagamentoEx(string.Empty, 0);
                }
                catch (ValorZeroException)
                {
                    // Tratamentos, Logs, Retornos para o front
                    continue;
                }
                catch (NomeInvalidoException)
                {
                    // Tratamentos, Logs, Retornos para o front
                    continue;
                }
        }

        private void ProcessarPagamentoEx(string nome, decimal valor)
        {
            if (valor == 0)
                throw new ValorZeroException();

            if (string.IsNullOrEmpty(nome))
                throw new NomeInvalidoException();

            // Code ...
        }

        #endregion

        #region .:: Sem Exceptions ::.

        private const string NomeInvalidoErro = "Nome invalido";
        private const string ValorZeroErro = "Valor a ser pago não pode ser zero";

        [Benchmark(Baseline = true)]
        public void Codigo_Sem_Exceptions()
        {
            for (int i = 0; i < 100_000; i++)
            {
                ErrorModel retorno = i % 2 == 0
                    ? ProcessarPagamentoModel(string.Empty, 150M)
                    : ProcessarPagamentoModel(string.Empty, 0);

                if (retorno.Codigo > 0)
                {
                    // Tratamentos, Logs, Retornos para o front
                    continue;
                }
            }
        }

        public ErrorModel ProcessarPagamentoModel(string nome, decimal valor)
        {
            if (valor == 0)
                return new ErrorModel(ValorZeroErro, 1);

            if (string.IsNullOrEmpty(nome))
                return new ErrorModel(NomeInvalidoErro, 2);

            // Code ...

            return default;
        }

        #endregion
    }

    // |                Method |         Mean |      Error |     StdDev |    Ratio | RatioSD |      Gen 0 | Gen 1 | Gen 2 |  Allocated |
    // |---------------------- |-------------:|-----------:|-----------:|---------:|--------:|-----------:|------:|------:|-----------:|
    // | Codigo_Com_Exceptions | 2,506.080 ms | 18.9906 ms | 15.8580 ms | 1,347.35 |   11.10 | 21000.0000 |     - |     - | 33600000 B |
    // | Codigo_Sem_Exceptions |     1.860 ms |  0.0086 ms |  0.0076 ms |     1.00 |    0.00 |          - |     - |     - |          - |
}