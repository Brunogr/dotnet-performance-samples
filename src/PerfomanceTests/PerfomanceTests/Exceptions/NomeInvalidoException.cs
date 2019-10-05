using System;

namespace PerfomanceTests.Exceptions
{
    internal class NomeInvalidoException : Exception
    {
        private const string ErroMensagem = "Nome não pode ser em branco";

        public NomeInvalidoException() : base(ErroMensagem)
        {
        }
    }
}