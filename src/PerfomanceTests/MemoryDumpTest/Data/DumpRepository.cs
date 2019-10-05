using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryDumpTest.Data
{
    public class CarrinhoRepository
    {
        public Carrinho Get()
        {
            var produtos = new List<Produto>()
            {
                new Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = new Faker().Commerce.ProductName(),
                    Descricao = new Faker().Lorem.Sentences(50000)
                },
                new Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = new Faker().Commerce.ProductName(),
                    Descricao = new Faker().Lorem.Sentences(50000)
                },
                new Produto()
                {
                    Id = Guid.NewGuid(),
                    Nome = new Faker().Commerce.ProductName(),
                    Descricao = new Faker().Lorem.Sentences(50000)
                }
            };

            return new Carrinho() { Id = Guid.NewGuid(), Produtos = produtos };
        }

    }

    public class Carrinho
    {
        ~Carrinho()
        {
            for (int i = 0; i < Produtos.Count(); i++)
            {
                Produtos[i].GetHashCode();
                Produtos[i] = null;
            }
        }
        public Guid Id { get; set; }
        public List<Produto> Produtos { get; set; }
    }

    public class Produto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
    }
}
