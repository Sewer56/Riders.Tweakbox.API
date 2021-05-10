using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Riders.Tweakbox.API.Application.Services;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <inheritdoc />
        public IPAddress IpAddress => _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress;
        public string UserId => _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
    }
}
