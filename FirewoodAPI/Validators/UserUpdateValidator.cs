using FirewoodAPI.DTOs;
using FluentValidation;

namespace FirewoodAPI.Validators
{
	public class UserUpdateValidator : AbstractValidator<UserUpdateDto>
	{
		public UserUpdateValidator()
		{
			RuleFor(x => x.FullName).NotEmpty().WithMessage("Fullname must not be empty").MinimumLength(12).WithMessage("FullName should has a minimun of 12 characters");

			RuleFor(x => x.Email).NotEmpty().WithMessage("Email must not be empty").EmailAddress().WithMessage("Email not valid");

			RuleFor(x => x.Password).NotEmpty().MinimumLength(8).WithMessage("Password should contains at least 8 characters");
		}
	}
}
