using DemoPractical.Models.DTOs;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	public class LoginValidation : AbstractValidator<LoginDTO>
	{
		public LoginValidation()
		{
			RuleFor(x => x.Email)
				.NotNull()
				.NotEmpty()
				.EmailAddress()
				.WithMessage("Please enter valid email address");

			RuleFor(x => x.Password)
				.NotNull()
				.NotEmpty()
				.MinimumLength(3)
				.WithMessage("Please length must be grater than 3");

		}
	}
}
