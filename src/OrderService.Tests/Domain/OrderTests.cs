using FluentValidation;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.Domain
{
    public class OrderTests
    {
        [Test]
        public void TestLoadOrder()
        {
            Assert.Throws<ValidationException>(() =>
            {
                var order = Order.Load(
                    id: Guid.Empty,
                    number: -1,
                    orderLines: null);
            });
        }
    }
}
