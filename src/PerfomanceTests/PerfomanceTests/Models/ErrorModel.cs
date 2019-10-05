namespace PerfomanceTests.Models
{
    public struct ErrorModel
    {
        public readonly string Motivo;
        public readonly int Codigo;

        public ErrorModel(string motivo, int codigo)
        {
            Motivo = motivo;
            Codigo = codigo;
        }
    }
}