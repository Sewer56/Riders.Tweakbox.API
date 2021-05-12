using System.Net;
using System.Threading.Tasks;
using Integration.Tests.Common;
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
            var response = await Api.Match.GetAll();
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(response.Content);
        }
    }
}
