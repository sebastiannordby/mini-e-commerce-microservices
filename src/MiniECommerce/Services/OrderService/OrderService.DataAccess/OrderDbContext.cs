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
        public DbSet<OrderDao> Orders { get; set; }
        public DbSet<OrderLineDao> OrderLines { get; set; }
    }
}
