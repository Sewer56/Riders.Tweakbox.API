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
using Riders.Tweakbox.API.Domain.Common;

namespace Application.Tests.Integrity.Helpers
{
    public static class DataGenerators
    {
        public static Random Random = new Random();

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
        /// <returns></returns>
        public static int GetRandomNumberOutsideRange(int min, int max, int absoluteMax = int.MaxValue)
        {
            var minResult = Random.Next(int.MinValue, min);
            var maxResult = Random.Next(max + 1, absoluteMax);
            var returnMin = Convert.ToBoolean(Random.Next(0, 1));
            return returnMin ? minResult : maxResult;
        }

        public static Faker<GetMatchResult> GetMatchCommandFaker()
        {
            return new Faker<GetMatchResult>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => x.IndexFaker)
                .RuleFor(x => x.CompletedTime, () => DateTime.UtcNow)
                .RuleFor(x => x.MatchType, x => x.PickRandom<MatchTypeDto>())
                .RuleFor(x => x.StageNo, x => x.Random.Int(Constants.Race.MinStageNo, Constants.Race.MaxStageNo))
                .RuleFor(x => x.Teams, MakeValidTeamData);
        }

        public static Faker<GetMatchPlayerInfo> GetPlayerInfoFaker()
        {
            int idFaker = 0;
            int matchIdFaker = 0;
            int playerIdFaker = 0;
            return new Faker<GetMatchPlayerInfo>()
                .StrictMode(true)
                .RuleFor(x => x.Id, x => idFaker++)
                .RuleFor(x => x.MatchId, x => matchIdFaker++)
                .RuleFor(x => x.PlayerId, x => playerIdFaker++)
                .RuleFor(x => x.Board, x => x.Random.Byte(Constants.Race.MinGearNo, Constants.Race.MaxGearNo))
                .RuleFor(x => x.Character, x => x.Random.Byte(Constants.Race.MinCharacterNo, Constants.Race.MaxCharacterNo))
                .RuleFor(x => x.FastestLapFrames, x => x.Random.Int(-1, 3600))
                .RuleFor(x => x.FinishTimeFrames, x => x.Random.Int(3600, 10800))
                .FinishWith((faker, info) =>
                {
                    if (info.FastestLapFrames == -1)
                        info.FinishTimeFrames = -1;
                });
        }

        public static List<List<GetMatchPlayerInfo>> MakeValidTeamData(Faker faker, GetMatchResult command)
        {
            var infoFaker   = GetPlayerInfoFaker();
            var teamCount   = command.MatchType.GetNumTeams();
            var playerCount = command.MatchType.GetNumPlayersPerTeam();
            var result      = new List<List<GetMatchPlayerInfo>>(teamCount);

            for (int x = 0; x < teamCount; x++)
            {
                var team = new List<GetMatchPlayerInfo>();
                for (int y = 0; y < playerCount; y++)
                    team.Add(infoFaker.Generate());

                result.Add(team);
            }

            return result;
        }

        public static Faker<PostServerRequest> GetPostServerRequestFaker()
        {
            return new Faker<PostServerRequest>()
                .StrictMode(true)
                .RuleFor(x => x.Port, x => x.Random.Int(0, 65535))
                .RuleFor(x => x.Name, x => x.Internet.UserName() + "'s Game")
                .RuleFor(x => x.Type, x => x.PickRandom<MatchTypeDto>())
                .RuleFor(x => x.Players, MakeValidPlayerData);
        }

        public static Faker<ServerPlayerInfoResult> GetPlayerInfoResultFaker()
        {
            return new Faker<ServerPlayerInfoResult>()
                .StrictMode(true)
                .RuleFor(x => x.Name, x => x.Internet.UserName())
                .RuleFor(x => x.Latency, x => x.Random.Int(0, 180));
        }

        public static IPAddress GetRandomIP()
        {
            return IPAddress.Parse($"{Random.Next(1,254)}.{Random.Next(1,254)}.{Random.Next(1,254)}.{Random.Next(1,254)}");
        }

        private static List<ServerPlayerInfoResult> MakeValidPlayerData(Faker faker, PostServerRequest request)
        {
            var playerFaker = GetPlayerInfoResultFaker();
            var numPlayers  = faker.Random.Int(0, 8);
            var result      = new List<ServerPlayerInfoResult>();

            for (int x = 0; x < numPlayers; x++)
                result.Add(playerFaker.Generate());

            return result;
        }
    }
}
