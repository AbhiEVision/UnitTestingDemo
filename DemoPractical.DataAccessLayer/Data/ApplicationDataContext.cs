using DemoPractical.DataAccessLayer.SeedData;
using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Data
{
	public class ApplicationDataContext : DbContext
	{
		public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) : base(options)
		{

		}

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Department> Departments { get; set; }

		public DbSet<EmployeeType> EmployeeTypes { get; set; }

		public DbSet<PermentEmployee> PermentEmployees { get; set; }

		public DbSet<ConractBaseEmployee> ConractBaseEmployees { get; set; }

		public DbSet<Role> Roles { get; set; }

		public DbSet<EmployeeRole> EmployeeRoles { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ConractBaseEmployee>().HasKey(x => x.EmployeeID);
			modelBuilder.Entity<PermentEmployee>().HasKey(x => x.EmployeeId);
			modelBuilder.Entity<EmployeeRole>().HasKey(x => new { x.RoleId, x.EmployeeId });

			modelBuilder.Entity<Department>().HasMany<Employee>().WithOne(x => x.Department).HasForeignKey(x => x.DepartmentId).OnDelete(DeleteBehavior.SetNull);

			// Seed Data
			modelBuilder.SeedDepartments();
			modelBuilder.SeedEmployeeTypes();
			modelBuilder.SeedRole();
		}

	}
}
