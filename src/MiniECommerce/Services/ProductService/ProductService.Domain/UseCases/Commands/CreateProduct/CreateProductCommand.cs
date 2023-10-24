using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Commands.CreateProduct
{
    public sealed record CreateProductCommand(
        ProductDto Product
    ) : Command<Guid>();
}
