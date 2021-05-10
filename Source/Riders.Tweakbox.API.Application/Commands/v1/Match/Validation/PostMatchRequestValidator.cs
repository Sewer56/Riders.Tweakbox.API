using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Riders.Tweakbox.API.Application.Models;
using Riders.Tweakbox.API.Domain.Common;

namespace Riders.Tweakbox.API.Application.Commands.v1.Match.Validation
{
    public class PostMatchRequestValidator : AbstractValidator<PostMatchRequest>
    {
        private string _badPlayerInfoMessage;

        public PostMatchRequestValidator()
        {
            CascadeMode = CascadeMode.Stop;
            RuleFor(x => x.MatchType).IsInEnum().WithMessage("Invalid Match Type.");
            RuleFor(x => x.StageNo).InclusiveBetween((byte) Constants.Race.MinStageNo, (byte) Constants.Race.MaxStageNo).WithMessage($"Invalid Stage Number. Must be between {Constants.Race.MinStageNo} and {Constants.Race.MaxStageNo}");
            RuleFor(x => x.CompletedTime).LessThan(command => DateTime.UtcNow).WithMessage(x => $"Are you a time traveller? You completed a match in the future. Completed time: {x.CompletedTime}, Now: {DateTime.UtcNow}");
            RuleFor(x => x.Teams).NotNull().WithMessage("Must have more than 0 teams. (Teams is null)");
            RuleFor(x => x.Teams.Count).Equal(x => x.MatchType.GetNumTeams()).WithMessage("Incorrect number of teams for match type.");
            RuleFor(x => x.Teams).Must(HaveTheRightMemberCount).WithMessage("Incorrect player count in team.");
            RuleFor(x => x.Teams).Must(BeValid).WithMessage($"Incorrect player info. {_badPlayerInfoMessage}");
        }

        private bool BeValid(List<List<PostMatchPlayerInfo>> teams)
        {
            var validator = Validator.Get<PostMatchPlayerInfo>();

            foreach (var team in teams)
            foreach (var player in team)
            {
                var result = validator.Validate(player);
                if (result.IsValid) 
                    continue;

                _badPlayerInfoMessage = result.ToString();
                return false;
            }

            return true;
        }

        private bool HaveTheRightMemberCount(PostMatchRequest request, List<List<PostMatchPlayerInfo>> teams)
        {
            int playersPerTeam = request.MatchType.GetNumPlayersPerTeam();
            return teams.All(team => playersPerTeam == team.Count);
        }
    }
}
