using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Bogus;
using EnumsNET;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Browser;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Application.Commands.v1.User;
using Riders.Tweakbox.API.Domain.Common;

namespace Application.Tests.Integrity.Helpers
{
    public static class DataGenerators
    {
        public static Random Random = new Random();

        public static class Identity
        {
            public static Faker<UserRegistrationRequest> GetRegistrationRequest()
            {
                return new Faker<UserRegistrationRequest>()
                    .StrictMode(true)
                    .RuleFor(x => x.UserName, x => x.Internet.UserName())
                    .RuleFor(x => x.Password, x => x.Internet.Password(32, false, "\\w", "La2"))
                    .RuleFor(x => x.Email, x => x.Internet.Email());
            }
        }

        public static class Match
        {
            public static Faker<GetMatchResult> GetMatchCommand(int minPlayerId, int maxPlayerId, MatchTypeDto? matchType = null, int? numTeams = null, int? numPlayersPerTeam = null)
            {
                return new Faker<GetMatchResult>()
                    .StrictMode(true)
                    .RuleFor(x => x.Id, x => x.IndexGlobal)
                    .RuleFor(x => x.CompletedTime, () => DateTime.UtcNow)
                    .RuleFor(x => x.MatchType, x => matchType ?? x.PickRandom<MatchTypeDto>())
                    .RuleFor(x => x.StageNo, x => x.Random.Int(Constants.Race.MinStageNo, Constants.Race.MaxStageNo))
                    .RuleFor(x => x.Teams, (faker, result) => MakeValidGetTeamData(result, minPlayerId, maxPlayerId, numTeams, numPlayersPerTeam));
            }

            public static Faker<GetMatchPlayerInfo> GetPlayerInfo(int matchId, int playerId, int teamId)
            {
                return new Faker<GetMatchPlayerInfo>()
                    .StrictMode(true)
                    .RuleFor(x => x.MatchId, x => matchId)
                    .RuleFor(x => x.PlayerId, x => playerId)
                    .RuleFor(x => x.TeamNo, x => teamId)
                    .RuleFor(x => x.Board, x => x.Random.Byte(Constants.Race.MinGearNo, Constants.Race.MaxGearNo))
                    .RuleFor(x => x.Character, x => x.Random.Byte(Constants.Race.MinCharacterNo, Constants.Race.MaxCharacterNo))
                    .RuleFor(x => x.FastestLapFrames, x => x.Random.Int(-1, 3600))
                    .RuleFor(x => x.FinishTimeFrames, x => x.Random.Int(3600, 10800))
                    .RuleFor(x => x.Rating, x => x.Random.Int(300, 2000))
                    .FinishWith((faker, info) =>
                    {
                        if (info.FastestLapFrames == -1)
                            info.FinishTimeFrames = -1;
                    });
            }

            private static List<List<GetMatchPlayerInfo>> MakeValidGetTeamData(GetMatchResult command, int minPlayerId, int maxPlayerId, int? numTeams, int? numPlayersPerTeam)
            {
                var teamCount    = numTeams ?? command.MatchType.GetNumTeams();
                var playerCount  = numPlayersPerTeam ?? command.MatchType.GetNumPlayersPerTeam();
                var ids    = GetUniqueIntegers(minPlayerId, maxPlayerId + 1, teamCount * playerCount);
                var result = new List<List<GetMatchPlayerInfo>>(teamCount);

                for (int x = 0; x < teamCount; x++)
                {
                    var team = new List<GetMatchPlayerInfo>();
                    for (int y = 0; y < playerCount; y++)
                    {
                        int index = (playerCount * x) + y;
                        team.Add(GetPlayerInfo(command.Id, ids[index], x).Generate());
                    }

                    result.Add(team);
                }

                return result;
            }
        }

        public static class ServerBrowser
        {
            public static Faker<PostServerRequest> GetPostServerRequest()
            {
                return new Faker<PostServerRequest>()
                    .StrictMode(true)
                    .RuleFor(x => x.Port, x => x.Random.Int(0, 65535))
                    .RuleFor(x => x.HasPassword, x => x.Random.Bool(0.5f))
                    .RuleFor(x => x.Name, x => x.Internet.UserName() + "'s Game")
                    .RuleFor(x => x.Type, x => x.PickRandom<MatchTypeDto>())
                    .RuleFor(x => x.Mods, x => x.Random.String())
                    .RuleFor(x => x.Players, MakeValidPlayerData);
            }

            public static IPAddress GetIP()
            {
                return IPAddress.Parse($"{Random.Next(1,254)}.{Random.Next(1,254)}.{Random.Next(1,254)}.{Random.Next(1,254)}");
            }

            public static Faker<ServerPlayerInfoResult> GetPlayerInfoResult()
            {
                return new Faker<ServerPlayerInfoResult>()
                    .StrictMode(true)
                    .RuleFor(x => x.Name, x => x.Internet.UserName())
                    .RuleFor(x => x.Latency, x => x.Random.Int(0, 180));
            }

            private static List<ServerPlayerInfoResult> MakeValidPlayerData(Faker faker, PostServerRequest request)
            {
                var playerFaker = GetPlayerInfoResult();
                var numPlayers  = faker.Random.Int(0, 8);
                var result      = new List<ServerPlayerInfoResult>();

                for (int x = 0; x < numPlayers; x++)
                    result.Add(playerFaker.Generate());

                return result;
            }
        }
        
        #region Tools

        /// <summary>
        /// Gets an enum outside its value range.
        /// </summary>
        public static TEnum GetEnumOutsideRange<TEnum>() where TEnum : struct, Enum
        {
            var min = Enums.ToInt32(Enums.GetValues<TEnum>().First());
            var max = Enums.ToInt32(Enums.GetValues<TEnum>().Last());
            var random = GetRandomNumberOutsideRange(min, max);
            return Enums.ToObject<TEnum>(random);
        }

        /// <summary>
        /// Gets a random number outside a given range.
        /// </summary>
        /// <param name="min">Min value (inclusive)</param>
        /// <param name="max">Max value (exclusive)</param>
        /// <param name="absoluteMax">Absolute max value that can be generated (exclusive).</param>
        /// <param name="absoluteMin">Absolute minimum value that can be generated.</param>
        /// <returns></returns>
        public static int GetRandomNumberOutsideRange(int min, int max, int absoluteMin = 0, int absoluteMax = int.MaxValue)
        {
            var minResult = Random.Next(absoluteMin, min);
            var maxResult = Random.Next(max + 1, absoluteMax);
            var returnMin = Convert.ToBoolean(Random.Next(0, 1));
            return returnMin ? minResult : maxResult;
        }

        /// <summary>
        /// Gets a list of unique integers between the specified min and max value.
        /// Note: Optimized for tiny collections only.
        /// </summary>
        /// <param name="min">Inclusive</param>
        /// <param name="max">Exclusive</param>
        /// <param name="number">Number of IDs to generate.</param>
        private static List<int> GetUniqueIntegers(int min, int max, int number)
        {
            var ids = new List<int>(number);
            while (ids.Count < number)
            {
                var random = Random.Next(min, max);
                if (!ids.Contains(random))
                    ids.Add(random);
            }

            return ids;
        }

        #endregion
    }
}
