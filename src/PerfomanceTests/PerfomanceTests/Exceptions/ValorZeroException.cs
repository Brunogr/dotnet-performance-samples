using System;

namespace PerfomanceTests.Exceptions
{
    public class ValorZeroException : Exception
    {
        private const string ErrorMotivo = "Valor a ser pago não pode ser zero!";

        public ValorZeroException() : base(ErrorMotivo)
        {
        }
    }
}