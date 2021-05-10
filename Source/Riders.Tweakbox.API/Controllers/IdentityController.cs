using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Application.Services;

namespace Riders.Tweakbox.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class IdentityController : ControllerBase
    {
        private IIdentityService _identityService;

        public IdentityController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        /// <summary>
        /// Logs a user into the system and returns a token to be used by subsequent API calls.
        /// </summary>
        /// <param name="request">User details.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">User details are incorrect.</response>
        /// <response code="200">Success</response>
        [HttpPost(Routes.Identity.Login)]
        public async Task<IActionResult> Login(UserLoginRequest request, CancellationToken cancellationToken)
        {
            return HandleAuthResponse(await _identityService.LoginAsync(request.Username, request.Password, cancellationToken));
        }

        /// <summary>
        /// Registers a new user and returns a token to be used by subsequent API calls.
        /// </summary>
        /// <param name="request">Registration details.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">User details are incorrect.</response>
        /// <response code="200">Success</response>
        [HttpPost(Routes.Identity.Register)]
        public async Task<IActionResult> Register(UserRegistrationRequest request, CancellationToken cancellationToken)
        {
            return HandleAuthResponse(await _identityService.RegisterAsync(request.UserName, request.Password, cancellationToken));
        }

        /// <summary>
        /// Refreshes a token returned by the API.
        /// </summary>
        /// <param name="request">Current token and the refresh token.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="400">Token details are incorrect.</response>
        /// <response code="200">Success</response>
        [HttpPost(Routes.Identity.Refresh)]
        public async Task<IActionResult> Refresh(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            return HandleAuthResponse(await _identityService.RefreshTokenAsync(request.Token, request.RefreshToken, cancellationToken));
        }

        private IActionResult HandleAuthResponse(AuthenticationResult authResponse)
        {
            if (!authResponse.Success)
                return BadRequest(new ErrorReponse() {Errors = authResponse.Errors});

            return Ok(new AuthSuccessResponse()
            {
                Token = authResponse.Token,
                RefreshToken = authResponse.RefreshToken
            });
        }
    }
}
