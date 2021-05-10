using System.Net;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface ICurrentUserService
    {
        IPAddress IpAddress { get; }
        string UserId { get; }
    }
}