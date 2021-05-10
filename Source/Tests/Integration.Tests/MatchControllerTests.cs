using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Integration.Tests.Common;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Xunit;

namespace Integration.Tests
{
    public class MatchControllerTests : TestBase
    {
        [Fact]
        public async Task GetAll_WithoutAnyMatches_ReturnsEmptyResponse()
        {
            // Arrange
            await AuthenticateAsync();

            // Act
            var response = await TestClient.GetAsync(Routes.Match.Base.ToRestGetAll());
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(await response.Content.ReadFromJsonAsync<List<GetMatchResult>>());
        }


    }
}
