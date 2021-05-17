using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models;

namespace Riders.Tweakbox.API.Controllers
{
    [ApiController]
    [ApiExplorerSettings(GroupName = "v1")]
    public class IdentityController : ControllerBase
    {
        private IIdentityService _identityService;
        private IGeoIpService _geoIpService;
        private ICurrentUserService _currentUserService;

        public IdentityController(IIdentityService identityService, IGeoIpService geoIpService, ICurrentUserService currentUserService)
        {
            _identityService = identityService;
            _geoIpService = geoIpService;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Retrieves a list of users from the system.
        /// </summary>
        /// <param name="query">How many users to fetch at once.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Success</response>
        [HttpGet(Routes.Identity.GetAll)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.User)]
        public async Task<IActionResult> GetAll([FromQuery] PaginationQuery query, CancellationToken cancellationToken)
        {
            var result = await _identityService.GetAll(query.SanitizeOrDefault(), cancellationToken);
            return Ok(new PagedResponse<UserDetailsResult>(result, query.PageNumber, query.PageSize));
        }

        /// <summary>
        /// Retrieves an individual user from the system.
        /// </summary>
        /// <param name="id">The ID of the individual user.</param>
        /// <param name="cancellationToken"></param>
        /// <response code="200">Success</response>
        /// <response code="404">Not Found</response>
        [HttpGet(Routes.Identity.Get)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Roles.User)]
        public async Task<IActionResult> Get(int id, CancellationToken cancellationToken)
        {
            var result = await _identityService.Get(id, cancellationToken);
            if (result == null)
                return NotFound(null);

            return Ok(result);
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
            var ip = _currentUserService.IpAddress;
            var details = _geoIpService.GetDetails(ip);
            var country = details?.Country.IsoCode.GetCountryFromShortName() ?? Country.UNK;
            return HandleAuthResponse(await _identityService.RegisterAsync(request.Email, request.UserName, request.Password, country, cancellationToken));
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
