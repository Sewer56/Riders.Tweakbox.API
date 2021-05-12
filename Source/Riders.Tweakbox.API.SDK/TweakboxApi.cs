using System;
using System.Net.Http;
using System.Threading.Tasks;
using OneOf;
using Refit;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.SDK.Common;
using Riders.Tweakbox.API.SDK.Handler;
using Riders.Tweakbox.API.SDK.Interfaces;

namespace Riders.Tweakbox.API.SDK
{
    /// <summary>
    /// Encapsulates all accesses to the Tweakbox API.
    /// </summary>
    public class TweakboxApi : IDisposable
    {
        public IIdentity Identity        { get; private set; }
        public IMatchApi Match           { get; private set; }
        public IServerBrowserApi Browser { get; private set; }
        public TweakboxAuthenticationHandler Handler { get; private set; }
        public HttpClient Client { get; private set; }

        public TweakboxApi(string url)
        {
            Handler  = new TweakboxAuthenticationHandler(this);
            Handler.InnerHandler = new HttpClientHandler();

            Client   = new HttpClient(Handler);
            Client.BaseAddress = new Uri(url);
            SetupServices();
        }

        /// <summary>
        /// Intended for testing but feel free to use.
        /// </summary>
        public TweakboxApi(Func<DelegatingHandler, HttpClient> getClient)
        {
            Handler = new TweakboxAuthenticationHandler(this);
            Client  = getClient(Handler);
            SetupServices();
        }

        private void SetupServices()
        {
            Identity = MakeRestService<IIdentity>(Client);
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
            return await Handler.TryAuthenticate(username, password);
        }

        private TService MakeRestService<TService>(HttpClient client)
        {
            return RestService.For<TService>(client, new RefitSettings(RefitConstants.ContentSerializer));
        }

        /// <inheritdoc />
        public void Dispose()
        {
            Client?.Dispose();
            Handler?.Dispose();
        }
    }
}
