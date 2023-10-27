using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Events.OrderService
{
    public record OrderStartedEvent(
        Guid OrderId,
        string UserEmail,
        DateTime StartedUtc
    );
}
