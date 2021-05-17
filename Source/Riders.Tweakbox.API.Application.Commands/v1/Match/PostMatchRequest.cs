using System;
using System.Collections.Generic;
using System.Linq;

namespace Riders.Tweakbox.API.Application.Commands.v1.Match
{
    /// <summary>
    /// Used for fetching match data.
    /// </summary>
    public class PostMatchRequest
    {
        /// <summary>
        /// The kind of match this data represents, e.g. 1v1 Ranked
        /// </summary>
        public MatchTypeDto MatchType   { get; set; }

        /// <summary>
        /// The stage this match was played on.
        /// </summary>
        public int StageNo              { get; set; }

        /// <summary>
        /// The time when the match has been completed.
        /// </summary>
        public DateTime CompletedTime   { get; set; }

        /// <summary>
        /// A list of teams containing a list of players.
        /// </summary>
        public List<List<PostMatchPlayerInfo>> Teams { get; set; }
    }

    /// <summary>
    /// Extension methods for the post match request.
    /// This code is mostly here so that it can be shared with the mod itself.
    /// </summary>
    public static class PostMatchRequestExtensions
    {
        /// <summary>
        /// Returns the total amount of players stored in this structure.
        /// </summary>
        /// <param name="request">The source request.</param>
        public static int GetTotalPlayerCount(this PostMatchRequest request)
        {
            int count = 0;
            for (int x = 0; x < request.Teams.Count; x++)
                count += request.Teams[x].Count;

            return count;
        }

        /// <summary>
        /// Gets the placements of each individual team, sorted by first place to last place.
        /// </summary>
        /// <param name="request">The request with the player data.</param>
        /// <param name="span">A span of with 1 entry per team (Hint: MatchType.GetNumTeams()). Should be 0 initialized.</param>
        public static Span<TeamPointsTuple> GetTeamPlacements(this PostMatchRequest request, Span<TeamPointsTuple> span)
        {
            var numTeams = request.Teams.Count;
            Span<int> points = stackalloc int[numTeams];
            points = GetTeamPoints(request, points);

            var result = span.Slice(0, numTeams);
            for (int x = 0; x < result.Length; x++)
                result[x] = new TeamPointsTuple() { Team = (ushort) x, Points = (ushort) points[x] };

            result.Sort(new TeamPointsComparer());
            return result;
        }
        
        /// <summary>
        /// Gets the total amount of points for each individual team.
        /// </summary>
        /// <param name="request">The request with the player data.</param>
        /// <param name="span">A span of with 1 entry per team (Hint: MatchType.GetNumTeams()). Should be 0 initialized.</param>
        /// <returns></returns>
        public static Span<int> GetTeamPoints(this PostMatchRequest request, Span<int> span)
        {
            var placements    = GetPlayerPlacements(request);
            int pointsToAward = placements.Length;

            // Insert team values.
            var results = span.Slice(0, request.Teams.Count);
            foreach (var placement in placements)
                results[placement.Team] += pointsToAward--;

            return results;
        }

        /// <summary>
        /// Returns all the players sorted from first to last place with their corresponding team.
        /// </summary>
        /// <param name="request">The originating request.</param>
        public static PlayerTeamTuple[] GetPlayerPlacements(this PostMatchRequest request)
        {
            var tuples = new PlayerTeamTuple[request.GetTotalPlayerCount()];
            int insertIndex = 0;

            for (var x = 0; x < request.Teams.Count; x++)
            {
                var team = request.Teams[x];
                foreach (var player in team)
                {
                    tuples[insertIndex++] = new PlayerTeamTuple() {PlayerInfo = player, Team = x};
                }
            }

            // Sort by finish time in ascending order.
            Array.Sort(tuples, new PlayerInfoComparer());
            return tuples;
        }

        /// <summary>
        /// Connects a player to the team to which they are assigned.
        /// </summary>
        public struct PlayerTeamTuple
        {
            public int Team                       { get; set; }
            public PostMatchPlayerInfo PlayerInfo { get; set; }
        }

        /// <summary>
        /// Connects a team to the amount of points they have accumulated.
        /// </summary>
        public struct TeamPointsTuple
        {
            public ushort Team   { get; set; }
            public ushort Points { get; set; }
        }

        // Allows for sorting by finish time.
        private struct PlayerInfoComparer : IComparer<PlayerTeamTuple>
        {
            public int Compare(PlayerTeamTuple x, PlayerTeamTuple y) => x.PlayerInfo.FinishTimeFrames.CompareTo(y.PlayerInfo.FinishTimeFrames);
        }

        private struct TeamPointsComparer : IComparer<TeamPointsTuple>
        {
            public int Compare(TeamPointsTuple x, TeamPointsTuple y) => x.Points.CompareTo(y.Points);
        }
    }
}
