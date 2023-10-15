using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Polly;
using ProductService.Library.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MiniECommerce.Consumption.Repositories
{
    internal class HttpRepository
    {
        protected readonly HttpClient _httpClient;
        protected readonly IHttpContextAccessor _accessor;

        public HttpRepository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public HttpRepository(
            HttpClient httpClient,
            IHttpContextAccessor accessor)
        {
            _httpClient = httpClient;
            _accessor = accessor;
        }

        public async Task<T?> Send<T>(HttpRequestMessage req)
        {
            var httpResponse = await Send(req);
            var jsonResponse = await httpResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });
        }

        public async Task<HttpResponseMessage> Send(HttpRequestMessage req)
        {
            var requestId = Guid.NewGuid().ToString();
            var policyPipeline = new ResiliencePipelineBuilder()
                .AddRetry(new()
                {
                    MaxRetryAttempts = 3,
                    Delay = TimeSpan.FromSeconds(3),
                    OnRetry = (retry) =>
                    {
                        Log.Information("Request({0}) Retry: {1} Uri: {2}",
                            requestId, retry.AttemptNumber, req.RequestUri);

                        return ValueTask.CompletedTask;
                    },
                    ShouldHandle = new PredicateBuilder()
                        .Handle<HttpRequestException>()
                }).Build();

            Log.Information(
                "Request({0}): {1}", requestId, req.RequestUri);

            if (_accessor?.HttpContext != null)
            {
                var accessToken = _accessor.HttpContext.Session.GetString("access_token");
                if(accessToken != null)
                {
                    req.Headers.Add("Authorization", "Bearer " + accessToken);
                }

            }

            req.Headers.Accept.Add(new("application/json"));
            req.Headers.Add("RequestId", requestId);

            var httpResponse = await policyPipeline
                .ExecuteAsync<HttpResponseMessage>(async cancellationToken =>
                {
                    return await _httpClient.SendAsync(req);
                });

            Log.Information("Request({0}): {1} - {2} ",
                requestId, req.RequestUri, httpResponse.StatusCode);

            return httpResponse;
        }
    }
}
