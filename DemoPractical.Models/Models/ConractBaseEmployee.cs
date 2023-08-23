using System.ComponentModel.DataAnnotations.Schema;

namespace DemoPractical.Models.Models
{
	public class ConractBaseEmployee
	{
		[ForeignKey("Employee")]
		public int EmployeeID { get; set; }

		public int HourlyPaid { get; set; }

		// Navigation Properties

		public Employee Employee { get; set; }
	}
}
