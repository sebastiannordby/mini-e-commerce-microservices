using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases
{
    public abstract record Request(Guid RequestId) : IRequest;
    public abstract record Request<TResponse>(Guid RequestId) : IRequest<TResponse>;
}
