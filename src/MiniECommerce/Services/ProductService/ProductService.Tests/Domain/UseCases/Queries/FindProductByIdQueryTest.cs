using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.UseCases.Commands.CreateProduct;
using ProductService.Domain.UseCases.Queries.Find;
using ProductService.Library.Models;
using ProductService.Tests.Domain.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.UseCases.Queries
{
    public class FindProductByIdQueryTest : BaseProductServiceTest
    {
        [Test]
        public async Task TestQuery()
        {
            var mediator = Services.GetService<IMediator>();
            var productId = await mediator.Send(new CreateProductCommand(
                new ProductDto()
                {
                    Number = 1,
                    Name = nameof(CreateProductCommandTest)
                }
            ));

            var productView = await mediator.Send(
                new FindProductByIdQuery(productId));

            Assert.That(productView, Is.Not.Null);
            Assert.That(productId == productView.Id);
        }
    }
}
