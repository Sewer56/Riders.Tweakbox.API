using System;
using System.Linq;
using Application.Tests.Integrity.Helpers;
using AutoFixture;
using AutoFixture.Kernel;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Domain.Models;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Domain.Models.Memory;
using Xunit;

namespace Application.Tests.Mappings
{
    public class MappingTests
    {
        private Fixture _fixture;

        public MappingTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void CanMapMatchPlayersToTeams()
        {
            var faker = DataGenerators.Match.GetMatchCommand(0, 100);

            for (int x = 0; x < 50; x++)
            {
                // Arrange
                var fixtureGet  = faker.Generate();
                var fixturePost = Mapping.Mapper.Map<PostMatchRequest>(faker.Generate());

                // Act
                var matchGet  = Mapping.Mapper.Map<Match>(fixtureGet);
                var matchPost = Mapping.Mapper.Map<Match>(fixturePost);

                // Assert
                Assert.Equal(matchGet.Players.Count, fixtureGet.Teams.Sum(x => x.Count));
                Assert.Equal(matchPost.Players.Count, fixturePost.Teams.Sum(x => x.Count));

                Assert.Equal(matchGet.MatchType.GetNumTeams(), fixtureGet.Teams.Count);
                Assert.Equal(matchPost.MatchType.GetNumTeams(), fixturePost.Teams.Count);
            }
        }

        [Fact]
        public void CanMapTeamsToPlayers()
        {
            for (int x = 0; x < 50; x++)
            {
                // Arrange
                var fixture = _fixture.Create<Match>();

                // Act
                var commandGet = Mapping.Mapper.Map<GetMatchResult>(fixture);
                var commandPost = Mapping.Mapper.Map<PostMatchRequest>(fixture);

                // Assert
                Assert.Equal(fixture.Players.Count, commandGet.Teams.Sum(x => x.Count));
                Assert.Equal(fixture.Players.Count, commandPost.Teams.Sum(x => x.Count));
            }
        }

        [Theory]
        [InlineData(typeof(GetMatchResult), typeof(PostMatchRequest))]
        [InlineData(typeof(GetMatchPlayerInfo), typeof(PostMatchPlayerInfo))]
        [InlineData(typeof(PlayerRaceDetails), typeof(GetMatchPlayerInfo))]
        [InlineData(typeof(Match), typeof(GetMatchResult))] // Requires special handling, verify in another test.
        [InlineData(typeof(GetMatchResult), typeof(Match))] // Requires special handling, verify in another test.
        [InlineData(typeof(Match), typeof(PostMatchRequest))] // Requires special handling, verify in another test.
        [InlineData(typeof(PostMatchRequest), typeof(Match))] // Requires special handling, verify in another test.
        [InlineData(typeof(ServerInfo), typeof(ServerCreatedResult))]
        [InlineData(typeof(ServerInfo), typeof(GetServerResult))]
        [InlineData(typeof(PostServerRequest), typeof(ServerInfo))]
        [InlineData(typeof(ApplicationUser), typeof(UserDetailsResult))]
        public void ShouldSupportMapFromSourceToDestination(Type source, Type destination)
        {
            var fixture = _fixture.Create(source, new SpecimenContext(_fixture));
            var result  = Mapping.Mapper.Map(fixture, source, destination);
        }
    }
}
