using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Commands
{
    public abstract record Command() : Request();
    public abstract record Command<TResult>() : Request<TResult>();
}
