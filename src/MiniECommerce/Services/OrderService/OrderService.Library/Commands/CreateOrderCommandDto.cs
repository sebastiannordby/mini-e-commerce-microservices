using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Commands
{
    public class CreateOrderCommandDto
    {
        public string UserPrincipalName { get; set; }

        public class OrderLine
        {
            public Guid ProductId { get; set; }
            public int Quantity { get; set; }
        }
    }
}
