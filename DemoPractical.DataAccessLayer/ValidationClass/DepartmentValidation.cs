using DemoPractical.Models.Models;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	/// <summary>
	/// <type> Fluent validation </type>
	/// This class is validating the Department details before invoking the controller
	/// </summary>
	public class DepartmentValidation : AbstractValidator<Department>
	{
		public DepartmentValidation()
		{
			// Department name validations
			RuleFor(x => x.DepartmentName)
				.NotNull()
				.NotEmpty()
				.Length(1, 50);

		}
	}
}
