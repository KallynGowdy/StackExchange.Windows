using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Defines a <see cref="HttpClientHandler"/> that caches requests and responses
    /// based on simplified information.
    /// </summary>
    public class CachedHttpClientHandler : HttpClientHandler
    {
        private readonly ConcurrentDictionary<Tuple<HttpMethod, Uri>, CachedResponse> cachedInfo = new ConcurrentDictionary<Tuple<HttpMethod, Uri>, CachedResponse>();

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var key = BuildCacheKey(request);
            var response = GetCachedResponse(key);

            if (response == null)
            {
                response = await base.SendAsync(request, cancellationToken);
                if (!cachedInfo.ContainsKey(key))
                {
                    cachedInfo.TryAdd(key, await AsCachedAsync(response));
                }
                else
                {
                    response = GetCachedResponse(key);
                }
            }
            return response;
        }

        private HttpResponseMessage GetCachedResponse(Tuple<HttpMethod, Uri> key)
        {
            if (cachedInfo.TryGetValue(key, out var response))
            {
                return response.ToResponse();
            }
            return null;
        }

        private static Tuple<HttpMethod, Uri> BuildCacheKey(HttpRequestMessage request)
        {
            return new Tuple<HttpMethod, Uri>(request.Method, request.RequestUri);
        }

        private static async Task<CachedResponse> AsCachedAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return new CachedResponse(response.StatusCode, content);
        }

        private class CachedResponse
        {
            private readonly string response;
            private readonly HttpStatusCode statusCode;

            public CachedResponse(HttpStatusCode statusCode, string content)
            {
                this.response = content;
                this.statusCode = statusCode;
            }

            public HttpResponseMessage ToResponse()
            {
                return new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(response)
                };
            }
        }
    }
}
