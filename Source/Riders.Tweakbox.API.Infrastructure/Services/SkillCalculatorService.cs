using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moserware.Skills;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Infrastructure.Common;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class SkillCalculatorService : ISkillCalculatorService
    {
        public GameInfo DefaultGameInfo => new GameInfo(Constants.Skill.DefaultInitialMean, Constants.Skill.DefaultInitialStandardDeviation, Constants.Skill.DefaultBeta, Constants.Skill.DefaultDynamicsFactor, Constants.Skill.DefaultDrawProbability);
        public ApplicationDbContext _context;
        
        public SkillCalculatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task UpdateRatings(PostMatchRequest request)
        {
            // Get Player => User and Team Data
            var data   = await GetPlayerAndTeamData(request);
            var points = new int[request.Teams.Count];
            request.GetTeamPoints(points);

            var teamDictionaries = data.teams.Select(x => x.AsDictionary());
            var ratings = TrueSkillCalculator.CalculateNewRatings(DefaultGameInfo, teamDictionaries, points);

            foreach (var player in data.players)
            {
                var rating = ratings[player.Player];
                player.User.SetPlayerRatingForMode((MatchType) request.MatchType, (float) rating.Mean, (float) rating.StandardDeviation);
                player.PlayerInfo.Rating = (float) rating.Mean;
            }

            await _context.SaveChangesAsync();
        }

        private async Task<(Team[] teams, PlayerUserTuple[] players)> GetPlayerAndTeamData(PostMatchRequest request)
        {
            var numTeams  = request.Teams.Count;
            var teams     = new Team[numTeams];
            var people    = new PlayerUserTuple[request.GetTotalPlayerCount()];
            int userIndex = 0;

            // Populate Teams.
            for (int x = 0; x < numTeams; x++)
            {
                var team = new Team();
                var requestTeam = request.Teams[x];

                // Populate Players
                foreach (var requestPlayer in requestTeam)
                {
                    var user   = await _context.Users.FindAsync(requestPlayer.PlayerId);
                    var player = new Player(requestPlayer.PlayerId);
                    var currentRating = user.GetPlayerRatingForMode((MatchType) request.MatchType);

                    team.AddPlayer(player, new Rating(currentRating.rating, currentRating.stdDev));
                    people[userIndex++] = new PlayerUserTuple(player, user, requestPlayer);
                }

                teams[x] = team;
            }

            return (teams, people);
        }

        private struct PlayerUserTuple
        {
            public Player Player;
            public ApplicationUser User;
            public PostMatchPlayerInfo PlayerInfo;

            public PlayerUserTuple(Player player, ApplicationUser user, PostMatchPlayerInfo playerInfo)
            {
                Player = player;
                User = user;
                PlayerInfo = playerInfo;
            }
        }
    }
}
