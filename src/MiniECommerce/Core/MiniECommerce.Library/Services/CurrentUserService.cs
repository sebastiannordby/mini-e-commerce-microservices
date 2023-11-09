using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Authentication.Services
{
    internal class CurrentUserService : ICurrentUserService
    {
        public string UserEmail { get; init; }
        public string UserFullName { get; init; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            UserEmail = user?.FindFirstValue(ClaimTypes.Email) ?? "NOT_SIGNED_IN";
            UserFullName = user?.FindFirst("name")?.Value ?? "NOT_SIGNED_IN";
        }
    }
}
