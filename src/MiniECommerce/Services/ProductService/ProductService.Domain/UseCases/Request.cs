using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases
{
    public abstract record Request() : IRequest;
    public abstract record Request<TResponse>() : IRequest<TResponse>;
}
