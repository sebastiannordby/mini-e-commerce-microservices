using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniECommerce.Authentication.Services
{
    internal class OutgoingRequestHandler : DelegatingHandler
    {
        private readonly IRequestIdService _requestIdService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OutgoingRequestHandler(
            IRequestIdService requestIdService, 
            IHttpContextAccessor httpContextAccessor)
        {
            _requestIdService = requestIdService;
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Add("RequestId", 
                _requestIdService.GetRequestIdStr()); 

            return base.SendAsync(request, cancellationToken);
        }
    }
}
