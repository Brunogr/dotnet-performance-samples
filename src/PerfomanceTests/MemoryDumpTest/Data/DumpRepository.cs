using Bogus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MemoryDumpTest.Data
{
    public class ShopCartRepository
    {
        public ShopCart Get()
        {
            var produtos = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = new Faker().Commerce.ProductName(),
                    Description = new Faker().Lorem.Sentences(50000)
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = new Faker().Commerce.ProductName(),
                    Description = new Faker().Lorem.Sentences(50000)
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = new Faker().Commerce.ProductName(),
                    Description = new Faker().Lorem.Sentences(50000)
                }
            };

            return new ShopCart() { Id = Guid.NewGuid(), Products = produtos };
        }

    }

    public class ShopCart
    {
        ~ShopCart()
        {
            for (int i = 0; i < Products.Count(); i++)
            {
                Products[i] = null;
            }
        }
        public Guid Id { get; set; }
        public List<Product> Products { get; set; }
    }

    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
