using System.Threading;
using System.Threading.Tasks;
using Refit;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;

namespace Riders.Tweakbox.API.SDK.Interfaces
{
    public interface IIdentityApi
    {
        /// <summary>
        /// Retrieves a list of users from the system.
        /// Requires to be signed in!
        /// </summary>
        /// <param name="query">How many users to fetch at once.</param>
        /// <response code="200">Success</response>
        [Get("/" + Routes.Identity.Get)]
        public Task<ApiResponse<PagedResponse<UserDetailsResult>>> GetAll([Query] PaginationQuery query);

        /// <summary>
        /// Logs a user into the system and returns a token to be used by subsequent API calls.
        /// </summary>
        /// <param name="request">User details.</param>
        /// <response code="400">User details are incorrect.</response>
        /// <response code="200">Success</response>
        /// <returns><seealso cref="AuthSuccessResponse"/> if successful, else <seealso cref="Application.Commands.v1.Error.ErrorResponse"/></returns>
        [Post("/" + Routes.Identity.Login)]
        public Task<ApiResponse<AuthSuccessResponse>> Login([Body] UserLoginRequest request);

        /// <summary>
        /// Registers a new user and returns a token to be used by subsequent API calls.
        /// </summary>
        /// <param name="request">Registration details.</param>
        /// <response code="400">User details are incorrect.</response>
        /// <response code="200">Success</response>
        /// <returns><seealso cref="AuthSuccessResponse"/> if successful, else <seealso cref="Riders.Tweakbox.API.Application.Commands.v1.Error.ErrorResponse"/></returns>
        [Post("/" + Routes.Identity.Register)]
        public Task<ApiResponse<AuthSuccessResponse>> Register([Body] UserRegistrationRequest request);

        /// <summary>
        /// Refreshes a token returned by the API.
        /// </summary>
        /// <param name="request">Current token and the refresh token.</param>
        /// <response code="400">Token details are incorrect.</response>
        /// <response code="200">Success</response>
        /// <returns><seealso cref="AuthSuccessResponse"/> if successful, else <seealso cref="Riders.Tweakbox.API.Application.Commands.v1.Error.ErrorResponse"/></returns>
        [Post("/" + Routes.Identity.Refresh)]
        public Task<ApiResponse<AuthSuccessResponse>> Refresh([Body] RefreshTokenRequest request);
    }
}