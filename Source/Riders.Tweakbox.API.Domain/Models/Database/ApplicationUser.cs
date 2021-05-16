using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Riders.Tweakbox.API.Domain.Models.Database
{
    public class ApplicationUser : IdentityUser<int>
    {
        // Account Information
        public Country Country { get; set; }

        #region Statistics
        public int NumGamesTotal   { get; set; }

        public int NumWinsCustom   { get; set; }
        public int NumGamesCustom  { get; set; }

        public int NumWinsSolo     { get; set; }
        public int NumGamesSolo    { get; set; }

        public int NumWins1v1      { get; set; }
        public int NumGames1v1     { get; set; }

        public int NumWins2v2      { get; set; }
        public int NumGames2v2     { get; set; }

        public int NumWins3v3      { get; set; }
        public int NumGames3v3     { get; set; }

        public int NumWins4v4      { get; set; }
        public int NumGames4v4     { get; set; }

        public int NumWins2v2v2    { get; set; }
        public int NumGames2v2v2   { get; set; }

        public int NumWins2v2v2v2  { get; set; }
        public int NumGames2v2v2v2 { get; set; }

        /// <summary>
        /// TrueSkill rating for solo matches.
        /// </summary>
        public short RatingSolo { get; set; } = 1000;

        /// <summary>
        /// TrueSkill rating for 1v1 matches.
        /// </summary>
        public short Rating1v1 { get; set; } = 1000;

        /// <summary>
        /// TrueSkill rating for 2v2 matches.
        /// </summary>
        public short Rating2v2 { get; set; } = 1000;

        /// <summary>
        /// TrueSkill rating for 3v3 matches.
        /// </summary>
        public short Rating3v3 { get; set; } = 1000;

        /// <summary>
        /// TrueSkill rating for 4v4 matches.
        /// </summary>
        public short Rating4v4 { get; set; } = 1000;

        /// <summary>
        /// TrueSkill rating for 2v2v2 matches.
        /// </summary>
        public short Rating2v2v2 { get; set; } = 1000;

        /// <summary>
        /// TrueSkill rating for 2v2v2v2 matches.
        /// </summary>
        public short Rating2v2v2v2 { get; set; } = 1000;
        #endregion
    }
}
