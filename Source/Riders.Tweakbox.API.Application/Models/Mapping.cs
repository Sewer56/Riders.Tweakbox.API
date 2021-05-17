using System.Collections.Generic;
using FastExpressionCompiler;
using Mapster;
using MapsterMapper;
using Riders.Tweakbox.API.Application.Commands.v1;
using Riders.Tweakbox.API.Application.Commands.v1.Match;
using Riders.Tweakbox.API.Application.Commands.v1.Match.Result;
using Riders.Tweakbox.API.Domain.Models.Database;

namespace Riders.Tweakbox.API.Application.Models
{
    /// <summary>
    /// Static class containing all inter-class mappings that cannot be automatically mapped.
    /// </summary>
    public static class Mapping
    {
        public static readonly IMapper Mapper;

        static Mapping()
        {
            TypeAdapterConfig.GlobalSettings.Compiler = exp => exp.CompileFast();
            var typeAdapterConfig = new TypeAdapterConfig();
            typeAdapterConfig.Compiler = exp => exp.CompileFast(); 

            // Mapping Rules
            typeAdapterConfig.NewConfig<GetMatchResult, Match>().AfterMapping(GetFlattenTeamData);
            typeAdapterConfig.NewConfig<PostMatchRequest, Match>().AfterMapping(PostFlattenTeamData);
            typeAdapterConfig.NewConfig<Match, GetMatchResult>().AfterMapping(GetGroupTeamData);
            typeAdapterConfig.NewConfig<Match, PostMatchRequest>().AfterMapping(PostGroupTeamData);

            Mapper = new Mapper(typeAdapterConfig);
        }

        // Efficiently flatten and de-flatten.
        private static void GetGroupTeamData(Match match, GetMatchResult command)
        {
            var result          = new List<List<GetMatchPlayerInfo>>();
            int playersPerTeam  = command.MatchType.GetNumPlayersPerTeam();
            var team            = new List<GetMatchPlayerInfo>();

            for (int x = 0; x < match.Players.Count; x++)
            {
                team.Add(Mapper.Map<GetMatchPlayerInfo>(match.Players[x]));
                if (team.Count == playersPerTeam)
                {
                    result.Add(team);
                    team = new List<GetMatchPlayerInfo>();
                }
            }

            if (team.Count > 0)
                result.Add(team);

            command.Teams = result;
        }

        private static void PostGroupTeamData(Match match, PostMatchRequest request)
        {
            var result          = new List<List<PostMatchPlayerInfo>>();
            int playersPerTeam  = request.MatchType.GetNumPlayersPerTeam();
            var team            = new List<PostMatchPlayerInfo>();

            for (int x = 0; x < match.Players.Count; x++)
            {
                team.Add(Mapper.Map<PostMatchPlayerInfo>(match.Players[x]));
                if (team.Count == playersPerTeam)
                {
                    result.Add(team);
                    team = new List<PostMatchPlayerInfo>();
                }
            }
            
            if (team.Count > 0)
                result.Add(team);

            request.Teams = result;
        }

        private static void PostFlattenTeamData(PostMatchRequest request, Match match)
        {
            var result = new List<PlayerRaceDetails>();

            for (var x = 0; x < request.Teams.Count; x++)
            {
                var team = request.Teams[x];
                foreach (var playerDetails in team)
                {
                    playerDetails.TeamNo = x;
                    result.Add(Mapper.Map<PlayerRaceDetails>(playerDetails));
                }
            }

            match.Players = result;
        }

        private static void GetFlattenTeamData(GetMatchResult command, Match match)
        {
            var result = new List<PlayerRaceDetails>();
            for (var x = 0; x < command.Teams.Count; x++)
            {
                var team = command.Teams[x];
                foreach (var playerDetails in team)
                {
                    playerDetails.TeamNo = x;
                    result.Add(Mapper.Map<PlayerRaceDetails>(playerDetails));
                }
            }

            match.Players = result;
        }
    }
}
