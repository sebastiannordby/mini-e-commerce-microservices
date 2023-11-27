using Microsoft.AspNetCore.Http;
using MiniECommerce.Consumption.Responses;
using OrderService.Library.Commands;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.OrderService.Administration
{
    internal class OrderAdminRepository : HttpRepository, IOrderAdminRepository
    {
        public OrderAdminRepository(
            HttpClient httpClient, 
            IHttpContextAccessor accessor) : base(httpClient, accessor)
        {

        }

        public async Task<bool> Confirm(Guid orderId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://gateway/api/order-service/admin/order/confirm/{orderId}"),
            };

            var res = await Send<CommandResponse<bool>>(req);

            return res?.Data ?? false;
        }

        public async Task<bool> SetWaitingForConfirmation(Guid orderId)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"http://gateway/api/order-service/admin/order/waiting-for-confirmation/{orderId}"),
            };

            var res = await Send<CommandResponse<bool>>(req);

            return res?.Data ?? false;
        }

        public async Task<IEnumerable<OrderView>> List()
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("http://gateway/api/order-service/admin/order")
            };

            var res = await Send<QueryResponse<IEnumerable<OrderView>>>(req);

            return res?.Data ?? new List<OrderView>();
        }

        public async Task<bool> SetDeliveryAddress(SetOrderDeliveryAddressCommandDto command)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://gateway/api/order-service/admin/order/set-address"),
                Content = new StringContent(
                    JsonSerializer.Serialize(command), Encoding.UTF8, "application/json")
            };

            var res = await Send<CommandResponse<bool>>(req);

            return res?.Data ?? false;
        }
    }
}
