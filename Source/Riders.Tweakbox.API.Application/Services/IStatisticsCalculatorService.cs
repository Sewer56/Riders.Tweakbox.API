using System.Collections.Generic;
using System.Threading.Tasks;
using Moserware.Skills;
using Riders.Tweakbox.API.Application.Commands.v1.Match;

namespace Riders.Tweakbox.API.Application.Services
{
    public interface IStatisticsCalculatorService
    {
        /// <summary>
        /// Updates the ratings of each player referenced in the request.
        /// </summary>
        /// <param name="request">Incoming request received from a call to submit a new match result.</param>
        Task UpdateRatings(PostMatchRequest request);
    }
}