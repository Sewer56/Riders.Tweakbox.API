using System;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Integration.Tests.Common;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Xunit;

namespace Integration.Tests
{
    public class IdentityControllerTests : TestBase
    {
        [Fact]
        public async Task Register_UserNameIsUnique()
        {
            // Arrange
            // Create Default User
            await AuthenticateAsync();

            // Act
            // Try Create Default User Again
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                UserName = DefaultUserName,
                Password = "RandomPassword",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Content.ReadFromJsonAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_PasswordHasDigits()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                UserName = $"{DefaultUserName}2",
                Password = "RandomPassword",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Content.ReadFromJsonAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_PasswordHasLowercase()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                UserName = $"{DefaultUserName}2",
                Password = "UPPERCASEONLY123",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Content.ReadFromJsonAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_PasswordTooShort()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                UserName = $"{DefaultUserName}2",
                Password = "Arg1e",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Content.ReadFromJsonAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_CanRegisterUser()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                UserName = $"{DefaultUserName}2",
                Password = "RandomPassword123",
            });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var registrationResponse = await response.Content.ReadFromJsonAsync<AuthSuccessResponse>();
            Assert.False(String.IsNullOrEmpty(registrationResponse.Token));
            Assert.False(String.IsNullOrEmpty(registrationResponse.RefreshToken));
        }

        [Fact]
        public async Task Register_CannotExceedMaxLength()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await TestClient.PostAsJsonAsync(Routes.Identity.Register, new UserRegistrationRequest()
            {
                UserName = $"aaaaaaaaaabbbbbbbbbbccccccccccddd",
                Password = "RandomPassword123",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Content.ReadFromJsonAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }
    }
}
