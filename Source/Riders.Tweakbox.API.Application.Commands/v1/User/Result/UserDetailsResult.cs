using System;

namespace Riders.Tweakbox.API.Application.Commands.v1.User.Result
{
    public class UserDetailsResult
    {
        public int Id             { get; set; }
        public string UserName    { get; set; }

        // Account Information
        public CountryDto Country { get; set; }

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
        /// TrueSkill rating for custom matches.
        /// </summary>
        public float RatingCustom { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for custom matches.
        /// </summary>
        public float StandardDeviationCustom { get; set; }

        /// <summary>
        /// TrueSkill rating for solo matches.
        /// </summary>
        public float RatingSolo { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for solo matches.
        /// </summary>
        public float StandardDeviationSolo { get; set; }

        /// <summary>
        /// TrueSkill rating for 1v1 matches.
        /// </summary>
        public float Rating1v1 { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for 1v1 matches.
        /// </summary>
        public float StandardDeviation1v1 { get; set; }

        /// <summary>
        /// TrueSkill rating for 2v2 matches.
        /// </summary>
        public float Rating2v2 { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for 2v2 matches.
        /// </summary>
        public float StandardDeviation2v2 { get; set; }

        /// <summary>
        /// TrueSkill rating for 3v3 matches.
        /// </summary>
        public float Rating3v3 { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for 3v3 matches.
        /// </summary>
        public float StandardDeviation3v3 { get; set; }

        /// <summary>
        /// TrueSkill rating for 4v4 matches.
        /// </summary>
        public float Rating4v4 { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for 4v4 matches.
        /// </summary>
        public float StandardDeviation4v4 { get; set; }

        /// <summary>
        /// TrueSkill rating for 2v2v2 matches.
        /// </summary>
        public float Rating2v2v2 { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for 2v2v2 matches.
        /// </summary>
        public float StandardDeviation2v2v2 { get; set; }

        /// <summary>
        /// TrueSkill rating for 2v2v2v2 matches.
        /// </summary>
        public float Rating2v2v2v2 { get; set; }

        /// <summary>
        /// TrueSkill standard deviation for 2v2v2v2 matches.
        /// </summary>
        public float StandardDeviation2v2v2v2 { get; set; }
        #endregion

        /// <summary>
        /// Gets a player rating for a specified game mode.
        /// </summary>
        /// <param name="type">The match type to get the rating for.</param>
        /// <returns>Rating and standard deviation for the rating.</returns>
        public (float rating, float stdDev) GetPlayerRatingForMode(MatchTypeDto type)
        {
            switch (type)
            {
                case MatchTypeDto.Default: return (RatingCustom, StandardDeviationCustom);
                case MatchTypeDto.RankedSolo: return (RatingSolo, StandardDeviationSolo);
                case MatchTypeDto.Ranked1v1: return (Rating1v1, StandardDeviation1v1);
                case MatchTypeDto.Ranked2v2: return (Rating2v2, StandardDeviation2v2);
                case MatchTypeDto.Ranked3v3: return (Rating3v3, StandardDeviation3v3);
                case MatchTypeDto.Ranked4v4: return (Rating4v4, StandardDeviation4v4);
                case MatchTypeDto.Ranked2v2v2: return (Rating2v2v2, StandardDeviation2v2v2);
                case MatchTypeDto.Ranked2v2v2v2: return (Rating2v2v2v2, StandardDeviation2v2v2v2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

    }
}
