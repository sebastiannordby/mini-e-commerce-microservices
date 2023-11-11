using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductService.DataAccess.Repositories;
using ProductService.Domain.Repositories;
using ProductService.Domain.UseCases.Commands.CreateProduct;
using ProductService.Domain.UseCases.Commands.UpdateProduct;
using ProductService.Domain.UseCases.Queries.Find;
using ProductService.Domain.UseCases.Queries.List;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.UseCases.Commands
{
    public class UpdateProductCommandTest : BaseProductServiceTest
    {
        [Test]
        public async Task TestCommand()
        {
            var mediator = Services.GetService<IMediator>();
            var productRepository = Services.GetService<IProductRepository>();

            var productId = await mediator.Send(new CreateProductCommand(
                new ProductDto()
                {
                    Number = 1,
                    Name = nameof(CreateProductCommandTest)
                }
            ));

            var productDto = await mediator.Send(
                new FindProductByIdQuery(productId));

            productDto.Name = "Test 2";

            await mediator.Send(
                new UpdateProductCommand(productDto));

            var productDtoUpdated = await mediator.Send(
                new FindProductByIdQuery(productId));

            Assert.That(productDtoUpdated.Name == productDto.Name);
            Assert.That(productDtoUpdated.Id == productDto.Id);
            Assert.That(productDtoUpdated.Id == productId);
        }
    }
}
