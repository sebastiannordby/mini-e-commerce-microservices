using ProductService.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.API
{
    internal static class DummyData
    {
        internal static void UseDummyData(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetService<ProductDbContext>();
            var random = new Random();

            context.Products.Add(new()
            {
                Number = 1,
                Name = "Macbook Pro 11\"",
                Category = "Computers",
                Description = "Amazing M1 Macbook Pro, 11 inch pure joy!",
                PricePerQuantity = 20 + random.Next(1, 2500)
            });

            context.Products.Add(new()
            {
                Number = 2,
                Name = "Macbook Pro 12\"",
                Category = "Computers",
                Description = "Amazing M2 Macbook Pro, 12 inch pure joy!",
                PricePerQuantity = 20 + random.Next(1, 2500)
            });

            context.Products.Add(new()
            {
                Number = 3,
                Name = "Macbook Pro 13\"",
                Category = "Computers",
                Description = "Amazing M3 Macbook Pro, 13 inch pure joy!",
                PricePerQuantity = 20 + random.Next(1, 2500)
            });

            context.Products.Add(new()
            {
                Number = 4,
                Name = "Pineapple",
                Category = "Food",
                Description = "Apple, but pine!",
                PricePerQuantity = 20 + random.Next(1, 2500)
            });

            context.Products.Add(new()
            {
                Number = 5,
                Name = "Pear",
                Category = "Food",
                Description = "Pear extra nice!",
                PricePerQuantity = 20 + random.Next(1, 2500)
            });

            context.Products.Add(new()
            {
                Number = 6,
                Name = "Apple",
                Category = "Food",
                Description = "Apple very nice nice",
                PricePerQuantity = 20 + random.Next(1, 2500)
            });

            context.SaveChanges();
        }
    }
}
