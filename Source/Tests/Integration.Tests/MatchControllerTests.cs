using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Integration.Tests.Common;
using Integration.Tests.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Riders.Tweakbox.API.Application.Commands;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Infrastructure.Common;
using StructLinq;
using Xunit;

namespace Integration.Tests
{
    public class MatchControllerTests : TestBase
    {
        [Fact]
        public async Task GetAll_WithoutAnyMatches_ReturnsEmptyResponse()
        {
            // Arrange
            await RegisterAndAuthenticateAsync();

            // Act
            var response = await Api.MatchApi.GetAll(null);
            
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
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Act
            await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId, 2, MatchTypeDto.RankedSolo);
            var response = await Api.MatchApi.GetAll(null);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(matches, response.Content.Items.Count);
        }

        [Theory]
        [InlineData(MatchTypeDto.Default)]
        [InlineData(MatchTypeDto.RankedSolo)]
        [InlineData(MatchTypeDto.Ranked1v1)]
        [InlineData(MatchTypeDto.Ranked2v2)]
        [InlineData(MatchTypeDto.Ranked2v2v2)]
        [InlineData(MatchTypeDto.Ranked2v2v2v2)]
        [InlineData(MatchTypeDto.Ranked3v3)]
        [InlineData(MatchTypeDto.Ranked4v4)]
        public async Task Create_WithAllGameModes_AffectMatchMakingRating(MatchTypeDto gameMode)
        {
            var teams = gameMode.GetNumTeams();
            var players = teams * gameMode.GetNumPlayersPerTeam();
            const int matches = 1;

            // Arrange & Create Players
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Test Cases
            // Act
            await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId, 250, gameMode);
            
            // Assert
            var newUsers = await Api.IdentityApi.GetAll(new PaginationQuery() { PageSize = players, PageNumber = 0 });

            // Note: If there is an odd number of teams, the team coming in the middle place
            // will have their ratings unchanged.
            for (int x = 0; x < players; x++)
            {
                var newPlayer      = newUsers.Content.Items[x];
                var originalPlayer = users.users.Items.Find(x => x.Id == newPlayer.Id);

                var newRating      = newPlayer.GetPlayerRatingForMode(gameMode);
                var originalRating = originalPlayer.GetPlayerRatingForMode(gameMode);

                // If the ratings are not equal everything is as expected, if they are,
                // ensure change was made by checking standard deviation.
                if (newRating.rating == originalRating.rating)
                    Assert.NotEqual(originalRating.stdDev, newRating.stdDev);
            } 
        }

        [Theory]
        [InlineData(MatchTypeDto.Default)]
        [InlineData(MatchTypeDto.RankedSolo)]
        [InlineData(MatchTypeDto.Ranked1v1)]
        [InlineData(MatchTypeDto.Ranked2v2)]
        [InlineData(MatchTypeDto.Ranked2v2v2)]
        [InlineData(MatchTypeDto.Ranked2v2v2v2)]
        [InlineData(MatchTypeDto.Ranked3v3)]
        [InlineData(MatchTypeDto.Ranked4v4)]
        public async Task Create_WithAllGameModes_IncrementsTotalGamesPlayed(MatchTypeDto gameMode)
        {
            var teams = gameMode.GetNumTeams();
            var players = teams * gameMode.GetNumPlayersPerTeam();
            const int matches = 1;

            // Arrange & Create Players
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Test Cases
            // Act
            await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId, 250, gameMode);
            
            // Assert
            var newUsers = await Api.IdentityApi.GetAll(new PaginationQuery() { PageSize = players, PageNumber = 0 });

            // Assert Games Played
            for (int x = 0; x < players; x++)
            {
                var newPlayer      = newUsers.Content.Items[x];
                var originalPlayer = users.users.Items.Find(x => x.Id == newPlayer.Id);

                var newGamesPlayed      = newPlayer.GetGamesPlayed(gameMode);
                var originalGamesPlayed = originalPlayer.GetGamesPlayed(gameMode);

                Assert.Equal(originalGamesPlayed.totalGames + 1, newGamesPlayed.totalGames);
            }
        }

        [Fact]
        public async Task Create_WithSoloMatchmaking_SupportsCustomTeamCount()
        {
            const int players = 6;
            const int matches = 1;

            // Arrange
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Act
            await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId, 250, MatchTypeDto.RankedSolo, players, 1);
            
            // Assert
            var newUsers = await Api.IdentityApi.GetAll(new PaginationQuery() { PageSize = players, PageNumber = 0 });
            for (int x = 0; x < players; x++)
            {
                var newPlayer      = newUsers.Content.Items[x];
                var originalPlayer = users.users.Items.Find(x => x.Id == newPlayer.Id);
                Assert.NotEqual(newPlayer.RatingSolo, originalPlayer.RatingSolo);
            }
        }
        
        [Theory]
        [InlineData(MatchTypeDto.Ranked2v2v2)]
        [InlineData(MatchTypeDto.Ranked2v2v2v2)]
        public async Task Create_WithMultiTeams_AffectsMatchMakingRating(MatchTypeDto gameMode)
        {
            int players = gameMode.GetNumTeams() * gameMode.GetNumPlayersPerTeam();
            const int matches = 1;

            // Arrange
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Act
            await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId, 250, gameMode);
            
            // Assert
            // As all players started with the same rating and all 3 teams were of 2 players,
            // there should be 3 unique rankings 2 times.
            var newUsers = await Api.IdentityApi.GetAll(new PaginationQuery() { PageSize = players, PageNumber = 0 });
            var ratings  = new Dictionary<float, int>();
            for (int x = 0; x < players; x++)
            {
                var rating = newUsers.Content.Items[x].GetPlayerRatingForMode(gameMode).rating;
                ratings.TryGetValue(rating, out int currentValue);
                ratings[rating] = ++currentValue;
            }

            // There can be draws leading to not always the exact expected amount
            // so we only check if it's in range. Might make some more specialised tests down the road.
            Assert.InRange(ratings.Count, 1, gameMode.GetNumTeams());
            foreach (var rating in ratings)
                Assert.InRange(rating.Value, gameMode.GetNumPlayersPerTeam(), players);
        }

        [Fact]
        public async Task GetAll_WithMatches_ReturnsMany()
        {
            const int players = 50;
            const int matches = 30;

            // Arrange
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Act
            await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId);
            var response = await Api.MatchApi.GetAll(null);
            
            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotEmpty(response.Content.Items);
        }

        [Fact]
        public async Task Delete_WithSingleMatch_DeletesCascade()
        {
            const int players = 8;
            const int matches = 1;

            // Arrange
            await AuthenticateAsAdminAsync();
            var users = await SeedPlayersAsync(players);
            var scope   = base.Factory.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Act & Assert: Seeding
            var seededMatches = await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId);
            Assert.NotEmpty(seededMatches);
            Assert.NotEmpty(context.RaceDetails.ToList());

            // Act & Assert: Delete
            var deleted = await Api.MatchApi.Delete(seededMatches[0].Id);
            Assert.True(deleted.Content);

            var matchResponses = await Api.MatchApi.GetAll(null);
            Assert.Empty(matchResponses.Content.Items);

            // Act & Assert: Cascade Deletion
            var results = context.RaceDetails.ToList();
            Assert.Empty(results);
        }

        [Fact]
        public async Task Delete_WithInvalidMatch_ReturnsFalse()
        {
            const int players = 8;
            const int matches = 1;

            // Arrange
            await AuthenticateAsAdminAsync();
            var users = await SeedPlayersAsync(players);

            // Act & Assert: Seeding
            var seededMatches = await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId);

            // Act & Assert: Delete
            var deleted = await Api.MatchApi.Delete(seededMatches[0].Id + 1);
            Assert.Equal(HttpStatusCode.NotFound, deleted.StatusCode);
            Assert.False(deleted.Content);
        }

        [Fact]
        public async Task Get_WithValidMatch_ReturnsOk()
        {
            const int players = 8;
            const int matches = 1;

            // Arrange
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Act & Assert: Seeding
            var seededMatches = await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId);

            // Act & Assert: Delete
            var get = await Api.MatchApi.Get(seededMatches[0].Id);

            Assert.Equal(HttpStatusCode.OK, get.StatusCode);
            Assert.NotNull(get.Content);
            Assert.Equal(seededMatches[0].Id, get.Content.Id);
            Assert.Equal(seededMatches[0].StageNo, get.Content.StageNo);
        }

        [Fact]
        public async Task Get_WithInvalidMatch_ReturnsNotFound()
        {
            const int players = 8;
            const int matches = 1;

            // Arrange
            await RegisterAndAuthenticateAsync();
            var users = await SeedPlayersAsync(players);

            // Act & Assert: Seeding
            var seededMatches = await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId);

            // Act & Assert: Delete
            var get = await Api.MatchApi.Get(seededMatches[0].Id + 1);
            Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
        }

        [Fact]
        public async Task Update_WithValidMatch_SuccessfullyUpdates()
        {
            const int players = 8;
            const int matches = 1;

            // Arrange
            await AuthenticateAsAdminAsync();
            var users = await SeedPlayersAsync(players);

            // Act & Assert: Seeding
            var seededMatches = await Api.MatchApi.SeedMatches(matches, users.minId, users.maxId);
            var seededMatch = seededMatches[0];
            var playerOne = seededMatch.Teams[0][0];
            
            playerOne.Board = 0;
            playerOne.Character = 0;
            playerOne.FastestLapFrames = 0;
            seededMatch.StageNo = 0;

            // Act & Assert: Delete
            var get = await Api.MatchApi.Update(seededMatches[0].Id, Mapping.Mapper.Map<PostMatchRequest>(seededMatch));
            var getMatch = await Api.MatchApi.Get(seededMatches[0].Id);

            Assert.Equal(HttpStatusCode.OK, get.StatusCode);
            Assert.True(get.Content);

            var resultPlayer = getMatch.Content.Teams.SelectMany(x => x).First(x => x.PlayerId == playerOne.PlayerId);
            Assert.Equal(playerOne.Board, resultPlayer.Board);
            Assert.Equal(playerOne.Character, resultPlayer.Character);
            Assert.Equal(playerOne.FastestLapFrames, resultPlayer.FastestLapFrames);
            Assert.Equal(seededMatch.StageNo, getMatch.Content.StageNo);
        }

        private async Task<(int minId, int maxId, PagedResponse<UserDetailsResult> users)> SeedPlayersAsync(int players)
        {
            await Api.IdentityApi.SeedUsers(players);
            var users = await Api.IdentityApi.GetAll(new PaginationQuery() {PageSize = players, PageNumber = 0});
            var minUserId = users.Content.Items.Min(x => x.Id);
            var maxUserId = users.Content.Items.Max(x => x.Id);
            return (minUserId, maxUserId, users.Content);
        }
    }
}
