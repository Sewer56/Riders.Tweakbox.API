using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;
using Riders.Tweakbox.API.Application.Commands.v1.User;

namespace Riders.Tweakbox.API.SDK.Sample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            const string Username = "RedSwordW";
            const string Password = "SamplePassword12";

            var tweakbox = new TweakboxApi("https://localhost:6969");

            // Register
            var registerResponse = await tweakbox.Identity.Register(new UserRegistrationRequest()
            {
                Email = "admin@tweakbox.com",
                UserName = Username,
                Password = Password
            });

            if (registerResponse.StatusCode == HttpStatusCode.BadRequest)
            {
                // Probably already registered
                if (registerResponse.Error != null)
                    Console.WriteLine($"Register Error: {registerResponse.Error.Content}");
            }
            else
            {
                Console.WriteLine($"Registered, Token: {registerResponse.Content.Token}");
            }

            // Login
            var loginResponse = await tweakbox.Identity.Login(new UserLoginRequest()
            {
                Username = Username,
                Password = Password
            });

            Console.WriteLine($"Login: {loginResponse.Content.Token}");

            // Create Server
            var serverCreate = await tweakbox.Browser.CreateOrRefresh(new PostServerRequest()
            {
                Name = "Sewer's Server",
                Port = 1,
                Type = MatchTypeDto.Default,
                Players = new List<ServerPlayerInfoResult>()
                {
                    new ServerPlayerInfoResult() { Latency = 999, Name = "Snfn" },
                    new ServerPlayerInfoResult() { Latency = 120, Name = "Pixel" },
                }
            });

            var getAll = await tweakbox.Browser.GetAll();

            // Delete Server
            await tweakbox.Browser.Delete(serverCreate.Content.Id, 1);
        }
    }
}
