using DemoPractical.Models.Models;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	/// <summary>
	/// <type> Fluent validation </type>
	/// This class is validating the Employee details before invoking the controller
	/// </summary>
	public class EmployeeValidation : AbstractValidator<Employee>
	{
		public EmployeeValidation()
		{
			// Name validation
			RuleFor(x => x.Name)
				.NotNull()
				.Length(3, 50)
				.WithMessage("Length must between the 3 to 50")
				.Matches(@"^([a-zA-z ]){1,50}")
				.WithMessage("Name only contains the Alphabets.");

			// Email validation
			RuleFor(x => x.Email)
				.NotNull()
				.Length(3, 256)
				.WithMessage("Email length must be between the 3 to 256")
				.EmailAddress()
				.WithMessage("Please Enter the valid Email Address");

			// Phone Number Validation
			RuleFor(x => x.PhoneNumber)
				.NotNull()
				.Length(10, 10)
				.WithMessage("Phone number must be length of 10")
				.Matches("^(\\+\\d{1,2}\\s?)?\\(?\\d{3}\\)?[\\s.-]?\\d{3}[\\s.-]?\\d{4}$")
				.WithMessage("Please enter the valid phone number");

			// Password validation
			RuleFor(x => x.Password)
				.NotNull()
				.MinimumLength(3)
				.WithMessage("Password must Length of 3");

			// Department Id validation
			RuleFor(x => x.DepartmentId)
				.NotNull();

			// Employee type validation
			RuleFor(x => x.EmployeeTypeId)
				.NotNull();
		}
	}
}
