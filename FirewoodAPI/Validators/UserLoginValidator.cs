using FirewoodAPI.DTOs;
using FluentValidation;

namespace FirewoodAPI.Validators
{
	public class UserLoginValidator : AbstractValidator<UserLoginDto>
	{
        public UserLoginValidator()
        {
			RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be empty").EmailAddress().WithMessage("Email not valid");
			RuleFor(x => x.Password).NotEmpty().WithMessage("Password should not be empty");
		}
    }
}
