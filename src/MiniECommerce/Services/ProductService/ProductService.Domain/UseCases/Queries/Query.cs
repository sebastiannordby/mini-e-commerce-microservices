using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries
{
    public abstract record Query<TResponse>(Guid RequestId) : Request<TResponse>(RequestId);
}
