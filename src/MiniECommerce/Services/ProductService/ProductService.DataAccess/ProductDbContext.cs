using Microsoft.EntityFrameworkCore;
using ProductService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.DataAccess
{
    public class ProductDbContext : DbContext
    {
        public DbSet<ProductDao> Products { get; set; }

        public ProductDbContext(
            DbContextOptions options) : base(options)
        {

        }
    }
}
