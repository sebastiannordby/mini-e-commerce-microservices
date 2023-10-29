using Microsoft.AspNetCore.Http;
using MiniECommerce.Consumption.Responses;
using OrderService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
