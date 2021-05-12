using System;
using System.Net;
using System.Threading.Tasks;
using Integration.Tests.Common;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User;
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
            var response = await Api.Identity.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = DefaultUserName,
                Password = "RandomPassword",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_PasswordHasDigits()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.Identity.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = $"{DefaultUserName}2",
                Password = "RandomPassword",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_PasswordHasLowercase()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.Identity.Register( new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = $"{DefaultUserName}2",
                Password = "UPPERCASEONLY123",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_PasswordTooShort()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.Identity.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = $"{DefaultUserName}2",
                Password = "Arg1e",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Register_CanRegisterUser()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.Identity.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = $"{DefaultUserName}2",
                Password = "RandomPassword123",
            });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.False(String.IsNullOrEmpty(response.Content.Token));
            Assert.False(String.IsNullOrEmpty(response.Content.RefreshToken));
        }

        [Fact]
        public async Task Register_CannotExceedMaxLength()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.Identity.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = $"aaaaaaaaaabbbbbbbbbbccccccccccddd",
                Password = "RandomPassword123",
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }
    }
}
