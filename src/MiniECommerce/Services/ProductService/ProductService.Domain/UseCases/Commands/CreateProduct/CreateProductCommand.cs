using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        Guid RequestId,
        ProductDto Product
    ) : Command<Guid>(RequestId);
}
