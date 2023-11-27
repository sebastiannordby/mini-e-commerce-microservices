using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Library.Enumerations
{
    public enum OrderStatus
    {
        WaitingForDeliveryAddress = 0,
        WaitingForInvoiceAddress = 1,
        WaitingForConfirmation = 100,
        Confirmed = 200,
        Delivered = 300
    }
}
