using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace StackExchange.Windows.Api
{
    /// <summary>
    /// Defines a HttpClientHandler that is able to authenticate requests to the API.
    /// </summary>
    public class AuthenticatedHttpClientHandler : HttpClientHandler
    {
        public const string AccessTokenParameter = "access_token";
        public const string KeyParameter = "key";

        private readonly string key;
        private readonly Func<string> accessToken;

        public AuthenticatedHttpClientHandler(string key, Func<string> tokenGetter)
        {
            if (key == null) throw new ArgumentNullException(nameof(key));
            if (tokenGetter == null) throw new ArgumentNullException(nameof(tokenGetter));
            accessToken = tokenGetter;
            this.key = key;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = accessToken();
            if (token != null)
            {
                var query = request.RequestUri.ToDictionary();

                if (!query.ContainsKey(AccessTokenParameter))
                {
                    query.Add(AccessTokenParameter, token);
                    if (!query.ContainsKey(key))
                    {
                        query.Add(KeyParameter, key);
                    }

                    request.RequestUri = query.ToUri(request.RequestUri);
                }
            }
            return base.SendAsync(request, cancellationToken);
        }
    }
}
