using System;
using System.Collections.Generic;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Xunit;

namespace Application.Tests.Stats
{
    public class StatisticsTests
    {
        [Fact]
        public void CalculateTeamPlacements_WithFourWayTie()
        {
            // Arrange
            int numTeams = MatchTypeDto.Ranked2v2v2v2.GetNumTeams();
            var request = new PostMatchRequest()
            {
                MatchType = MatchTypeDto.Ranked2v2v2v2,
                CompletedTime = DateTime.UtcNow,
                StageNo = 0,
                Teams = new List<List<PostMatchPlayerInfo>>()
                {
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                    },
                }
            };

            // Act
            var placements = request.GetPlayerPlacements();
            var teamRanks = request.GetTeamPlacements(new Span<PostMatchRequestExtensions.TeamPlacement>(new PostMatchRequestExtensions.TeamPlacement[numTeams]));

            // Assert Player Rank
            Assert.Equal(0, placements[0].Rank);
            Assert.Equal(0, placements[1].Rank);

            Assert.Equal(0, placements[2].Rank);
            Assert.Equal(0, placements[3].Rank);

            Assert.Equal(0, placements[4].Rank);
            Assert.Equal(0, placements[5].Rank);

            Assert.Equal(0, placements[6].Rank);
            Assert.Equal(0, placements[7].Rank);

            // Assert Team Rank
            Assert.Equal(0, teamRanks[0].Rank);
            Assert.Equal(0, teamRanks[1].Rank);

            Assert.Equal(0, teamRanks[2].Rank);
            Assert.Equal(0, teamRanks[3].Rank);
        }

        [Fact]
        public void CalculatePlayerPlacements_WithFourTeams()
        {
            // Arrange
            var request = new PostMatchRequest()
            {
                MatchType = MatchTypeDto.Ranked2v2v2v2,
                CompletedTime = DateTime.UtcNow,
                StageNo = 0,
                Teams = new List<List<PostMatchPlayerInfo>>()
                {
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207, PlayerId = 1 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 9338 , PlayerId = 2 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 5044 , PlayerId = 3 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 9969 , PlayerId = 4 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 8722 , PlayerId = 5 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 7756 , PlayerId = 6 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 7026 , PlayerId = 7 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 9883 , PlayerId = 8 },
                    },
                }
            };

            // Act
            var placements = request.GetPlayerPlacements();

            // Assert
            Assert.Equal(1, placements[0].Team);
            Assert.Equal(3, placements[0].PlayerInfo.PlayerId);

            Assert.Equal(3, placements[1].Team);
            Assert.Equal(7, placements[1].PlayerInfo.PlayerId);

            Assert.Equal(2, placements[2].Team);
            Assert.Equal(6, placements[2].PlayerInfo.PlayerId);

            Assert.Equal(2, placements[3].Team);
            Assert.Equal(5, placements[3].PlayerInfo.PlayerId);

            Assert.Equal(0, placements[4].Team);
            Assert.Equal(2, placements[4].PlayerInfo.PlayerId);

            Assert.Equal(3, placements[5].Team);
            Assert.Equal(8, placements[5].PlayerInfo.PlayerId);

            Assert.Equal(1, placements[6].Team);
            Assert.Equal(4, placements[6].PlayerInfo.PlayerId);

            Assert.Equal(0, placements[7].Team);
            Assert.Equal(1, placements[7].PlayerInfo.PlayerId);
        }

        [Fact]
        public void CalculateTeamRanks_WithFourTeamsAndTie()
        {
            // Arrange
            int numTeams = MatchTypeDto.Ranked2v2v2v2.GetNumTeams();
            var request = new PostMatchRequest()
            {
                MatchType = MatchTypeDto.Ranked2v2v2v2,
                CompletedTime = DateTime.UtcNow,
                StageNo = 0,
                Teams = new List<List<PostMatchPlayerInfo>>()
                {
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 10207 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 9338 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 5044 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 9969 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 8722 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 7756 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 7026 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 9883 },
                    },
                }
            };

            // Act
            var placements = request.GetTeamPlacements(new Span<PostMatchRequestExtensions.TeamPlacement>(new PostMatchRequestExtensions.TeamPlacement[numTeams]));

            // Assert

            /*
                Team 0: 3 + 0 = 3
                Team 1: 7 + 1 = 8
                Team 2: 5 + 4 = 9
                Team 3: 6 + 2 = 8
            */

            var teamZero = GetTeam(placements, 0);
            var teamOne = GetTeam(placements, 1);
            var teamTwo = GetTeam(placements, 2);
            var teamThree = GetTeam(placements, 3);

            Assert.Equal(teamZero.Rank, 2);
            Assert.Equal(teamZero.Points, 3);

            Assert.Equal(teamOne.Rank, 1);
            Assert.Equal(teamOne.Points, 8);

            Assert.Equal(teamTwo.Rank, 0);
            Assert.Equal(teamTwo.Points, 9);

            Assert.Equal(teamThree.Rank, 1);
            Assert.Equal(teamThree.Points, 8);

            PostMatchRequestExtensions.TeamPlacement GetTeam(Span<PostMatchRequestExtensions.TeamPlacement> placements, int teamId)
            {
                foreach (var placement in placements)
                {
                    if (placement.Team == teamId)
                        return placement;
                }

                throw new Exception("Placement for team not found.");
            }
        }

        [Fact]
        public void CalculateTeamRanks_WithFourTeamsNoTie()
        {
            // Arrange
            int numTeams = MatchTypeDto.Ranked2v2v2v2.GetNumTeams();
            var request = new PostMatchRequest()
            {
                MatchType = MatchTypeDto.Ranked2v2v2v2,
                CompletedTime = DateTime.UtcNow,
                StageNo = 0,
                Teams = new List<List<PostMatchPlayerInfo>>()
                {
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 1 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 2 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 3 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 4 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 5 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 6 },
                    },
                    new List<PostMatchPlayerInfo>()
                    {
                        new PostMatchPlayerInfo() { FinishTimeFrames = 7 },
                        new PostMatchPlayerInfo() { FinishTimeFrames = 8 },
                    },
                }
            };

            // Act
            var placements = request.GetTeamPlacements(new Span<PostMatchRequestExtensions.TeamPlacement>(new PostMatchRequestExtensions.TeamPlacement[numTeams]));

            // Assert

            /*
                Team 0: 7 + 6 = 13
                Team 1: 5 + 4 = 9
                Team 2: 3 + 2 = 5
                Team 3: 1 + 0 = 1
            */

            var teamZero = GetTeam(placements, 0);
            var teamOne = GetTeam(placements, 1);
            var teamTwo = GetTeam(placements, 2);
            var teamThree = GetTeam(placements, 3);

            Assert.Equal(teamZero.Rank, 0);
            Assert.Equal(teamZero.Points, 13);

            Assert.Equal(teamOne.Rank, 1);
            Assert.Equal(teamOne.Points, 9);

            Assert.Equal(teamTwo.Rank, 2);
            Assert.Equal(teamTwo.Points, 5);

            Assert.Equal(teamThree.Rank, 3);
            Assert.Equal(teamThree.Points, 1);

            PostMatchRequestExtensions.TeamPlacement GetTeam(Span<PostMatchRequestExtensions.TeamPlacement> placements, int teamId)
            {
                foreach (var placement in placements)
                {
                    if (placement.Team == teamId)
                        return placement;
                }

                throw new Exception("Placement for team not found.");
            }
        }
    }
}
