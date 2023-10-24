using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Gateway.Consumption
{
    internal class AuthorizationHeaderService
    {
        public IHttpContextAccessor _httpContextAccessor;

        public AuthorizationHeaderService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<string> GetAuthorizationHeaderValue()
        {
            var authHeader = _httpContextAccessor
                ?.HttpContext
                ?.Request
                ?.Headers?["Authorization"];

            return await Task.FromResult(authHeader ?? "");
        }
    }
}
