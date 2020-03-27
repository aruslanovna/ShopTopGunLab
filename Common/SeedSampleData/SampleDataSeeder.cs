using Common.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Common.SeedSampleData
{
   public class SampleDataSeeder
    {

        private readonly IShopDbContext _context;

        public SampleDataSeeder(IShopDbContext context)
        {
            _context = context;
        
        }


        private readonly Dictionary<int, Product> Employees = new Dictionary<int, Product>();
        public async Task SeedAllAsync(CancellationToken cancellationToken)
        {
            if (_context.products.Any())
            {
                return;
            }

            await SeedProductsAsync(cancellationToken);

        }

        private async Task SeedProductsAsync(CancellationToken cancellationToken)
        {
            
                var productsList = new[]
                {
                new Product { ProductId = 1, Name = "Bread", Quantity = 200 , dimension = 0},
               new Product { ProductId = 2, Name = "Banana", Quantity = 200  , dimension = 0},
               new Product { ProductId = 3, Name = "Apple", Quantity = 200 , dimension = 0 },
               new Product { ProductId = 4, Name = "Oil", Quantity = 200 , dimension = 0 }
            };

                _context.products.AddRange(productsList);

                await _context.SaveChangesAsync(cancellationToken);
            
        }
    }
}

