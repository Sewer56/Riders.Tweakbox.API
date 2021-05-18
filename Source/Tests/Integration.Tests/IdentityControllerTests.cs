using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Integration.Tests.Common;
using Integration.Tests.Helpers;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Error;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Xunit;

namespace Integration.Tests
{
    public class IdentityControllerTests : TestBase
    {
        [Fact]
        public async Task Register_WithAlreadyUsedUsername_ReturnsError()
        {
            // Arrange
            // Create Default User
            await RegisterAndAuthenticateAsync();

            // Act
            // Try Create Default User Again
            var response = await Api.IdentityApi.Register(new UserRegistrationRequest()
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
        public async Task Register_WithoutDigitsInPassword_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.IdentityApi.Register(new UserRegistrationRequest()
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
        public async Task Register_WithoutLowercase_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.IdentityApi.Register(new UserRegistrationRequest()
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
        public async Task Register_WithPasswordTooShort_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.IdentityApi.Register(new UserRegistrationRequest()
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
            var response = await Api.IdentityApi.Register(new UserRegistrationRequest()
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
        public async Task GetAll_WhenUnauthorized_CannotQueryUsers()
        {
            // Authenticate for GetAll Endpoint
            string userName = $"{DefaultUserName}2";

            // Arrange & Act
            // Try Create Default User Again
            await Api.IdentityApi.Register(new UserRegistrationRequest()
            {
                Email = DefaultEmail,
                UserName = userName,
                Password = "RandomPassword123",
            });

            var getResponse = await Api.IdentityApi.GetAll(null);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, getResponse.StatusCode);
        }

        [Fact]
        public async Task GetAll_CanQueryUsers()
        {
            // Authenticate for GetAll Endpoint
            string userName = $"{DefaultUserName}2";
            await RegisterAndAuthenticateAsync();

            // Arrange & Act
            // Try Create Default User Again
            await Api.IdentityApi.Register(new UserRegistrationRequest()
            {
                Email = $"{DefaultEmail}2",
                UserName = userName,
                Password = "RandomPassword123",
            });

            var getResponse = await Api.IdentityApi.GetAll(null);

            // Assert
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.NotNull(getResponse.Content.Items.FirstOrDefault(x => x.UserName.Equals(userName)));
        }

        [Fact]
        public async Task GetAll_CanQueryUsers_SupportsPagination()
        {
            const int numUsers   = 25;
            const int pageSize   = 10;
            const int pageOneNumber = 0;
            const int pageTwoNumber = 1;

            // Authenticate for GetAll Endpoint
            await RegisterAndAuthenticateAsync();

            // Arrange & Act
            // Try Create Default User Again
            var actualUsers = await Api.IdentityApi.SeedUsers(numUsers);

            var getFirstPage = await Api.IdentityApi.GetAll(new PaginationQuery()
            {
                PageNumber = pageOneNumber,
                PageSize   = pageSize
            });

            var getSecondPage = await Api.IdentityApi.GetAll(new PaginationQuery()
            {
                PageNumber = pageTwoNumber,
                PageSize   = pageSize
            });

            // Assert
            Assert.Equal(HttpStatusCode.OK, getFirstPage.StatusCode);
            Assert.Equal(pageSize, getFirstPage.Content.Items.Count);
            Assert.Equal(pageSize, getFirstPage.Content.PageSize);
            Assert.Equal(pageOneNumber, getFirstPage.Content.PageNumber);

            Assert.Equal(HttpStatusCode.OK, getSecondPage.StatusCode);
            Assert.Equal(pageSize, getSecondPage.Content.Items.Count);
            Assert.Equal(pageSize, getSecondPage.Content.PageSize);
            Assert.Equal(pageTwoNumber, getSecondPage.Content.PageNumber);
        }

        [Fact]
        public async Task Register_WithTooLongName_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            var response = await Api.IdentityApi.Register(new UserRegistrationRequest()
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
            await RegisterAndAuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            var response = await Api.IdentityApi.Refresh(new RefreshTokenRequest()
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
            await RegisterAndAuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            await Api.IdentityApi.Refresh(new RefreshTokenRequest()
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
        public async Task Refresh_WithInvalidToken_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            await RegisterAndAuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            var response = await Api.IdentityApi.Refresh(new RefreshTokenRequest()
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
        public async Task Refresh_WithInvalidRefreshToken_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            await RegisterAndAuthenticateAsync();

            var lastResponse = Api.Handler.CachedAuthResponse;
            var response = await Api.IdentityApi.Refresh(new RefreshTokenRequest()
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
        public async Task Login_WithBadUsername_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            await RegisterAndAuthenticateAsync();

            var response = await Api.IdentityApi.Login(new UserLoginRequest()
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
        public async Task Login_WithInvalidPassword_ReturnsError()
        {
            // Arrange & Act
            // Try Create Default User Again
            await RegisterAndAuthenticateAsync();

            var response = await Api.IdentityApi.Login(new UserLoginRequest()
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
