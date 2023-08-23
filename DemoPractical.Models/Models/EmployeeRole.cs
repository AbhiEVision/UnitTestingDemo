using System.ComponentModel.DataAnnotations.Schema;

namespace DemoPractical.Models.Models
{
	public class EmployeeRole
	{
		[ForeignKey("Employee")]
		public int EmployeeId { get; set; }

		[ForeignKey("Role")]
		public int RoleId { get; set; }

		public Role Role { get; set; }

		public Employee Employee { get; set; }
	}
}
