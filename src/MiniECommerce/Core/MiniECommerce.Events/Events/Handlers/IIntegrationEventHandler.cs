using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Events.Events.Handlers
{
    public interface IIntegrationEventHandler<T>
    {
        Task Handle(T @event);
    }
}
