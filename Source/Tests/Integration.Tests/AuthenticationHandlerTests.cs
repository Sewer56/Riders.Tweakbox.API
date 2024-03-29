﻿using System;
using System.Net;
using System.Threading.Tasks;
using Integration.Tests.Common;
using Moq;
using Riders.Tweakbox.API.SDK;
using Riders.Tweakbox.API.SDK.Helpers;
using Xunit;

namespace Integration.Tests
{
    public class AuthenticationHandlerTests : TestBase
    {
        [Fact]
        public async Task GetToken_RefreshedOnExpiry()
        {
            // Arrange
            var dateTimeProviderMock = new Mock<DateTimeProvider>();
            Api = new TweakboxApi(handlers => Factory.CreateDefaultClient(handlers), dateTimeProviderMock.Object);
            await this.RegisterAndAuthenticateAsync();

            var cachedResponse = Api.AuthHandler.CachedAuthResponse; // Original Tokens

            // Act || Expire token and try accessing a restricted endpoint.
            dateTimeProviderMock.Setup(x => x.GetCurrentDateTimeUtc()).Returns(() => Api.AuthHandler.Token.ValidTo + TimeSpan.FromSeconds(1));
            var matches = await Api.MatchApi.GetAll();

            // Assert || First that token changed and second that response worked
            Assert.NotEqual(cachedResponse, Api.AuthHandler.CachedAuthResponse);
            Assert.Equal(HttpStatusCode.OK, matches.StatusCode);
        }
    }
}
