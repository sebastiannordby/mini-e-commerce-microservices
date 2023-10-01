using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.DataAccess.Models
{
    public class OrderLineDao
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public int Number { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductDescription { get; private set; }
        public int Quantity { get; private set; }
        public decimal PricePerQuantity { get; private set; }
    }
}
