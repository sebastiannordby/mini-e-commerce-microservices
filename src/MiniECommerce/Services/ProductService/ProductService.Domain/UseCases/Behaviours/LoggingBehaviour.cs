using MediatR;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Domain.UseCases.Behaviours
{
    public sealed class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var requestId = Guid.Empty;
            if (request.GetType().IsAssignableTo(typeof(Request)))
                requestId = (request as Request).RequestId;

            Log.Information("Request {0} received.", requestId);
            var response = await next();
            Log.Information("Request {0} completed.", requestId);

            return response;
        }
    }
}
