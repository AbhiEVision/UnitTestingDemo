using DemoPractical.DataAccessLayer.Data;
using DemoPractical.DataAccessLayer.Repositories;
using DemoPractical.Models.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.XUnitTest.Test.Repositories
{
	public class DepartmentRepositoryTest
	{
		public async Task<ApplicationDataContext> GetDataContext()
		{
			var options = new DbContextOptionsBuilder<ApplicationDataContext>()
				.UseInMemoryDatabase("TempDatabase")
				.Options;

			var databaseContext = new ApplicationDataContext(options);

			await databaseContext.Database.EnsureCreatedAsync();

			if (await databaseContext.Departments.CountAsync() <= 0)
			{
				databaseContext.Departments.AddRange(new Department()
				{
					Id = 1,
					DepartmentName = "HR"
				},
				new Department()
				{
					Id = 2,
					DepartmentName = "JAVA"
				},
				new Department()
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

			await databaseContext.SaveChangesAsync();

			return databaseContext;

		}

		[Fact]
		public async Task DepartmentRepository_GetDepartmentByIdAsync_ReturnsDepartment()
		{
			// Arrange
			var databaseContext = await GetDataContext();
			var repository = new DepartmentRepository(databaseContext);
			int departmentId = 1;
			string expectedDepatmentName = "HR";

			// Act
			var result = await repository.GetDepartmentByIdAsync(departmentId);


			// Assert
			result.Should().NotBeNull();
			result.Should().BeOfType<Department>();
			result.DepartmentName.Should().Be(expectedDepatmentName);
		}

		[Fact]
		public async Task DepartmentRepository_IsDepartmentExists_ReturnsTrue()
		{
			// Arrange
			var databaseContext = await GetDataContext();
			var repository = new DepartmentRepository(databaseContext);
			int departmentId = 1;

			// Act
			var result = await repository.IsDepartmentExists(departmentId);


			// Assert
			result.Should().Be(true);
		}

		[Fact]
		public async Task DepartmentRepository_IsDepartmentExists_ReturnsFalse()
		{
			// Arrange
			var databaseContext = await GetDataContext();
			var repository = new DepartmentRepository(databaseContext);
			int departmentId = 10;

			// Act
			var result = await repository.IsDepartmentExists(departmentId);


			// Assert
			result.Should().Be(false);
		}

	}
}
