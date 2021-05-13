using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Integration.Tests.Common
{
    public class FakeRemoteIpMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPAddress _fakeIpAddress = IPAddress.Parse("192.168.1.200");

        public FakeRemoteIpMiddleware(RequestDelegate next) => this._next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Connection.RemoteIpAddress = _fakeIpAddress;
            await this._next(httpContext);
        }
    }
}
