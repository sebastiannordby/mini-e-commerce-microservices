﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ProductService.DataAccess.Repositories;
using ProductService.Domain.Repositories;
using ProductService.Domain.UseCases.Commands.CreateProduct;
using ProductService.Library.Models;
using ProductService.Tests.Domain.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain.Repositories
{
    public class ProductViewRepositoryTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestListIsNotNullButEmpty()
        {
            var productViewRepository = Services
                .GetRequiredService<IProductViewRepository>();

            var productViews = await productViewRepository.List();

            Assert.That(productViews, Is.Not.Null);
            Assert.That(productViews, Is.Empty);
        }

        [Test]
        public async Task TestListContainsProduct()
        {
            var productViewRepository = Services
                .GetRequiredService<IProductViewRepository>();
            var mediator = Services.GetRequiredService<IMediator>();

            var createdProductId = await mediator.Send(new CreateProductCommand(
                new ProductDto()
                {
                    Number = 1,
                    Name = nameof(CreateProductCommandTest)
                }
            ));

            var productViews = await productViewRepository.List();

            Assert.That(productViews, Is.Not.Empty);
            Assert.That(productViews.Select(x => x.Id).Contains(createdProductId));
        }

        [Test]
        public async Task TestFindProduct()
        {
            var productViewRepository = Services
                .GetRequiredService<IProductViewRepository>();
            var mediator = Services.GetRequiredService<IMediator>();

            var createdProductId = await mediator.Send(new CreateProductCommand(
                new ProductDto()
                {
                    Number = 1,
                    Name = nameof(CreateProductCommandTest)
                }
            ));

            var productView = await productViewRepository.Find(createdProductId);

            Assert.That(productView, Is.Not.Null);
            Assert.That(productView.Id, Is.EqualTo(createdProductId));
        }
    }
}
