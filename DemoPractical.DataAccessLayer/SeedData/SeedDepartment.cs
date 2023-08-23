using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.SeedData
{
	internal static class SeedDepartment
	{
		public static void SeedDepartments(this ModelBuilder builder)
		{
			builder.Entity<Department>()
				.HasData(
					new Department()
					{
						Id = 1,
						DepartmentName = "HR"
					}, new Department()
					{
						Id = 2,
						DepartmentName = "JAVA"
					}, new Department()
					{
						Id = 3,
						DepartmentName = "DOTNET"
					},
					new Department()
					{
						Id = 4,
						DepartmentName = "IT"
					});
		}
	}
}
