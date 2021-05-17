namespace Riders.Tweakbox.API.Application.Commands.v1.Match.Result
{
    /// <summary>
    /// Information related to an individual player who played a match.
    /// </summary>
    public class GetMatchPlayerInfo
    {
        /// <summary>
        /// Unique Id of the match associated with this info.
        /// </summary>
        public int MatchId           { get; set; }

        /// <summary>
        /// Unique Id of the player associated with this info.
        /// </summary>
        public int PlayerId         { get; set; }

        /// <summary>
        /// The character that was used by the player.
        /// </summary>
        public byte Character       { get; set; }

        /// <summary>
        /// The board that was used by the player.
        /// </summary>
        public byte Board           { get; set; }

        /// <summary>
        /// Amount of frames between timer hitting 0:00 and race end.
        /// If this is -1, the player did not finish.
        /// </summary>
        public int FinishTimeFrames { get; set; }

        /// <summary>
        /// Amount of frames the fastest lap took.
        /// If this is -1, the player did not finish.
        /// </summary>
        public int FastestLapFrames { get; set; }

        /// <summary>
        /// Player rating in the respective gamemode after completing this match.
        /// </summary>
        public int Rating { get; set; }
    }
}
