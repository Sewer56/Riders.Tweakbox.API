using FluentValidation;
using Riders.Tweakbox.API.Application.Commands.v1.Browser.Result;

namespace Riders.Tweakbox.API.Application.Commands.v1.Browser.Validation
{
    public class GetServerResultValidator : AbstractValidator<GetServerResult>
    {
        /// <inheritdoc />
        public GetServerResultValidator()
        {
            RuleFor(x => x.Port).InclusiveBetween(1, ushort.MaxValue).WithMessage("Invalid port number");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Name is too short");
            RuleFor(x => x.Name).MaximumLength(64).WithMessage("Name it too long");
            RuleFor(x => x.Players).NotNull().WithMessage("Player data is null");
        }
    }
}
