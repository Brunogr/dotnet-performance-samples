using MemoryDumpTest.Data;
using System;
using System.Collections.Generic;

namespace MemoryDumpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new CarrinhoRepository();
            List<Carrinho> carrinhos = new List<Carrinho>();

            Console.WriteLine("Iniciando recuperação de dados.");

            for (int i = 0; i < 1_000; i++)
            {
                var carrinho = repository.Get();
                    carrinhos.Add(carrinho);

                if (i % 100 == 0)
                {
                    Console.WriteLine("Desalocando objetos!");
                    for (int j = 0; j < i; j++)
                    {
                        carrinhos[j] = null;
                    }
                }
            }

            Console.WriteLine("Teste finalizado.");
            Console.ReadKey();
        }
    }


    //public class FooService : IFooService
    //{
    //    public FooService(IServiceProvider serviceProvider)
    //    {
    //        serviceProvider = serviceProvider;
    //    }

    //    public Ping GetPing(Guid id)
    //    {
    //        var pingRepository = serviceProvider.GetService(typeof(IPingRepository));
    //        return pingRepository.Get(id);
    //    }
    //}
}
