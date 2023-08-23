using DemoPractical.Models.Models;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	public class RoleValidation : AbstractValidator<Role>
	{
		public RoleValidation()
		{
			RuleFor(x => x.RoleName)
				.NotNull()
				.NotEmpty()
				.Length(1, 50)
				.WithMessage("Role name length must be between 1 to 50");
		}

	}
}
