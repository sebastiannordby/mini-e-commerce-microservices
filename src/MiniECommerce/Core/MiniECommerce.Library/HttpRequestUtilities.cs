using Microsoft.AspNetCore.Http;

namespace MiniECommece.APIUtilities
{
    public static class HttpRequestUtilities 
    {
        public static Guid GetRequestId(this HttpRequest request)
        {
            var requestIdStr = request.Headers["RequestId"];
            if(string.IsNullOrWhiteSpace(requestIdStr))
                throw new ArgumentException("RequestId is required to send a request.");

            if (!Guid.TryParse(requestIdStr, out var requestId))
                throw new ArgumentException("RequestId must be a Guid: https://learn.microsoft.com/en-us/dotnet/api/system.guid?view=net-7.0");

            return requestId;
        }
    }
}