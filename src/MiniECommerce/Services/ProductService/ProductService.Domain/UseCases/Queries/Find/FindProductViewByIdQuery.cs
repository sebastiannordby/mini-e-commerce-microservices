using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.Find
{
    public sealed record FindProductViewByIdQuery(
        Guid ProductId
    ) : Query<ProductView?>();
}
