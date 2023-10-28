using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using MiniECommerce.Library.Services;
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
        private readonly AuthorizationHeaderService _authorizationHeaderService;
        private readonly ILogger<OutgoingRequestHandler> _logger;

        public OutgoingRequestHandler(
            IRequestIdService requestIdService,
            AuthorizationHeaderService authorizationHeaderService,
            ILogger<OutgoingRequestHandler> logger)
        {
            _requestIdService = requestIdService;
            _authorizationHeaderService = authorizationHeaderService;
            _logger = logger;
        }

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Outgoing request to: {0}", request.RequestUri);
            request.Headers.Add("RequestId",
                _requestIdService.GetRequestIdStr());

            var authHeader = await _authorizationHeaderService
                .GetAuthorizationHeaderValue();
            if (authHeader is not null)
                request.Headers.Add("Authorization", authHeader);

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
