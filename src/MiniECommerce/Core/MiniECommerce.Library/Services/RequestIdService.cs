using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Authentication.Services
{
    public interface IRequestIdService
    {
        Guid? GetRequestId();
        string? GetRequestIdStr();
    }

    internal class RequestIdService : IRequestIdService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestIdService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public Guid? GetRequestId()
        {
            var requestIdStr = _httpContextAccessor
                ?.HttpContext
                ?.Request
                ?.Headers?["RequestId"];
            if (requestIdStr is null)
                return null;

            if (!Guid.TryParse(requestIdStr, out var requestId))
                return null;

            return requestId;
        }

        public string? GetRequestIdStr()
        {
            var requestIdStr = _httpContextAccessor
                ?.HttpContext
                ?.Request
                ?.Headers?["RequestId"];

            return requestIdStr;
        }
    }
}
