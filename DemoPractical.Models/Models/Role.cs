using System.ComponentModel.DataAnnotations;

namespace DemoPractical.Models.Models
{
	public class Role
	{
		[Key]
		public int Id { get; set; }

		public string RoleName { get; set; } = null!;
	}
}
