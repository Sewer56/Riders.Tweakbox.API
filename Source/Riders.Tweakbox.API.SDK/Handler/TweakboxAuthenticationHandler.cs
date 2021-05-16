using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using OneOf;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.SDK.Common;

namespace Riders.Tweakbox.API.SDK.Handler
{
    public class TweakboxAuthenticationHandler : DelegatingHandler
    {
        public TweakboxApi Owner { get; private set; }
        
        /// <inheritdoc />
        public TweakboxAuthenticationHandler(TweakboxApi owner)
        {
            Owner = owner;
        }

        public AuthSuccessResponse CachedAuthResponse     { get; internal set; }
        public JwtSecurityToken    Token            { get; internal set; }
        public UserLoginRequest    LoginRequest     { get; internal set; }

        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await GetToken();
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Allows you to authenticate with the API before making any API calls which require authentication.
        /// </summary>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        /// <returns>Either a successful login or authentication failure details.</returns>
        public async Task<OneOf<AuthSuccessResponse, ErrorReponse>> TryAuthenticate(string username, string password)
        {
            LoginRequest = new UserLoginRequest()
            {
                Username = username,
                Password = password
            };

            var result = await Owner.IdentityApi.Login(LoginRequest);

            if (result.StatusCode == HttpStatusCode.BadRequest)
                return JsonSerializer.Deserialize<ErrorReponse>(result.Error.Content, RefitConstants.SerializerOptions);

            CachedAuthResponse = result.Content;
            Token = new JwtSecurityToken(CachedAuthResponse.Token);
            return result.Content;
        }

        private async Task<string> GetToken()
        {
            if (CachedAuthResponse == null)
                return "";

            if (DateTime.UtcNow <= Token.ValidTo)
                return CachedAuthResponse.Token;

            // Refresh token.
            var result = await Owner.IdentityApi.Refresh(new RefreshTokenRequest()
            {
                Token = CachedAuthResponse.Token,
                RefreshToken = CachedAuthResponse.RefreshToken,
            });

            // Successful Refresh
            if (result.StatusCode != HttpStatusCode.BadRequest)
            {
                CachedAuthResponse = result.Content;
                return CachedAuthResponse.Token;
            }

            // Try login if refresh failed.
            var errors = result.Error.Content;
            var login  = await Owner.TryAuthenticate(LoginRequest.Username, LoginRequest.Password);

            if (login.IsT1)
            {
                throw new AuthenticationException("Refreshing Json Web Token failed.\n" +
                                                  $"Error: {errors}\n" +
                                                  $"Tried to log-in again but failed.\n" +
                                                  $"Error: {string.Join("\n", login.AsT1.Errors)}");
            }

            return login.AsT0.Token;
        }
    }
}
