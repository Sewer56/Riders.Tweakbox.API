﻿using System.Threading;
using System.Threading.Tasks;
using Refit;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
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
        [Get("/" + Routes.Identity.GetAll)]
        public Task<ApiResponse<PagedResponse<UserDetailsResult>>> GetAll([Query] PaginationQuery query);

        /// <summary>
        /// Retrieves an individual user from the system.
        /// Requires to be signed in!
        /// </summary>
        /// <param name="id">The unique identifier of the user.</param>
        /// <response code="200">Success</response>
        [Get("/" + Routes.Identity.Get)]
        public Task<ApiResponse<PagedResponse<UserDetailsResult>>> Get([Query] int id);

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
        /// Allows you to request a password reset by sending a token to a specified email address.
        /// You can then use that token with <see cref="ResetPassword"/> (ResetPassword).
        /// </summary>
        /// <param name="request">Password reset details.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="404">Email does not exist.</response>
        /// <response code="200">Success</response>
        [Post("/" + Routes.Identity.GetPasswordResetToken)]
        public Task<ApiResponse<ErrorReponse>> RequestResetToken([Body] GetPasswordResetTokenRequest request);

        /// <summary>
        /// Allows you to request a password reset using a token mailed to a user's email address.
        /// </summary>
        /// <param name="request">Password reset details.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">Bad request/internal error.</response>
        /// <response code="200">Success</response>
        [Post("/" + Routes.Identity.PasswordReset)]
        public Task<ApiResponse<AuthSuccessResponse>> ResetPassword([Body] GetPasswordResetTokenRequest request);

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