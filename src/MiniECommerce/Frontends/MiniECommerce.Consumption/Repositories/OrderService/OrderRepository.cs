using FluentResults;
using Microsoft.AspNetCore.Http;
using MiniECommerce.Consumption.Responses;
using OrderService.Library.Commands;
using OrderService.Library.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.OrderService
{
    internal class OrderRepository : HttpRepository, IOrderRepository
    {
        public OrderRepository(
            HttpClient httpClient,
            IHttpContextAccessor httpContextAccessor
        ) : base(httpClient, httpContextAccessor)
        {
        }

        public async Task<OrderView?> Get(Guid orderId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"http://gateway/api/order-service/order/{orderId}")
            };

            var res = await Send<QueryResponse<OrderView?>>(req);

            return res?.Data;
        }

        public async Task<Guid?> GetStartedOrder()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://gateway/api/order-service/order/started-order")
            };

            var res = await Send<QueryResponse<Guid?>>(req);

            return res?.Data;
        }

        public async Task<IEnumerable<OrderView>> List()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://gateway/api/order-service/order")
            };

            var res = await Send<QueryResponse<IEnumerable<OrderView>>>(req);

            return res?.Data ?? Enumerable.Empty<OrderView>();
        }

        public async Task<Result> SetAddress(SetOrderAddressCommandDto command)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://gateway/api/order-service/order/set-address"),
                Content = new StringContent(
                    JsonSerializer.Serialize(command), Encoding.UTF8, "application/json")
            };

            var res = await Send<CommandResponse<Result>>(req);

            return res?.Data ?? Result.Fail("Error in HttpRequest");
        }

        public async Task<Guid?> Start()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://gateway/api/order-service/order/start")
            };

            var res = await Send<CommandResponse<Guid>>(req);

            return res?.Data;
        }
    }
}
