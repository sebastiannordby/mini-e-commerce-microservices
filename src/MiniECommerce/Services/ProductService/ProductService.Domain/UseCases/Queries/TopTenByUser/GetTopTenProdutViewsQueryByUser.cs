using MediatR;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.TopTenByUser
{
    public record GetTopTenProdutViewsQueryByUser : IRequest<IEnumerable<ProductView>>;
}
