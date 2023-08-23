namespace DemoPractical.Models.DTOs
{
	public class EmployeeSalaryDetails
	{
		public string EmployeeName { get; set; } = null!;

		public string EmployeeEmail { get; set; } = null!;

		public string TypeOfEmployee { get; set; } = null!;

		public int? Salary { get; set; }

		public int? HourlyPaid { get; set; }
	}
}
