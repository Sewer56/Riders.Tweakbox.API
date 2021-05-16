using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Application.Tests.Integrity.Helpers;
using Integration.Tests.Common;
using Integration.Tests.Helpers;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
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
            var response = await Api.Match.GetAll(null);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Empty(response.Content.Items);
        }

        [Fact]
        public async Task GetAll_WithSingleMatch_ReturnsOne()
        {
            const int players = 8;
            const int matches = 1;

            // Arrange
            await AuthenticateAsync();
            var registrations = await Api.IdentityApi.SeedUsers(players);
            var users         = await Api.IdentityApi.GetAll(new PaginationQuery() { PageSize = players, PageNumber = 0 });
            var minUserId     = users.Content.Items.Min(x => x.Id);
            var maxUserId     = users.Content.Items.Max(x => x.Id);

            // Act
            var seed     = await Api.Match.SeedMatches(matches, minUserId, maxUserId);
            var response = await Api.Match.GetAll(null);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(matches, response.Content.Items.Count);
        }

        [Fact]
        public async Task GetAll_WithMatches_ReturnsMany()
        {
            const int players = 200;
            const int matches = 20;

            // Arrange
            await AuthenticateAsync();
            var registrations = await Api.IdentityApi.SeedUsers(players);
            var users         = await Api.IdentityApi.GetAll(new PaginationQuery() { PageSize = players, PageNumber = 0 });
            var minUserId     = users.Content.Items.Min(x => x.Id);
            var maxUserId     = users.Content.Items.Max(x => x.Id);

            // Act
            var seed     = await Api.Match.SeedMatches(matches, minUserId, maxUserId);
            var response = await Api.Match.GetAll(null);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(response.Content.Items);
        }
    }
}
