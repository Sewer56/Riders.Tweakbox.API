using System.Linq;
using System.Threading.Tasks;
using Moserware.Skills;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Services;
using Riders.Tweakbox.API.Domain.Common;
using Riders.Tweakbox.API.Domain.Models;
using Riders.Tweakbox.API.Domain.Models.Database;
using Riders.Tweakbox.API.Infrastructure.Common;

namespace Riders.Tweakbox.API.Infrastructure.Services
{
    public class StatisticsCalculatorService : IStatisticsCalculatorService
    {
        public GameInfo DefaultGameInfo => new GameInfo(Constants.Skill.DefaultInitialMean, Constants.Skill.DefaultInitialStandardDeviation, Constants.Skill.DefaultBeta, Constants.Skill.DefaultDynamicsFactor, Constants.Skill.DefaultDrawProbability);
        public ApplicationDbContext _context;
        
        public StatisticsCalculatorService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task UpdateRatings(PostMatchRequest request)
        {
            // Get Player => User and Team Data
            var data       = await GetPlayerAndTeamData(request);
            var placements = new PostMatchRequestExtensions.TeamPlacement[request.Teams.Count];
            request.GetTeamPlacements(placements);

            // Extract Ranks
            var ranks = new int[request.Teams.Count];
            for (int x = 0; x < ranks.Length; x++)
                ranks[x] = GetRankForTeam(placements, x);

            var teamDictionaries = data.teams.Select(x => x.AsDictionary());
            var ratings = TrueSkillCalculator.CalculateNewRatings(DefaultGameInfo, teamDictionaries, ranks);

            // Lowest Rank (1st Place)
            foreach (var player in data.players)
            {
                var rating    = ratings[player.Player];
                bool isWinner = ranks[0] == ranks[player.TeamIndex];

                player.User.IncrementGameCounter((MatchType) request.MatchType, isWinner);
                player.User.SetPlayerRatingForMode((MatchType) request.MatchType, (float) rating.Mean, (float) rating.StandardDeviation);
                player.PlayerInfo.Rating = (float) rating.Mean;
                player.PlayerInfo.StdDev = (float) rating.StandardDeviation;
            }

            await _context.SaveChangesAsync();
        }

        private int GetRankForTeam(PostMatchRequestExtensions.TeamPlacement[] placements, int teamId)
        {
            foreach (var placement in placements)
            {
                if (placement.Team == teamId)
                    return placement.Rank;
            }

            return -1;
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
                    people[userIndex++] = new PlayerUserTuple(player, user, requestPlayer, x);
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
            public int TeamIndex;

            public PlayerUserTuple(Player player, ApplicationUser user, PostMatchPlayerInfo playerInfo, int teamIndex)
            {
                Player = player;
                User = user;
                PlayerInfo = playerInfo;
                TeamIndex = teamIndex;
            }
        }
    }
}
