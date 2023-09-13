using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Models;

public class Order
{
    public Guid Id { get; set; }
    public int Number { get; set; }

    public class OrderLine
    {
        public Guid OrderId { get; set; }
        public int Number { get; set; }
        public Guid ProductId { get; set; }
    }
}
