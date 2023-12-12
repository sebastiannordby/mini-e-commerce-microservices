using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Queries.ListCategories
{
    public record class ListProductCategoriesQuery : IRequest<IEnumerable<string>>;
}
