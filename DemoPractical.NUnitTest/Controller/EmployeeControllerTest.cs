using DemoPractical.API.Controllers.V1;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractical.NUnitTest.Controller
{
	[TestFixture]
	public class EmployeeControllerTest
	{
		private EmployeeController _employeeController;
		private IEmployeeRepository _employeeRepository;
		private IDepartmentRepository _departmentRepository;

		[SetUp]
		public void CreateFakeObjects()
		{
			_employeeRepository = A.Fake<IEmployeeRepository>();
			_departmentRepository = A.Fake<IDepartmentRepository>();
			_employeeController = new EmployeeController(_employeeRepository, _departmentRepository);
		}

		[Test]
		public async Task EmployeeController_GetEmployee_ShouldReturnEmployee()
		{
			// Arrange
			int employeeID = 1;
			A.CallTo(() => _employeeRepository.GetEmployeeByIdAsync(employeeID)).Returns(Task.FromResult(new Employee()));

			// Act
			var result = await _employeeController.GetEmployee(employeeID);

			// Assert
			result.Should().BeOfType<OkObjectResult>();

		}

		[Test]
		public async Task EmployeeController_GetEmployee_ShouldReturnBadRequest()
		{
			// Arrange
			int employeeID = 1;
			A.CallTo(() => _employeeRepository.GetEmployeeByIdAsync(employeeID)).Returns(Task.FromResult<Employee>(null));

			// Act
			var result = await _employeeController.GetEmployee(employeeID);

			// Assert
			result.Should().BeOfType(typeof(BadRequestObjectResult));
		}

		[Test]
		public async Task EmployeeController_CreateEmployee_ShouldReturnOkObjectResult()
		{
			// Arrange
			var employee = new CreateEmployeeDTO();
			A.CallTo(() => _employeeRepository.CreateEmployeeAsync(employee));
			A.CallTo(() => _employeeRepository.GetEmployeeByEmail(employee.Email)).Returns(Task.FromResult<Employee>(null));

			// Act
			var result = await _employeeController.CreateEmployee(employee);

			// Arrange
			result.Should().BeOfType<OkObjectResult>();

		}

		[Test]
		public async Task EmployeeController_CreateEmployee_ShouldReturnBadRequestObjectResult()
		{
			// Arrange
			var employee = new CreateEmployeeDTO();
			A.CallTo(() => _employeeRepository.CreateEmployeeAsync(employee));
			A.CallTo(() => _employeeRepository.GetEmployeeByEmail(employee.Email)).Returns(Task.FromResult(new Employee()));

			// Act
			var result = await _employeeController.CreateEmployee(employee);

			// Arrange
			result.Should().BeOfType<BadRequestObjectResult>();
		}

	}
}
