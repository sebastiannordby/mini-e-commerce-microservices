using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Tests.Domain
{
    public class ProductTests : BaseProductServiceTest
    {
        [Test]
        public async Task TestLoadProductsReturnsValues()
        {
            var productViewRepository = Services
                .GetService<IProductViewRepository>();

            var productViews = await productViewRepository.List();

            Assert.IsEmpty(productViews);
            Assert.IsNotNull(productViews);
        }
    }
}
