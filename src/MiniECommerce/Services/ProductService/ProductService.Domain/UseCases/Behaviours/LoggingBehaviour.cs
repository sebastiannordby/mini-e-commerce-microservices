using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Behaviours
{
    public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<TRequest> _logger;

        public LoggingBehaviour(ILogger<TRequest> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestId = Guid.Empty;
            if (request.GetType().IsAssignableTo(typeof(Request)))
                requestId = (request as Request).RequestId;

            _logger.LogInformation("Request {0} receieved.", requestId);
            var response = await next();
            _logger.LogInformation("Request {0} completed.", requestId);

            return response;
        }
    }
}
