using FluentValidation;
using Riders.Tweakbox.API.Domain.Common;

namespace Riders.Tweakbox.API.Application.Commands.v1.User.Validation
{
    public class UserRegistrationRequestValidator : AbstractValidator<UserRegistrationRequest>
    {
        /// <inheritdoc />
        public UserRegistrationRequestValidator()
        {
            RuleFor(x => x.UserName).MaximumLength(Constants.User.UserNameMaxLength).WithMessage("Username exceeded maximum length.");
        }
    }
}
