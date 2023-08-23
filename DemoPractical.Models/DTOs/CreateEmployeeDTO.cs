namespace DemoPractical.Models.DTOs
{
	public class CreateEmployeeDTO
	{
		public string? Name { get; set; }

		public string? Email { get; set; }

		public string? Password { get; set; }

		public string? ConfirmPassword { get; set; }

		public string? PhoneNumber { get; set; }

		public int? DepartmentId { get; set; }

		public int RoleId { get; set; }

		public int? EmployeeTypeId { get; set; }

		public int? HourlyPaid { get; set; }

		public int? Salary { get; set; }


	}
}
