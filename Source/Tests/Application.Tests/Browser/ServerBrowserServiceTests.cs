using System;
using System.Net;
using Application.Tests.Integrity.Helpers;
using Moq;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Infrastructure.Services;
using Xunit;

namespace Application.Tests.Browser
{
    public class ServerBrowserServiceTests
    {
        [Fact]
        public void RefreshServerList_RemovesItemWhenExpired()
        {
            // Arrange
            var dateTimeProvider = new Mock<IDateTimeService>();
            dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(DateTime.UtcNow - TimeSpan.FromSeconds(Constants.ServerBrowser.RefreshTimeSeconds));
            
            var browserService = new ServerBrowserService(dateTimeProvider.Object);
            var ip             = DataGenerators.GetRandomIP();

            // Act
            var result = browserService.CreateOrRefresh(ip, DataGenerators.GetPostServerRequestFaker().Generate());
            Assert.NotEmpty(browserService.GetAll().Results);

            dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(DateTime.UtcNow);
            browserService.RefreshServerList(null);

            // Assert
            Assert.Empty(browserService.GetAll().Results);
        }

        [Fact]
        public void RefreshServerList_DoesNotRemoveItemWhenNotExpired()
        {
            // Arrange
            var dateTimeProvider = new Mock<IDateTimeService>();
            dateTimeProvider.Setup(x => x.GetCurrentDateTime()).Returns(DateTime.UtcNow - TimeSpan.FromSeconds(Constants.ServerBrowser.RefreshTimeSeconds));
            
            var browserService = new ServerBrowserService(dateTimeProvider.Object);
            var ip             = DataGenerators.GetRandomIP();

            // Act
            var result = browserService.CreateOrRefresh(ip, DataGenerators.GetPostServerRequestFaker().Generate());

            // Assert
            Assert.NotEmpty(browserService.GetAll().Results);
        }

        [Fact]
        public void CreateOrRefresh_ReturnedDataIsCorrect()
        {
            // Arrange
            var browserService = new ServerBrowserService(new DateTimeService());
            var ip             = DataGenerators.GetRandomIP();
            var data           = DataGenerators.GetPostServerRequestFaker().Generate();

            // Act
            var result   = browserService.CreateOrRefresh(ip, data);
            
            // Assert
            Assert.Equal(data.Name, result.Name);
            Assert.Equal(data.Port, result.Port);
            Assert.Equal(data.Type, result.Type);
            Assert.Equal(data.Players, result.Players);
        }

        [Fact]
        public void Delete_DeletesWhenServerExists()
        {
            // Arrange
            var browserService = new ServerBrowserService(new DateTimeService());
            var ip             = DataGenerators.GetRandomIP();
            var data           = DataGenerators.GetPostServerRequestFaker().Generate();

            // Act
            var result   = browserService.CreateOrRefresh(ip, data);
            bool success = browserService.Delete(ip, data.Port, result.Id);

            // Assert
            Assert.Empty(browserService.GetAll().Results);
            Assert.True(success);
        }

        [Fact]
        public void Delete_DoesNotDeleteWhenGuidMismatch()
        {
            // Arrange
            var browserService = new ServerBrowserService(new DateTimeService());
            var ip             = DataGenerators.GetRandomIP();
            var data           = DataGenerators.GetPostServerRequestFaker().Generate();

            // Act
            var result   = browserService.CreateOrRefresh(ip, data);
            bool success = browserService.Delete(ip, data.Port, Guid.Empty);

            // Assert
            Assert.NotEmpty(browserService.GetAll().Results);
            Assert.False(success);
        }

        [Fact]
        public void Delete_DoesNotDeleteWhenServerDoesNotExist()
        {
            // Arrange
            var browserService = new ServerBrowserService(new DateTimeService());
            var ip             = IPAddress.Parse("127.0.0.1");
            var data           = DataGenerators.GetPostServerRequestFaker().Generate();

            // Act
            bool success = browserService.Delete(ip, data.Port, Guid.Empty);

            // Assert
            var getAllResult = browserService.GetAll().Results;
            Assert.Empty(getAllResult);
            Assert.False(success);
        }
    }
}
