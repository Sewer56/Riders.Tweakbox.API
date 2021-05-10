using System.ComponentModel.DataAnnotations;

namespace Riders.Tweakbox.API.Domain.Models.Database
{
    public class PlayerRaceDetails
    {
        [Key]
        public int Id { get; set; }

        public int PlayerId    { get; set; }
        public Player Player   { get; set; }

        public int MatchId     { get; set; }
        public Match Match     { get; set; }

        public byte Character  { get; set; }
        public byte Board      { get; set; }

        /// <summary>
        /// If this is -1, the player did not finish.
        /// </summary>
        public int FinishTimeFrames { get; set; }
        public int FastestLapFrames { get; set; }
    }
}
