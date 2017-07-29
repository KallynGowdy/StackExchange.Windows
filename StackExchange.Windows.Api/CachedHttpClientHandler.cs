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
        /// <summary>
        /// The amount of time between cache purges.
        /// That is, when the cache is cleared in order to limit it's size.
        /// An alternative method would be to provide a maximum size and remove
        /// the oldest values first.
        /// TODO: Move to using Akavache for caching. (InMemory store)
        ///       Probably less prone to error and does a better job at managing expirations.
        /// </summary>
        private readonly TimeSpan cachePurge = TimeSpan.FromMinutes(10);
        private readonly TimeSpan expiration = TimeSpan.FromMinutes(1);
        private readonly ConcurrentDictionary<Tuple<HttpMethod, Uri>, CachedResponse> cachedInfo = new ConcurrentDictionary<Tuple<HttpMethod, Uri>, CachedResponse>();
        private DateTimeOffset lastPurgeTime = DateTimeOffset.Now;

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (ShouldPurgeCache())
            {
                lock (cachedInfo)
                {
                    if (ShouldPurgeCache())
                    {
                        PurgeCache();
                    }
                }
            }
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

        private void PurgeCache()
        {
            lastPurgeTime = DateTimeOffset.Now;
            cachedInfo.Clear();
        }

        private bool ShouldPurgeCache()
        {
            return lastPurgeTime.Add(cachePurge) < DateTimeOffset.Now;
        }

        private HttpResponseMessage GetCachedResponse(Tuple<HttpMethod, Uri> key)
        {
            if (cachedInfo.TryGetValue(key, out var response))
            {
                if (response.HasExpired())
                {
                    cachedInfo.TryRemove(key, out response);
                    return null;
                }
                return response.ToResponse();
            }
            return null;
        }

        private static Tuple<HttpMethod, Uri> BuildCacheKey(HttpRequestMessage request)
        {
            return new Tuple<HttpMethod, Uri>(request.Method, request.RequestUri);
        }

        private async Task<CachedResponse> AsCachedAsync(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            return new CachedResponse(response.StatusCode, content, DateTimeOffset.Now.Add(expiration));
        }

        private class CachedResponse
        {
            private readonly string response;
            private readonly HttpStatusCode statusCode;
            private readonly DateTimeOffset? expirationDate;

            public CachedResponse(HttpStatusCode statusCode, string content, DateTimeOffset? expirationDate)
            {
                this.response = content;
                this.statusCode = statusCode;
                this.expirationDate = expirationDate;
            }

            public HttpResponseMessage ToResponse()
            {
                return new HttpResponseMessage(statusCode)
                {
                    Content = new StringContent(response)
                };
            }

            public bool HasExpired()
            {
                return !expirationDate.HasValue || DateTimeOffset.Now > expirationDate.Value;
            }
        }
    }
}
