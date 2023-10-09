using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Commands
{
    public class StartOrderCommandDto
    {
        public Guid BasketId { get; set; }
        public string BuyersFullName { get; set; }
        public string BuyersEmailAddress { get; set; }
    }
}
