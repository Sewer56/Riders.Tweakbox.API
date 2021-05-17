using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Riders.Tweakbox.API.Domain.Common;

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
        /// TrueSkill rating for custom matches.
        /// </summary>
        public float RatingCustom { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for custom matches.
        /// </summary>
        public float StandardDeviationCustom { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for solo matches.
        /// </summary>
        public float RatingSolo { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for solo matches.
        /// </summary>
        public float StandardDeviationSolo { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for 1v1 matches.
        /// </summary>
        public float Rating1v1 { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for 1v1 matches.
        /// </summary>
        public float StandardDeviation1v1 { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for 2v2 matches.
        /// </summary>
        public float Rating2v2 { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for 2v2 matches.
        /// </summary>
        public float StandardDeviation2v2 { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for 3v3 matches.
        /// </summary>
        public float Rating3v3 { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for 3v3 matches.
        /// </summary>
        public float StandardDeviation3v3 { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for 4v4 matches.
        /// </summary>
        public float Rating4v4 { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for 4v4 matches.
        /// </summary>
        public float StandardDeviation4v4 { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for 2v2v2 matches.
        /// </summary>
        public float Rating2v2v2 { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for 2v2v2 matches.
        /// </summary>
        public float StandardDeviation2v2v2 { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;

        /// <summary>
        /// TrueSkill rating for 2v2v2v2 matches.
        /// </summary>
        public float Rating2v2v2v2 { get; set; } = (float) Constants.Skill.DefaultInitialMean;

        /// <summary>
        /// TrueSkill standard deviation for 2v2v2v2 matches.
        /// </summary>
        public float StandardDeviation2v2v2v2 { get; set; } = (float) Constants.Skill.DefaultInitialStandardDeviation;
        #endregion

        /// <summary>
        /// Gets a player rating for a specified game mode.
        /// </summary>
        /// <param name="type">The match type to get the rating for.</param>
        /// <returns>Rating and standard deviation for the rating.</returns>
        public (float rating, float stdDev) GetPlayerRatingForMode(MatchType type)
        {
            switch (type)
            {
                case MatchType.Default: return (RatingCustom, StandardDeviationCustom);
                case MatchType.RankedSolo: return (RatingSolo, StandardDeviationSolo);
                case MatchType.Ranked1v1: return (Rating1v1, StandardDeviation1v1);
                case MatchType.Ranked2v2: return (Rating2v2, StandardDeviation2v2);
                case MatchType.Ranked3v3: return (Rating3v3, StandardDeviation3v3);
                case MatchType.Ranked4v4: return (Rating4v4, StandardDeviation4v4);
                case MatchType.Ranked2v2v2: return (Rating2v2v2, StandardDeviation2v2v2);
                case MatchType.Ranked2v2v2v2: return (Rating2v2v2v2, StandardDeviation2v2v2v2);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        /// <summary>
        /// Sets a player rating for a specified game mode.
        /// </summary>
        /// <param name="type">The match type to get the rating for.</param>
        /// <param name="rating">The new rating.</param>
        /// <param name="stdDev">The new standard deviation.</param>
        /// <returns>Rating and standard deviation for the rating.</returns>
        public void SetPlayerRatingForMode(MatchType type, float rating, float stdDev)
        {
            switch (type)
            {
                case MatchType.Default:
                    RatingCustom = rating;
                    StandardDeviationCustom = stdDev;
                    break;
                case MatchType.RankedSolo: 
                    RatingSolo = rating;
                    StandardDeviationSolo = stdDev;
                    break;
                case MatchType.Ranked1v1:
                    Rating1v1 = rating;
                    StandardDeviation1v1 = stdDev;
                    break;
                case MatchType.Ranked2v2: 
                    Rating2v2 = rating;
                    StandardDeviation2v2 = stdDev;
                    break;
                case MatchType.Ranked3v3:
                    Rating3v3 = rating;
                    StandardDeviation3v3 = stdDev;
                    break;
                case MatchType.Ranked4v4: 
                    Rating4v4 = rating;
                    StandardDeviation4v4 = stdDev;
                    break;
                case MatchType.Ranked2v2v2: 
                    Rating2v2v2 = rating;
                    StandardDeviation2v2v2 = stdDev;
                    break;
                case MatchType.Ranked2v2v2v2: 
                    Rating2v2v2v2 = rating;
                    StandardDeviation2v2v2v2 = stdDev;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
