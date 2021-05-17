using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Riders.Tweakbox.API.Application.Commands.v1.Match
{
    /// <summary>
    /// Information related to an individual player who played a match.
    /// </summary>
    public class PostMatchPlayerInfo
    {
        /// <summary>
        /// Unique Id of the player associated with this info.
        /// </summary>
        public int PlayerId { get; set; }

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
        /// Set this to int.MaxValue if the player did not finish.
        /// </summary>
        public int FinishTimeFrames { get; set; }

        /// <summary>
        /// Amount of frames the fastest lap took.
        /// If this is -1, the player did not finish.
        /// </summary>
        public int FastestLapFrames { get; set; }

        /// <summary>
        /// [Internal Use Only]
        /// Player rating in the respective gamemode after completing this match.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonIgnore]
        public float Rating { get; set; }
    }
}
