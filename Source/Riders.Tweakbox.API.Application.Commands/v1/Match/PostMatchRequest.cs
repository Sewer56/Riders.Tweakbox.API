using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

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
        public static Span<TeamPlacement> GetTeamPlacements(this PostMatchRequest request, Span<TeamPlacement> span)
        {
            var numTeams = request.Teams.Count;
            Span<int> points = stackalloc int[numTeams];
            points = GetTeamPoints(request, points);

            var result = span.Slice(0, numTeams);
            for (int x = 0; x < result.Length; x++)
                result[x] = new TeamPlacement() { Team = (byte) x, Points = (ushort) points[x] };

            // Sort by team time in descending order.
            result.Sort(new TeamPointsComparerDesc());

            // Rank teams
            int currentRank = 0;
            for (int x = 0; x < result.Length; x++)
            {
                ref var item = ref result[x];
                item.Rank = (byte) currentRank;

                // Check for last.
                if (x == result.Length - 1) 
                    continue;

                // Check if should increment based off of rank of next.
                if (item.Points != result[x + 1].Points)
                    currentRank++;
            }

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
            int maxPoints = placements.Length - 1;

            // Insert team values.
            var results = span.Slice(0, request.Teams.Count);
            foreach (var placement in placements)
                results[placement.Team] += (maxPoints) - placement.Rank;

            return results;
        }

        /// <summary>
        /// Returns all the players sorted from first to last place with their corresponding team.
        /// </summary>
        /// <param name="request">The originating request.</param>
        public static PlayerPlacement[] GetPlayerPlacements(this PostMatchRequest request)
        {
            var tuples = new PlayerPlacement[request.GetTotalPlayerCount()];
            int insertIndex = 0;

            for (var x = 0; x < request.Teams.Count; x++)
            {
                var team = request.Teams[x];
                foreach (var player in team)
                {
                    tuples[insertIndex++] = new PlayerPlacement() {PlayerInfo = player, Team = (ushort) x};
                }
            }

            // Sort by finish time in ascending order.
            Array.Sort(tuples, new PlayerInfoComparer());

            // Get Player Placements
            int currentRank = 0;
            for (int x = 0; x < tuples.Length; x++)
            {
                ref var item = ref tuples[x];
                item.Rank = (ushort) currentRank;

                // Check for last.
                if (x == tuples.Length - 1) 
                    continue;

                // Check if should increment based off of rank of next.
                if (item.PlayerInfo.FinishTimeFrames != tuples[x + 1].PlayerInfo.FinishTimeFrames)
                    currentRank++;
            }

            return tuples;
        }

        /// <summary>
        /// Connects a player to the team to which they are assigned.
        /// </summary>
        public struct PlayerPlacement
        {
            public ushort Team                    { get; set; }
            public ushort Rank                    { get; set; }
            public PostMatchPlayerInfo PlayerInfo { get; set; }
        }

        /// <summary>
        /// Connects a team to the amount of points they have accumulated.
        /// </summary>
        public struct TeamPlacement
        {
            /// <summary>
            /// Index of the team.
            /// </summary>
            public byte Team   { get; set; }

            /// <summary>
            /// The ranking of the team, with a lower number representing higher rank.
            /// First place has a ranking of 0.
            /// </summary>
            public byte Rank   { get; set; }

            /// <summary>
            /// Points the team has.
            /// </summary>
            public ushort Points { get; set; }
        }

        // Allows for sorting by finish time.
        private struct PlayerInfoComparer : IComparer<PlayerPlacement>
        {
            public int Compare(PlayerPlacement x, PlayerPlacement y) => x.PlayerInfo.FinishTimeFrames.CompareTo(y.PlayerInfo.FinishTimeFrames);
        }

        private struct TeamPointsComparerDesc : IComparer<TeamPlacement>
        {
            public int Compare(TeamPlacement x, TeamPlacement y) => x.Points.CompareTo(y.Points) * -1;
        }
    }
}
