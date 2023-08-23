using DemoPractical.Models.Models;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	/// <summary>
	/// <type> Fluent validation </type>
	/// This class is validating the Permanent Employee Details before the invoking the controller
	/// </summary>
	public class PermanentEmployeeValidation : AbstractValidator<PermentEmployee>
	{
		public PermanentEmployeeValidation()
		{
			// Id can not be null
			RuleFor(x => x.EmployeeId)
				.NotEmpty()
				.NotNull();

			// Salary Validations 
			RuleFor(x => x.Salary)
				.NotEmpty()
				.NotNull()
				.GreaterThanOrEqualTo(1)
				.WithMessage("Salary must be grater than 0");

		}
	}
}
