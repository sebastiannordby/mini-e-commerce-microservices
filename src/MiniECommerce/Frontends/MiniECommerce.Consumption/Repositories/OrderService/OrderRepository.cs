using Microsoft.AspNetCore.Http;
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

            return await Send<OrderView?>(req);
        }

        public async Task<Guid?> GetStartedOrder()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://gateway/api/order-service/order/started-order")
            };

            var res = await Send(req);
            var content = await res.Content.ReadAsStringAsync();

            if (string.IsNullOrWhiteSpace(content))
                return null;

            if (!Guid.TryParse(content, out var orderId))
                return null;

            return orderId;
        }

        public async Task<Guid> Start(StartOrderCommandDto command)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://gateway/api/order-service/order/start"),
                Content = new StringContent(
                    JsonSerializer.Serialize(command), Encoding.UTF8, "application/json")
            };

            return await Send<Guid>(req);
        }
    }
}
