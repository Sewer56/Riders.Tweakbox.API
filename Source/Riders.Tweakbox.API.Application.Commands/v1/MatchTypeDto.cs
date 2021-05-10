using System;

namespace Riders.Tweakbox.API.Application.Commands.v1
{
    /// <summary>
    /// Default = 0<br/>
    /// RankedSolo = 1<br/>
    /// Ranked1v1 = 2<br/>
    /// Ranked2v2 = 3<br/>
    /// Ranked3v3 = 4<br/>
    /// Ranked4v4 = 5<br/>
    /// Ranked2v2v2 = 6<br/>
    /// Ranked2v2v2v2 = 7
    /// </summary>
    public enum MatchTypeDto : int
    {
        /// <summary>
        /// Unranked / Custom / Default.
        /// </summary>
        Default = 0,

        /// <summary>
        /// Ranked 1v1v1v1...
        /// </summary>
        RankedSolo = 1,

        /// <summary>
        /// Competitive 1v1 Ranked
        /// </summary>
        Ranked1v1 = 2,

        /// <summary>
        /// Competitive 2v2 Ranked
        /// </summary>
        Ranked2v2 = 3,

        /// <summary>
        /// Competitive 3v3 Ranked
        /// </summary>
        Ranked3v3 = 4,

        /// <summary>
        /// Competitive 4v4 Ranked
        /// </summary>
        Ranked4v4 = 5,

        /// <summary>
        /// Competitive 2v2v2 Ranked
        /// </summary>
        Ranked2v2v2 = 6,

        /// <summary>
        /// Competitive 2v2v2v2 Ranked
        /// </summary>
        Ranked2v2v2v2 = 7
    }

    public static class MatchTypeExtensions
    {
        public static int GetNumPlayersPerTeam(this MatchTypeDto matchTypeDto)
        {
            return matchTypeDto switch
            {
                MatchTypeDto.Default       => 1,
                MatchTypeDto.RankedSolo    => 1,
                MatchTypeDto.Ranked1v1     => 1,
                MatchTypeDto.Ranked2v2     => 2,
                MatchTypeDto.Ranked3v3     => 3,
                MatchTypeDto.Ranked4v4     => 4,
                MatchTypeDto.Ranked2v2v2   => 2,
                MatchTypeDto.Ranked2v2v2v2 => 2,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public static int GetNumTeams(this MatchTypeDto matchTypeDto)
        {
            return matchTypeDto switch
            {
                MatchTypeDto.Default       => 8,
                MatchTypeDto.RankedSolo    => 8,
                MatchTypeDto.Ranked1v1     => 2,
                MatchTypeDto.Ranked2v2     => 2,
                MatchTypeDto.Ranked3v3     => 2,
                MatchTypeDto.Ranked4v4     => 2,
                MatchTypeDto.Ranked2v2v2   => 3,
                MatchTypeDto.Ranked2v2v2v2 => 4,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}