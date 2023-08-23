using DemoPractical.Models.Models;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	public class ContractBaseEmployeeValidate : AbstractValidator<ConractBaseEmployee>
	{
		public ContractBaseEmployeeValidate()
		{
			RuleFor(x => x.EmployeeID)
				.NotNull()
				.NotEmpty();

			RuleFor(x => x.HourlyPaid)
				.NotEmpty()
				.NotNull()
				.GreaterThanOrEqualTo(1)
				.WithMessage("Hourly paid must be grater than 0");
		}
	}
}
