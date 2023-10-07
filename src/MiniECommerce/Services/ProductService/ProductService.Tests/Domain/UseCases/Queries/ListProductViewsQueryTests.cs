using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.UseCases.Queries.List;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.UseCases.Queries
{
    public sealed class ListProductViewsQueryTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestQuery()
        {
            var mediator = Services.GetService<IMediator>();
            var productViews = await mediator.Send(new ListProductViewsQuery(Guid.NewGuid()));

            Assert.IsNotNull(productViews);
        }
    }
}
