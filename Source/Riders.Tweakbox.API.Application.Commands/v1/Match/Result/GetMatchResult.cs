using System;
using System.Collections.Generic;

namespace Riders.Tweakbox.API.Application.Commands.v1.Match.Result
{
    /// <summary>
    /// Used for fetching match data.
    /// </summary>
    public class GetMatchResult
    {
        /// <summary>
        /// Unique Id of the individual match.
        /// </summary>
        public int Id                   { get; set; }

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
        public List<List<GetMatchPlayerInfo>> Teams { get; set; }
    }
}
