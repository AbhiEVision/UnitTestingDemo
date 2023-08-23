using System.ComponentModel.DataAnnotations.Schema;

namespace DemoPractical.Models.Models
{
	public class PermentEmployee
	{
		[ForeignKey("Employee")]
		public int EmployeeId { get; set; }

		public int Salary { get; set; }

		// Navigation Properties

		public Employee Employee { get; set; }
	}
}
