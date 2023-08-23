using DemoPractical.Models.DTOs;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	public class EmailModelValidation : AbstractValidator<EmailModel>
	{
		public EmailModelValidation()
		{
			RuleFor(x => x.To)
				.NotNull()
				.NotEmpty()
				.EmailAddress()
				.WithMessage("Please enter the valid email address!");

			RuleFor(x => x.Subject)
				.NotNull()
				.NotEmpty()
				.Length(1, 256)
				.WithMessage("You can not enter more than 256 characters in subject");

			RuleFor(x => x.Body)
				.NotNull()
				.WithMessage("You must add something inside a body!")
				.Length(1, 50000)
				.WithMessage("You can not insert more than 50000 characters!");
		}
	}
}
