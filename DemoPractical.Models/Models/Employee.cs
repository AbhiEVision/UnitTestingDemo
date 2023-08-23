namespace DemoPractical.Models.Models
{
	public class Employee
	{
		public int Id { get; set; }

		public string? Name { get; set; }

		public string? Email { get; set; }

		public string? Password { get; set; }

		public string PhoneNumber { get; set; }

		public int? DepartmentId { get; set; }

		public int? EmployeeTypeId { get; set; }

		// Navigation Properties
		public EmployeeType? EmployeeType { get; set; }
		public Department? Department { get; set; }

	}
}