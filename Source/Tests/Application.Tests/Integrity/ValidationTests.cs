using System;
using System.Collections.Generic;
using Application.Tests.Integrity.Helpers;
using AutoFixture;
using FluentValidation;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Domain.Common;
using Xunit;

namespace Application.Tests.Integrity
{
    public class ValidationTests
    {
        private Fixture _fixture;

        public ValidationTests()
        {
            _fixture = new Fixture();
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void CanValidateCorrectGetMatchCommandData()
        {
            // Arrange
            var generator = DataGenerators.Match.GetMatchCommand(0, 100);
            var validator = Validator.Get<GetMatchResult>();

            for (int x = 0; x < 100; x++)
            {
                // Act & Assert
                var fixture = generator.Generate();

                // Assert
                var result = validator.Validate(fixture);
                Assert.True(result.IsValid, result.ToString());
            }
        }

        [Fact]
        public void CanValidateCorrectPostMatchCommandData()
        {
            // Arrange
            var generator = DataGenerators.Match.GetMatchCommand(0, 100);
            var validator = Validator.Get<PostMatchRequest>();

            for (int x = 0; x < 100; x++)
            {
                // Act & Assert
                var fixture = Mapping.Mapper.Map<PostMatchRequest>(generator.Generate());

                // Assert
                var result = validator.Validate(fixture);
                Assert.True(result.IsValid, result.ToString());
            }
        }

        [Fact]
        public void CanValidateIncorrectGetMatchCommandData()
        {
            // Arrange
            var generator = DataGenerators.Match.GetMatchCommand(0, 100);
            var validator = Validator.Get<GetMatchResult>();

            for (int x = 0; x < 100; x++)
            {
                // Act & Assert
                var fixture = generator.Generate();
                var teamCount = fixture.MatchType.GetNumTeams();
                var teamPlayerCount = fixture.MatchType.GetNumPlayersPerTeam();

                // Assert Correct
                AssertTrue(validator, fixture);

                // Incorrect: Stage
                AssertInvalidMember(fixture, x => x.StageNo, (x, value) => x.StageNo = value, DataGenerators.GetRandomNumberOutsideRange(Constants.Race.MinStageNo, Constants.Race.MinStageNo), validator, "Stage No Validation Failed");
                AssertInvalidMember(fixture, x => x.MatchType, (x, value) => x.MatchType = value, DataGenerators.GetEnumOutsideRange<MatchTypeDto>(), validator, "Match Type Validation Failed");
                AssertInvalidMember(fixture, x => x.CompletedTime, (x, value) => x.CompletedTime = value, DateTime.UtcNow.AddMinutes(DataGenerators.Random.NextDouble() + 1), validator, "Match Type Validation Failed");
                AssertInvalidMember(fixture, x => x.Teams, (x, value) => x.Teams = value, null, validator, "Team Null Validation Failed");
                AssertInvalidMember(fixture, x => x.Teams, (x, value) => x.Teams = value, 
                                    AllocateListWithSize<List<GetMatchPlayerInfo>>(DataGenerators.GetRandomNumberOutsideRange(teamCount, teamCount + 1, 0, 128)), 
                                    validator, "Team Count Validation Failed");

                AssertInvalidMember(fixture, x => x.Teams, (x, value) => x.Teams = value, 
                                    AllocateListOfListsWithSize<GetMatchPlayerInfo>(teamCount, DataGenerators.GetRandomNumberOutsideRange(teamPlayerCount, teamPlayerCount + 1, 0, 128)), 
                                    validator, "Player Count Validation Failed");
            }
        }

        [Fact]
        public void CanValidateIncorrectPostMatchCommandData()
        {
            // Arrange
            var generator = DataGenerators.Match.GetMatchCommand(0, 100);
            var validator = Validator.Get<PostMatchRequest>();

            for (int x = 0; x < 100; x++)
            {
                // Act & Assert
                var fixture = Mapping.Mapper.Map<PostMatchRequest>(generator.Generate());
                var teamCount = fixture.MatchType.GetNumTeams();
                var teamPlayerCount = fixture.MatchType.GetNumPlayersPerTeam();

                // Assert Correct
                AssertTrue(validator, fixture);

                // Incorrect: Stage
                AssertInvalidMember(fixture, x => x.StageNo, (x, value) => x.StageNo = value, DataGenerators.GetRandomNumberOutsideRange(Constants.Race.MinStageNo, Constants.Race.MinStageNo), validator, "Stage No Validation Failed");
                AssertInvalidMember(fixture, x => x.MatchType, (x, value) => x.MatchType = value, DataGenerators.GetEnumOutsideRange<MatchTypeDto>(), validator, "Match Type Validation Failed");
                AssertInvalidMember(fixture, x => x.CompletedTime, (x, value) => x.CompletedTime = value, DateTime.UtcNow.AddMinutes(DataGenerators.Random.NextDouble() + 1), validator, "Match Type Validation Failed");
                AssertInvalidMember(fixture, x => x.Teams, (x, value) => x.Teams = value, null, validator, "Team Null Validation Failed");
                  AssertInvalidMember(fixture, x => x.Teams, (x, value) => x.Teams = value, 
                                    AllocateListWithSize<List<PostMatchPlayerInfo>>(DataGenerators.GetRandomNumberOutsideRange(teamCount, teamCount + 1, 0, 128)), 
                                    validator, "Team Count Validation Failed");

                AssertInvalidMember(fixture, x => x.Teams, (x, value) => x.Teams = value, 
                                    AllocateListOfListsWithSize<PostMatchPlayerInfo>(teamCount, DataGenerators.GetRandomNumberOutsideRange(teamPlayerCount, teamPlayerCount + 1, 0, 128)), 
                                    validator, "Player Count Validation Failed");
            }
        }

        // Helper Classes
        void AssertTrue<T>(AbstractValidator<T> validator, T fixture)
        {
            var result = validator.Validate(fixture);
            Assert.True(result.IsValid, result.ToString());
        }

        // Asserts an individual member.
        void AssertInvalidMember<T, TProperty>(T fixture, Func<T, TProperty> getValue, Action<T, TProperty> setValue, TProperty value, AbstractValidator<T> validator, string message)
        {
            // Act
            var oldValue = getValue(fixture);
            setValue(fixture, value);

            // Assert
            var result   = validator.Validate(fixture);
            Assert.False(result.IsValid, message);
            
            // Restore & Assert
            setValue(fixture, oldValue);
            AssertTrue(validator, fixture);
        }

        List<T> AllocateListWithSize<T>(int size) => new List<T> ( new T[size] );
        
        List<List<T>> AllocateListOfListsWithSize<T>(int numLists, int size)
        {
            List<List<T>> result = new List<List<T>>(numLists);
            for (int x = 0; x < numLists; x++)
                result.Add(AllocateListWithSize<T>(size));

            return result;
        }
    }
}
