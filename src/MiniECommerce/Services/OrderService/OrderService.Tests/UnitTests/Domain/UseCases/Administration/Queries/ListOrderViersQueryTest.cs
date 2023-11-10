using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OrderService.Domain.Repositories;
using OrderService.Domain.Services;
using OrderService.Domain.UseCases.Administration.Queries.ListOrders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Tests.UnitTests.Domain.UseCases.Administration.Queries
{
    public class ListOrderViersQueryTest : BaseOrderServiceTest
    {
        [Test]
        public async Task QueryShouldNotReturnNull()
        {
            var mediator = Services.GetRequiredService<IMediator>();

            var res = await mediator.Send(new AdmListOrderViewsQuery(

            ));

            Assert.IsNotNull(res);
        }

        [Test]
        public async Task QueryShouldContainRecords()
        {
            var mediator = Services.GetRequiredService<IMediator>();
            var orderService = Services.GetRequiredService<IOrderService>();
            var orderViewRepository = Services.GetRequiredService<IOrderViewRepository>();
            var initOrderService = Services.GetRequiredService<IInitializeOrderService>();

            var order1ToSave = await initOrderService.Initialize(
                nameof(QueryShouldContainRecords), nameof(QueryShouldContainRecords));

            var order2ToSave = await initOrderService.Initialize(
                nameof(QueryShouldContainRecords), nameof(QueryShouldContainRecords));

            var order1Id = await orderService.Save(order1ToSave);
            var order2Id = await orderService.Save(order2ToSave);

            var res = await mediator.Send(new AdmListOrderViewsQuery(

            ));

            Assert.IsNotNull(res);
            Assert.IsNotEmpty(res);

            var orderViewIds = res.Select(x => x.Id).ToList();

            Assert.Contains(order1Id, orderViewIds);
            Assert.Contains(order2Id, orderViewIds);
        }
    }
}
