using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.SeedData
{
	public static class SeedRoles
	{
		public static void SeedRole(this ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Role>()
				.HasData
				(
					new Role() { Id = 1, RoleName = "Admin" },
					new Role() { Id = 2, RoleName = "User" }
				);
		}
	}
}
