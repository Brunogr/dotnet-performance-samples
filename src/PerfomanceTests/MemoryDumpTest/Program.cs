using MemoryDumpTest.Data;
using System;
using System.Collections.Generic;

namespace MemoryDumpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var repository = new ShopCartRepository();
            List<ShopCart> carrinhos = new List<ShopCart>();

            Console.WriteLine("Starting to fetch data.");

            for (int i = 0; i < 500; i++)
            {
                var carrinho = repository.Get();
                    carrinhos.Add(carrinho);

                if (i % 100 == 0)
                {
                    Console.WriteLine("Deallocating objects!");
                    for (int j = 0; j < i; j++)
                    {
                        carrinhos[j] = null;
                    }
                }
            }

            Console.WriteLine("Test Finalized.");
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
