using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using OneOf;
using Polly;
using Polly.Extensions.Http;
using Refit;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.SDK.Common;
using Riders.Tweakbox.API.SDK.Handler;
using Riders.Tweakbox.API.SDK.Helpers;
using Riders.Tweakbox.API.SDK.Interfaces;

namespace Riders.Tweakbox.API.SDK
{
    /// <summary>
    /// Encapsulates all accesses to the Tweakbox API.
    /// </summary>
    public class TweakboxApi : IDisposable
    {
        public IIdentityApi IdentityApi  { get; private set; }
        public IMatchApi Match           { get; private set; }
        public IServerBrowserApi Browser { get; private set; }
        public TweakboxAuthenticationHandler AuthHandler { get; private set; }
        public PolicyHttpMessageHandler PolicyHandler { get; private set; }
        public HttpClient Client { get; private set; }

        public TweakboxApi(string url, DateTimeProvider provider = null)
        {
            SetupHttpClientHandlers(provider);
            Client = new HttpClient(PolicyHandler);
            Client.BaseAddress = new Uri(url);
            SetupServices();
        }

        /// <summary>
        /// Intended for testing but feel free to use.
        /// </summary>
        public TweakboxApi(Func<DelegatingHandler[], HttpClient> getClient, DateTimeProvider provider = null)
        {
            SetupHttpClientHandlers(provider);
            Client = getClient(new DelegatingHandler[] { PolicyHandler, AuthHandler });
            SetupServices();
        }

        private void SetupHttpClientHandlers(DateTimeProvider provider)
        {
            var policy = HttpPolicyExtensions.HandleTransientHttpError().WaitAndRetryAsync(new TimeSpan[]
            {
                TimeSpan.FromSeconds(1),
                TimeSpan.FromSeconds(2),
                TimeSpan.FromSeconds(3)
            });

            PolicyHandler = new PolicyHttpMessageHandler(policy);
            AuthHandler   = new TweakboxAuthenticationHandler(this, provider ?? new DateTimeProvider());

            PolicyHandler.InnerHandler = AuthHandler;
            AuthHandler.InnerHandler   = new HttpClientHandler();
        }

        private void SetupServices()
        {
            IdentityApi = MakeRestService<IIdentityApi>(Client);
            Match    = MakeRestService<IMatchApi>(Client);
            Browser  = MakeRestService<IServerBrowserApi>(Client);
        }

        /// <summary>
        /// Allows you to authenticate with the API before making any API calls which require authentication.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Either a successful login or authentication failure details.</returns>
        public async Task<OneOf<AuthSuccessResponse, ErrorReponse>> TryAuthenticate(string username, string password)
        {
            return await AuthHandler.TryAuthenticate(username, password);
        }

        private TService MakeRestService<TService>(HttpClient client)
        {
            return RestService.For<TService>(client, new RefitSettings(RefitConstants.ContentSerializer));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Client?.Dispose();
            AuthHandler?.Dispose();
        }
    }
}
