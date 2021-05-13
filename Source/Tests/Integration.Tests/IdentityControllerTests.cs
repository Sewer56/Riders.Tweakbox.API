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
                Password = DefaultPassword,
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
            var response = await Api.Identity.Register(new UserRegistrationRequest()
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
        public async Task Register_NameCannotExceedMaxLength()
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

        [Fact]
        public async Task Refresh_ReturnsToken()
        {
            // Arrange & Act
            // Try Create Default User Again
            await AuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            var response = await Api.Identity.Refresh(new RefreshTokenRequest()
            {
                RefreshToken = lastResponse.RefreshToken,
                Token = lastResponse.Token
            });

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEqual(lastResponse.Token, response.Content.Token);
            Assert.NotEqual(lastResponse.RefreshToken, response.Content.RefreshToken);
        }

        [Fact]
        public async Task Refresh_ReturnsValidToken()
        {
            // Arrange & Act
            // Try Create Default User Again
            await AuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            await Api.Identity.Refresh(new RefreshTokenRequest()
            {
                RefreshToken = lastResponse.RefreshToken,
                Token = lastResponse.Token
            });

            // Call API requiring auth.
            var matchResponse = await Api.Match.GetAll(null);

            // Assert
            Assert.Equal(HttpStatusCode.OK, matchResponse.StatusCode);
            Assert.Empty(matchResponse.Content.Items);
        }

        [Fact]
        public async Task Refresh_ReturnsBadRequestForInvalidToken()
        {
            // Arrange & Act
            // Try Create Default User Again
            await AuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            var response = await Api.Identity.Refresh(new RefreshTokenRequest()
            {
                RefreshToken = lastResponse.RefreshToken,
                Token = lastResponse.Token + 'a'
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Refresh_ReturnsNothingForInvalidRefreshToken()
        {
            // Arrange & Act
            // Try Create Default User Again
            await AuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            var response = await Api.Identity.Refresh(new RefreshTokenRequest()
            {
                RefreshToken = lastResponse.RefreshToken + 'a',
                Token = lastResponse.Token
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Login_HasBadUsername()
        {
            // Arrange & Act
            // Try Create Default User Again
            await AuthenticateAsync();

            var response = await Api.Identity.Login(new UserLoginRequest()
            {
                Username = DefaultUserName + 'a',
                Password = DefaultPassword
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }

        [Fact]
        public async Task Login_HasBadPassword()
        {
            // Arrange & Act
            // Try Create Default User Again
            await AuthenticateAsync();

            var response = await Api.Identity.Login(new UserLoginRequest()
            {
                Username = DefaultUserName,
                Password = DefaultPassword + 'a'
            });

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);

            var registrationResponse = await response.Error.GetContentAsAsync<ErrorReponse>();
            Assert.NotEmpty(registrationResponse.Errors);
        }
    }
}
