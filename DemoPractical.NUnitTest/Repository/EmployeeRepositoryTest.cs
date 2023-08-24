using DemoPractical.DataAccessLayer.Data;
using DemoPractical.DataAccessLayer.Repositories;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.NUnitTest.Repository
{
	public class EmployeeRepositoryTest
	{
		private IEmployeeRepository _employeeRepository;


		[SetUp]
		public async Task CreateEmployeeRepository()
		{
			// Creating options
			var databaseContextOptions = new DbContextOptionsBuilder<ApplicationDataContext>()
				.UseInMemoryDatabase("TempDatabase")
				.Options;

			ApplicationDataContext dbContext = new ApplicationDataContext(databaseContextOptions);

			await dbContext.Database.EnsureCreatedAsync();

			// Adding Departments
			if (await dbContext.Departments.CountAsync() <= 0)
			{
				dbContext.Departments.AddRange(new Department()
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
				await dbContext.SaveChangesAsync();
			}

			// Adding Employee Types
			if (await dbContext.EmployeeTypes.CountAsync() <= 0)
			{
				dbContext.EmployeeTypes.AddRange(
					new EmployeeType()
					{
						Id = 1,
						Type = "Permanent"
					},
					new EmployeeType()
					{
						Id = 2,
						Type = "Contract"

					});
				await dbContext.SaveChangesAsync();

			}

			// Adding Role Types
			if (await dbContext.Roles.CountAsync() <= 0)
			{
				dbContext.Roles.AddRange(
					new Role() { Id = 1, RoleName = "Admin" },
					new Role() { Id = 2, RoleName = "User" }
				);
				await dbContext.SaveChangesAsync();
			}

			// Adding Employee and its types
			if (await dbContext.Employees.CountAsync() <= 0)
			{
				dbContext.Employees.AddRange(
					new Employee()
					{
						Id = 1,
						DepartmentId = 1,
						Email = "test@gmail.com",
						EmployeeTypeId = 1,
						Name = "testUser",
						Password = "test@123",
						PhoneNumber = "1231231231"
					},
						new Employee()
						{
							Id = 2,
							DepartmentId = 1,
							Email = "test1@gmail.com",
							EmployeeTypeId = 2,
							Name = "testUser",
							Password = "test@123",
							PhoneNumber = "1231231231"
						});
				await dbContext.SaveChangesAsync();

				dbContext.PermentEmployees.AddRange(
					new PermentEmployee()
					{
						EmployeeId = 1,
						Salary = 650000
					});
				await dbContext.SaveChangesAsync();

				dbContext.ConractBaseEmployees.AddRange(
					new ConractBaseEmployee()
					{
						EmployeeID = 2,
						HourlyPaid = 1000
					});
				await dbContext.SaveChangesAsync();
			}

			var departmentRepository = A.Fake<IDepartmentRepository>();

			_employeeRepository = new EmployeeRepository(dbContext, departmentRepository);
		}

		[Test]
		[TestCase(1)]
		public async Task EmployeeRepository_GetEmployeeByIdAsync_ReturnEmployee(int employeeID)
		{
			// Arrange
			Employee expectedData = new Employee()
			{
				Id = 1,
				DepartmentId = 1,
				Email = "test@gmail.com",
				EmployeeTypeId = 1,
				Name = "testUser",
				Password = "test@123",
				PhoneNumber = "1231231231"
			};

			// Act
			var result = await _employeeRepository.GetEmployeeByIdAsync(employeeID);

			// Arrange
			result.Should().NotBeNull();
			result.Should().BeOfType<Employee>();
			result.Email.Should().Be(expectedData.Email);
		}

		[Test]
		public async Task EmployeeRepository_CreateEmployee_ReturnNothing()
		{
			// Arrange
			CreateEmployeeDTO employeeDTO = new CreateEmployeeDTO()
			{
				Name = "Test",
				Email = "Test123@gmail.com",
				ConfirmPassword = "HEHE",
				Password = "HEHE",
				DepartmentId = 1,
				EmployeeTypeId = 1,
				Salary = 10000,
				PhoneNumber = "1231223123",
				RoleId = 1,
			};


			// Act && Assert
			Assert.DoesNotThrowAsync(async () => await _employeeRepository.CreateEmployeeAsync(employeeDTO));


			var temp = async () => await _employeeRepository.CreateEmployeeAsync(employeeDTO);

			await temp.Should().NotThrowAsync();
		}
	}
}
