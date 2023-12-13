using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OrderService.Domain.Models.Order;

namespace OrderService.DataAccess.Models
{
    public class OrderLineDao
    {
        public Guid Id { get; private set; }
        public Guid OrderId { get; private set; }
        public int Number { get; private set; }
        public Guid ProductId { get; private set; }
        public string ProductName { get; private set; } 
        public string ProductCategory { get; private set; }
        public string ProductDescription { get; private set; }
        public int Quantity { get; private set; }
        public decimal PricePerQuantity { get; private set; }

        public OrderLineDao()
        {

        }

        public OrderLineDao(Guid orderId, OrderLine orderLine)
        {
            Id = orderLine.Id;
            Number = orderLine.Number;
            ProductId = orderLine.ProductId;
            ProductName = orderLine.ProductName;
            ProductCategory = orderLine.ProductCategory;
            ProductDescription = orderLine.ProductDescription;
            Quantity = orderLine.Quantity;
            PricePerQuantity = orderLine.PricePerQuantity;
        }

        internal void Update(OrderLine orderLine)
        {
            Number = orderLine.Number;
            ProductId = orderLine.ProductId;
            ProductName = orderLine.ProductName;
            ProductCategory = orderLine.ProductCategory;
            Quantity = orderLine.Quantity;
            PricePerQuantity = orderLine.PricePerQuantity;
        }
    }
}
