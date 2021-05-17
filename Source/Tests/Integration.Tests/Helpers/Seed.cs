using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using Application.Tests.Integrity.Helpers;
using Refit;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Commands.v1.User.Result;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Domain.Models;
using Riders.Tweakbox.API.SDK.Interfaces;

namespace Integration.Tests.Helpers
{
    public static class Seed
    {
        /// <summary>
        /// Seeds a number of users up to a specified amount.
        /// </summary>
        /// <param name="identityApi">Access to the Identity API</param>
        /// <param name="count">Up to this number of users will be created.</param>
        /// <param name="maxParallelism">Max level of parallelism.</param>
        /// <returns>Amount of users successfully seeded.</returns>
        public static async Task<List<AuthSuccessResponse>> SeedUsers(this IIdentityApi identityApi, int count, int maxParallelism = 250)
        {
            if (count < maxParallelism)
                maxParallelism = count;

            var generator = DataGenerators.Identity.GetRegistrationRequest();
            var responses = new List<AuthSuccessResponse>();
            var tasks     = new Task<ApiResponse<AuthSuccessResponse>>[maxParallelism];
            var identities = generator.Generate(count);

            int identityCount = 0;
            int remaining = count;
            
            // Populate All Users in Async
            while (remaining > 0)
            {
                int remainingTasks = Math.Min(maxParallelism, remaining);
                for (int x = 0; x < remainingTasks; x++)
                {
                    tasks[x] = identityApi.Register(identities[identityCount++]);
                    remaining--;
                }

                await Task.WhenAll(tasks);

                for (int x = 0; x < remainingTasks; x++)
                {
                    var result = tasks[x].Result;
                    if (result.StatusCode == HttpStatusCode.OK)
                        responses.Add(result.Content);
                }
            }
            
            return responses;
        }

        /// <summary>
        /// Seeds a number of matches.
        /// </summary>
        /// <param name="match">Access to the match API.</param>
        /// <param name="count">The number of matches.</param>
        /// <param name="minPlayerId">Minimum Player Id (Inclusive)</param>
        /// <param name="maxPlayerId">Maximum Player Id (Inclusive)</param>
        /// <param name="maxParallelism">Max level of parallelism.</param>
        /// <param name="matchType">The type of match to seed.</param>
        /// <param name="numTeams">Number of teams.</param>
        /// <param name="numPlayersPerTeam">Custom number of players to use.</param>
        /// <returns></returns>
        public static async Task<List<GetMatchResult>> SeedMatches(this IMatchApi match, int count, int minPlayerId, int maxPlayerId, int maxParallelism = 250, MatchTypeDto? matchType = null, int? numTeams = null, int? numPlayersPerTeam = null)
        {
            if (count < maxParallelism)
                maxParallelism = count;

            var generator = DataGenerators.Match.GetMatchCommand(minPlayerId, maxPlayerId, matchType, numTeams, numPlayersPerTeam);
            var results   = new List<GetMatchResult>(count);
            var tasks     = new Task<ApiResponse<GetMatchResult>>[maxParallelism];
            var seeds     = generator.Generate(count);
            var matches   = Mapping.Mapper.Map<List<PostMatchRequest>>(seeds);

            int currentMatch = 0;
            int remaining = count;

            // Populate All matches in Async
            while (remaining > 0)
            {
                int remainingTasks = Math.Min(maxParallelism, remaining);
                for (int x = 0; x < remainingTasks; x++)
                {
                    tasks[x]   = match.Create(matches[currentMatch++]);
                    remaining--;
                }

                await Task.WhenAll(tasks);

                for (int x = 0; x < remainingTasks; x++)
                {
                    var result = tasks[x].Result;
                    if (result.StatusCode == HttpStatusCode.Created)
                        results.Add(result.Content);
                }
            }

            return results;
        }
    }
}
