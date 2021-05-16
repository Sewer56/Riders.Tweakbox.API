using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Riders.Tweakbox.API;

namespace Integration.Tests.Common
{
    public class TestWebApplicationFactory : WebApplicationFactory<Startup>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return Program.CreateHostBuilder(null)
                .ConfigureServices(services =>
                {
                    services.AddSingleton<IStartupFilter, CustomStartupFilter>();
                });
        }

        public class CustomStartupFilter : IStartupFilter
        {
            public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
            {
                return app =>
                {
                    app.UseMiddleware<FakeRemoteIpMiddleware>();
                    next(app);
                };
            }
        }
    }
}
