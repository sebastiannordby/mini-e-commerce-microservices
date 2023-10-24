using Microsoft.AspNetCore.Http;
using OrderService.Library.Commands;
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
