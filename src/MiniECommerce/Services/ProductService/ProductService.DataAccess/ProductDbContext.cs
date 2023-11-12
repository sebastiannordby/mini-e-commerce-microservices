using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess
{
    internal sealed class ProductDbContext : DbContext
    {
        internal DbSet<ProductDao> Products { get; set; }
        internal DbSet<ProductPurchaseStatsDao> ProductPurchaseStats { get; set; }

        public ProductDbContext(
            DbContextOptions options) : base(options)
        {

        }
    }
}
