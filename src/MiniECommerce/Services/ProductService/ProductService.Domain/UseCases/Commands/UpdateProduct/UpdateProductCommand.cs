using MediatR;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Commands.UpdateProduct
{
    public sealed record UpdateProductCommand(
        Guid RequestId,
        ProductDto Product
    ) : Command(RequestId);
}
