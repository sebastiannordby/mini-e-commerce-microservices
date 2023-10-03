using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using ProductService.Domain.Services;
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
        public void TestLoadProductThrowsValidationException()
        {
            var loadProductService = Services
                .GetService<ILoadProductService>();

            Assert.ThrowsAsync<ValidationException>(async() =>
            {
                await loadProductService.LoadAsync(
                    id: Guid.Empty,
                    number: null,
                    name: null,
                    description: null,
                    category: null);
            });
        }
    }
}
