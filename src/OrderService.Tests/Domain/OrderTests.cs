﻿using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using OrderService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderService.Domain;
using OrderService.Domain.Services;

namespace OrderService.Tests.Domain
{
    public class OrderTests
    {
        [Test]
        public void TestLoadOrder()
        {
            var services = new ServiceCollection();
            services.AddOrderServiceDomain();
            var serviceProvider = services.BuildServiceProvider();
            var orderService = serviceProvider.GetService<IOrderService>();

            Assert.ThrowsAsync<ValidationException>(async() =>
            {
                await orderService.LoadAsync(
                    id: Guid.Empty,
                    number: -1,
                    orderLines: null
                );
            });
        }
    }
}
