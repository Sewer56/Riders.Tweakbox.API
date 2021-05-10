using FluentValidation;
using Riders.Tweakbox.API.Domain.Common;

namespace Riders.Tweakbox.API.Application.Commands.v1.Match.Validation
{
    public class PostMatchPlayerInfoValidator : AbstractValidator<PostMatchPlayerInfo>
    {
        public PostMatchPlayerInfoValidator()
        {
            RuleFor(x => x.Board).InclusiveBetween((byte)Constants.Race.MinGearNo, (byte)Constants.Race.MaxGearNo);
            RuleFor(x => x.Character).InclusiveBetween((byte)Constants.Race.MinCharacterNo, (byte)Constants.Race.MaxCharacterNo);
            RuleFor(x => x.FastestLapFrames).InclusiveBetween(-1, int.MaxValue);
            RuleFor(x => x.FinishTimeFrames).GreaterThanOrEqualTo(x => x.FastestLapFrames);
        }
    }
}
