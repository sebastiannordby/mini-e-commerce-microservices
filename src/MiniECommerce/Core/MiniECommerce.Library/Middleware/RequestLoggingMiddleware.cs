using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Authentication.Middleware
{
    internal class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLoggingMiddleware(RequestDelegate next)
        {
            this._next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var header = context.Request.Headers["RequestId"];

            if (header.Count > 0)
            {
                var logger = context.RequestServices
                    .GetRequiredService<ILogger<RequestLoggingMiddleware>>();

                var email = context.User?.FindFirst(ClaimTypes.Email);

                using (logger.BeginScope("{@RequestId}", header[0]))
                {
                    logger.LogInformation("Request started. User: {0}", email);
                    await this._next(context);
                    logger.LogInformation("Request finished. User: {0}", email);
                }
            }
            else
            {
                await this._next(context);
            }
        }
    }
}
