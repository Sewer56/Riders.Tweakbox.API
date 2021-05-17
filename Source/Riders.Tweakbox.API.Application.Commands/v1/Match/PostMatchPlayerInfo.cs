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
        /// The team number assigned to the player.
        /// This is automatically set during mapping!
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonIgnore]
        public int TeamNo { get; set; }

        /// <summary>
        /// [Internal Use Only]
        /// Player rating in the respective gamemode after completing this match.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonIgnore]
        public float Rating { get; set; }

        /// <summary>
        /// [Internal Use Only]
        /// Player standard deviation in the respective gamemode after completing this match.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        [JsonIgnore]
        public float StdDev { get; set; }
    }
}
