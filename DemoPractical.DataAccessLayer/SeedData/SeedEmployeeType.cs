using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.SeedData
{
	internal static class SeedEmployeeType
	{
		public static void SeedEmployeeTypes(this ModelBuilder builder)
		{
			builder.Entity<EmployeeType>()
				.HasData
				(
					new EmployeeType()
					{
						Id = 1,
						Type = "Permanent"
					},
					new EmployeeType()
					{
						Id = 2,
						Type = "Contract"
					}
				);
		}
	}
}
