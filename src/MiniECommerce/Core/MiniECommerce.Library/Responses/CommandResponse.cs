using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Responses
{
    public sealed record CommandResponse<TData>(
        TData Data    
    );
}
