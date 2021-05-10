using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Riders.Tweakbox.API.Domain.Models.Database
{
    [Index(nameof(MatchType))]
    public class Match
    {
        [Key]
        public int Id { get; set; }
        public int StageNo { get; set; }
        public DateTime CompletedTime { get; set; }
        public MatchType MatchType { get; set; }

        // Details
        public List<PlayerRaceDetails> Players { get; set; }

        // Match Properties
        public int GetNumPlayersPerTeam() => MatchType.GetNumPlayersPerTeam();
        public int GetNumTeams() => MatchType.GetNumTeams();
    }
}