using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Commands
{
    public class StartOrderCommandDto
    {
        public required string BuyersFullName { get; set; }
        public required string BuyersEmailAddress { get; set; }
    }
}
