using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Infrastructure.Common;

namespace Riders.Tweakbox.API
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await host.Services.InitialSetupDatabase();
            await host.RunAsync();
        }

        public static async Task InitialSetupDatabase(this IServiceProvider serviceProvider)
        {
            // Entity Framework: Run migrations at Startup.
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            await dbContext.Database.MigrateAsync();

            // Create roles at startup
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            await CreateRoleIfNotExists(roleManager, Roles.Admin);
            await CreateRoleIfNotExists(roleManager, Roles.User);

            // Register default admin user.
            var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
            var configuration   = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            var adminUser       = new DefaultAdminUser();
            configuration.Bind(nameof(adminUser), adminUser);
            await identityService.TryRegisterDefaultAdminUserAsync(adminUser.AdminEmail, adminUser.Username, adminUser.Password, CancellationToken.None);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task CreateRoleIfNotExists(RoleManager<ApplicationRole> manager, string roleName)
        {
            if (!await manager.RoleExistsAsync(roleName))
                await manager.CreateAsync(new ApplicationRole(roleName));
        }
    }
}
