using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Riders.Tweakbox.API;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Infrastructure.Common;

namespace Integration.Tests.Common
{
    public abstract class TestBase
    {
        protected readonly HttpClient TestClient;
        protected const string DefaultUserName = "ArgieArgieArgie";
        protected const string DefaultEmail = "Admin@IStillLoveYou.net";

        protected TestBase()
        {
            var appFactory = new WebApplicationFactory<Startup>();
            TestClient     = appFactory.CreateClient();

            using var scope = appFactory.Services.CreateScope();
            Task.Run(() => ClearDatabase(scope.ServiceProvider)).Wait();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = DefaultUserName,
                Password = "IStillLoveYou!11!111",
            });

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        private static async Task ClearDatabase(IServiceProvider provider)
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
            await provider.InitialSetupDatabase();
        }
    }
}
