using ProductService.DataAccess;
using ProductService.Domain.Models;
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
            if (context == null)
                return;

            var random = new Random();

            for (int i = 1; i <= 75; i++)
            {
                context.Products.Add(new()
                {
                    Number = i,
                    Name = GetRandomProductName(random),
                    Category = GetRandomCategory(random),
                    Description = GetRandomDescription(random),
                    PricePerQuantity = 20 + random.Next(1, 2500)
                });
            }

            context.SaveChanges();
        }

        internal static string GetRandomProductName(Random random)
        {
            // Generate random product names here.
            string[] productNames = {
                "Macbook Pro 11\"",
                "iPhone 13 Pro",
                "Samsung Galaxy S21",
                "Dell XPS 13",
                "Sony 65-Inch 4K TV",
                "Microsoft Surface Laptop",
                "Google Pixel 6",
                "HP Envy x360",
                "Canon EOS 5D Mark IV",
                "Bose QuietComfort 35 II",
                "LG OLED 55-Inch TV",
                "Sony Playstation 5",
                "Apple Watch Series 7",
                "Amazon Echo Dot",
                "Lenovo ThinkPad X1 Carbon",
                "Nikon D850",
                "Sony WH-1000XM4 Headphones",
                "iPad Air 4th Gen",
                "Xiaomi Mi 11",
                "LG Gram 17",
                "Logitech G Pro X Gaming Keyboard"
            };

            return productNames[random.Next(productNames.Length)];
        }

        internal static string GetRandomCategory(Random random)
        {
            // Generate random categories here.
            string[] categories = {
                "Computers",
                "Smartphones",
                "Tablets",
                "Laptops",
                "TVs",
                "Cameras",
                "Headphones",
                "Gaming Consoles",
                "Wearable Tech",
                "Audio Equipment",
                "Home Appliances",
                "Home Decor",
                "Furniture",
                "Kitchen Gadgets",
                "Sports & Outdoors",
                "Fashion & Apparel",
                "Books & Literature",
                "Toys & Games",
                "Health & Fitness",
                "Beauty & Personal Care"
            };

            return categories[random.Next(categories.Length)];
        }

        internal static string GetRandomDescription(Random random)
        {
            // Generate random product descriptions here.
            string[] descriptions = {
                "Amazing product!",
                "High-quality and reliable.",
                "The latest technology.",
                "Great value for money.",
                "Sleek and stylish design.",
                "Unmatched performance.",
                "Innovative features.",
                "Perfect for work and play.",
                "Exceptional camera quality.",
                "Immersive entertainment experience.",
                "Efficient and portable.",
                "Cutting-edge gaming performance.",
                "Stay connected with ease.",
                "A must-have for tech enthusiasts.",
                "Enhance your productivity.",
                "Capture memories in stunning detail.",
                "Superb audio quality.",
                "Lightweight and durable construction.",
                "Stay ahead of the curve.",
                "Elevate your entertainment setup."
            };

            return descriptions[random.Next(descriptions.Length)];
        }
    }
}
