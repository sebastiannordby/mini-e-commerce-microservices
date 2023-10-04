using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.ListProducts
{
    public sealed record ListProductQuery(
        Guid RequestId
    ) : Query<IEnumerable<ProductView>>(RequestId);
}
