using Microsoft.AspNetCore.Http;
using ProductService.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using UserService.Library;

namespace MiniECommerce.Consumption.Repositories.UserService
{
    internal class UserRepository : HttpRepository, IUserRepository
    {
        public UserRepository(
            HttpClient httpClient, 
            IHttpContextAccessor accessor) : base(httpClient, accessor)
        {

        }

        public async Task<UserInfoView?> Get()
        {
            var req = new HttpRequestMessage()
            {
                RequestUri = new Uri(
                    $"http://gateway/api/user-service/user")
            };

            return await Send<UserInfoView?>(req);
        }

        public async Task Save(UserInfoView infoView)
        {
            var req = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri(
                    $"http://gateway/api/user-service/user"),
                Content = new StringContent(
                    JsonSerializer.Serialize(infoView), Encoding.UTF8, "application/json")
            };

            await Send(req);
        }
    }
}
