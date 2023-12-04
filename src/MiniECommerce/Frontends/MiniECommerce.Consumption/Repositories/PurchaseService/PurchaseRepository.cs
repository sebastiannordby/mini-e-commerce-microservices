using Microsoft.AspNetCore.Http;
using PurchaseService.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories.PurchaseService
{
    internal class PurchaseRepository : HttpRepository, IPurchaseRepository
    {
        public PurchaseRepository(
            HttpClient httpClient, 
            IHttpContextAccessor accessor) : base(httpClient, accessor)
        {
        }

        public async Task<string> Pay(PaymentCommandDto command)
        {
            var request = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("http://gateway/api/purchase-service/purchase/pay"),
                Content = new StringContent(
                    JsonSerializer.Serialize(command), Encoding.UTF8, "application/json")
            };

            var httpResponse = await Send(request);
            var response = await httpResponse
                .Content.ReadAsStringAsync();

            return response;
        }
    }
}
