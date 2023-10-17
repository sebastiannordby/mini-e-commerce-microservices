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
        public string? UserEmail { get; init; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;

            UserEmail = user?.FindFirstValue(ClaimTypes.Email);
        }
    }
}
