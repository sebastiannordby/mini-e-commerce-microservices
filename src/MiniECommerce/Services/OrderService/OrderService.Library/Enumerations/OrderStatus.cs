using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Enumerations
{
    public enum OrderStatus
    {
        InFill = 0,
        WaitingForConfirmation = 1,
        Confirmed = 2,
        Delivered = 3
    }
}
