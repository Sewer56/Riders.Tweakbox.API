using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Domain.Models;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> TryRegisterDefaultAdminUserAsync(string email, string username, string password, CancellationToken cancellationToken);
        Task<AuthenticationResult> RegisterAsync(string email, string username, string password, Country country, CancellationToken cancellationToken);
        Task<AuthenticationResult> LoginAsync(string username, string password, CancellationToken cancellationToken);
        Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken, CancellationToken cancellationToken);
        Task<List<UserDetailsResult>> GetAll(PaginationQuery paginationQuery, CancellationToken token);
        Task<UserDetailsResult> Get(int id, CancellationToken token);
    }
}