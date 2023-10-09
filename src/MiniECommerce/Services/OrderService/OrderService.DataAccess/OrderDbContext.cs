using Microsoft.EntityFrameworkCore;
using OrderService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess
{
    public class OrderDbContext : DbContext
    {
        internal DbSet<OrderDao> Orders { get; set; }
        internal DbSet<OrderLineDao> OrderLines { get; set; }

        public OrderDbContext(DbContextOptions options) : base(options)
        {

        }
    }
}
