using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.List
{
    public sealed record ListProductViewsQuery(
        string? Search,
        decimal? FromPricePerQuantity = null, 
        decimal? ToPricePerQuantity = null, 
        IEnumerable<string>? Categories = null
    ) : Query<IEnumerable<ProductView>>();
}
