using System.Threading;
using System.Threading.Tasks;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> TryRegisterDefaultAdminUserAsync(string username, string password, CancellationToken cancellationToken);
        Task<AuthenticationResult> RegisterAsync(string username, string password, CancellationToken cancellationToken);
        Task<AuthenticationResult> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
    }
}