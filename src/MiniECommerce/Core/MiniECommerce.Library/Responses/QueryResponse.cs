using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Library.Responses
{
    public record QueryResponse<TData>(
        TData Data
    );
}
