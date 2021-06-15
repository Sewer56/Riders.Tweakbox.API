using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Riders.Tweakbox.API;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Models.Config;
using Riders.Tweakbox.API.Infrastructure.Common;
using Riders.Tweakbox.API.SDK;

namespace Integration.Tests.Common
{
    public abstract class TestBase
    {
        public TweakboxApi Api { get; protected set; }

        protected DefaultAdminUser AdminUserDetails { get; set; }
        protected readonly HttpClient TestClient;
        protected readonly TestWebApplicationFactory Factory;
        protected const string DefaultUserName = "ArgieArgieArgie";
        protected const string DefaultEmail = "Admin@IStillLoveYou.net";
        protected const string DefaultPassword = "IStillLoveYou!11!111";
        
        protected TestBase()
        {
            Factory = new TestWebApplicationFactory();
            TestClient = Factory.CreateClient();

            using var scope = Factory.Services.CreateScope();
            Task.Run(() => ClearDatabase(scope.ServiceProvider)).Wait();

            Api = new TweakboxApi(handlers => Factory.CreateDefaultClient(handlers));
        }

        protected async Task AuthenticateAsAdminAsync()
        {
            var loginResult = await Api.TryAuthenticate(AdminUserDetails.Username, AdminUserDetails.Password);
            if (loginResult.IsT1)
                throw new Exception("Failed to Login");
        }

        protected async Task RegisterAndAuthenticateAsync()
        {
            var registerResult = await Api.IdentityApi.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = DefaultUserName,
                Password = DefaultPassword,
            });

            if (registerResult.StatusCode != HttpStatusCode.OK)
                throw new Exception("Failed to Register");

            var loginResult = await Api.TryAuthenticate(DefaultUserName, DefaultPassword);
            if (loginResult.IsT1)
                throw new Exception("Failed to Login");
        }

        private async Task ClearDatabase(IServiceProvider provider)
        {
            var context = provider.GetRequiredService<ApplicationDbContext>();
            var types   = context.Model.GetEntityTypes();
            
            foreach (var type in types)
            {
                try
                {
                    var tableName = type.GetTableName();
                    context.Database.ExecuteSqlRaw($"DELETE FROM {tableName}");
                }
                catch (Exception)
                {
                    // ignored
                }
            }

            try
            {
                await context.SaveChangesAsync();
            }
            catch (Exception)
            {
                // ignored
            }

            // Now setup db after clearing
            AdminUserDetails = await provider.InitialSetupDatabase();
        }
    }
}
