using System;
using System.Net;
using System.Threading.Tasks;
using Application.Tests.Integrity.Helpers;
using Integration.Tests.Common;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Models;
using Xunit;

namespace Integration.Tests
{
    public class ServerBrowserControllerTests : TestBase
    {
        [Fact]
        public async Task CreateOrRefresh_CanCreateItem()
        {
            // Act & Arrange
            var generator = DataGenerators.ServerBrowser.GetPostServerRequest();
            var item      = generator.Generate();
            var result    = await Api.Browser.CreateOrRefresh(item);

            // Assert
            Assert.Equal(HttpStatusCode.OK, result.StatusCode);

            var mapped = Mapping.Mapper.Map<PostServerRequest>(result.Content);
            Assert.Equal(item, mapped);
        }

        [Fact]
        public async Task GetAll_CanQueryItems()
        {
            // Act & Arrange
            var generator = DataGenerators.ServerBrowser.GetPostServerRequest();
            var item      = generator.Generate();
            await Api.Browser.CreateOrRefresh(item);
            var getAll    = await Api.Browser.GetAll();

            // Assert
            var mapped = Mapping.Mapper.Map<PostServerRequest>(getAll.Content.Results[0]);
            Assert.Equal(item, mapped);
        }

        [Fact]
        public async Task Delete_CanDeleteItem()
        {
            // Act & Arrange
            var generator = DataGenerators.ServerBrowser.GetPostServerRequest();
            var item      = generator.Generate();
            var result    = await Api.Browser.CreateOrRefresh(item);
            await Api.Browser.Delete(result.Content.Id, item.Port);
            var getAll    = await Api.Browser.GetAll();

            // Assert
            Assert.Empty(getAll.Content.Results);
        }

        [Fact]
        public async Task Delete_WithInvalidId_ReturnsNotFound()
        {
            // Act & Arrange
            var generator = DataGenerators.ServerBrowser.GetPostServerRequest();
            var item      = generator.Generate();
            var result    = await Api.Browser.CreateOrRefresh(item);
            var response  = await Api.Browser.Delete(Guid.NewGuid(), item.Port);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task Delete_WithInvalidPort_ReturnsNotFound()
        {
            // Act & Arrange
            var generator = DataGenerators.ServerBrowser.GetPostServerRequest();
            var item      = generator.Generate();
            var result    = await Api.Browser.CreateOrRefresh(item);
            var response  = await Api.Browser.Delete(result.Content.Id, item.Port + 1 % 65535);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
