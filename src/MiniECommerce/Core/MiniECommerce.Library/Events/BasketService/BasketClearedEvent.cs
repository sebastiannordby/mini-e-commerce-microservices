using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Events.BasketService
{
    public sealed record BasketClearedEvent(
        string UserEmail
    );
}
