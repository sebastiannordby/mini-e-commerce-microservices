using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.UseCases.Commands.CreateProduct;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.UseCases.Commands
{
    public sealed class CreateProductCommandTest : BaseProductServiceTest
    {
        [Test]
        public async Task TestCommand()
        {
            var mediator = Services.GetService<IMediator>();
            var response = await mediator.Send(new CreateProductCommand(
                new ProductDto()
                {
                    Number = 1,
                    Name = nameof(CreateProductCommandTest)
                }
            ));

            var productViews = await mediator.Send(new ListProductViewsQuery());

            Assert.That(response != Guid.Empty);
            Assert.That(productViews, Is.Not.Null);
            Assert.That(productViews, Is.Not.Empty);
        }
    }
}
