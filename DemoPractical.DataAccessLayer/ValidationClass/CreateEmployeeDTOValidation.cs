using DemoPractical.Models.DTOs;
using FluentValidation;

namespace DemoPractical.DataAccessLayer.ValidationClass
{
	public class CreateEmployeeDTOValidation : AbstractValidator<CreateEmployeeDTO>
	{
		public CreateEmployeeDTOValidation()
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

			// Password validation
			RuleFor(x => x.ConfirmPassword)
				.NotNull()
				.MinimumLength(3)
				.WithMessage("Password must Length of 3")
				.Equal(x => x.Password)
				.WithMessage("Password and Confirm Password must be same");

			// Department Id validation
			RuleFor(x => x.DepartmentId)
				.NotNull();

			// Employee Role Id
			RuleFor(x => x.RoleId)
				.NotNull()
				.NotEmpty()
				.GreaterThan(0)
				.WithMessage("Role id must be grater than 0")
				.LessThan(3)
				.WithMessage("Role id must be less than 3");


			// Employee type validation
			RuleFor(x => x.EmployeeTypeId)
				.NotNull()
				.InclusiveBetween(1, 2);

			// Conditional Type's
			When(x => x.EmployeeTypeId == 1, () =>
			{
				RuleFor(x => x.Salary)
					.NotNull()
					.GreaterThan(0);
				RuleFor(x => x.HourlyPaid)
					.Null()
					.WithMessage("While entering the details of the permanent employee Hourly based salary must be null");
			})
			.Otherwise(() =>
			{
				RuleFor(x => x.HourlyPaid)
					.NotNull()
					.GreaterThan(0);
				RuleFor(x => x.Salary)
					.Null()
					.WithMessage("While Entering the details of the details of contract base employee Salary field must be null");
			});


		}
	}
}
