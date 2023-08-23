using DemoPractical.API.Controllers.V2;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.Models;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace DemoPractical.XUnitTest.Test.NewFolder
{
	public class DepartmentControllerTest
	{
		private readonly DepartmentController _departmentController;
		private readonly IDepartmentRepository _repository;

		public DepartmentControllerTest()
		{
			// Dependencies
			_repository = A.Fake<IDepartmentRepository>();

			//SUT
			_departmentController = new DepartmentController(_repository);

		}

		[Fact]
		public async Task DepartmentController_GetDepartment_ReturnOkObjectResult_FakeItEasy()
		{
			// Arrange
			int departmentId = 1;
			A.CallTo(() => _repository.GetDepartmentByIdAsync(departmentId)).Returns(new Department());

			// Act
			var result = await _departmentController.GetDepartment(departmentId);

			// Assertion
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

		[Fact]
		public async Task DepartmentController_CreateDepartment_ReturnOkObjectResult_FakeItEasy()
		{
			// Arrange
			var department = A.Fake<Department>();
			var departmentController = new DepartmentController(_repository);
			A.CallTo(() => _repository.AddDepartment(department));


			// Act
			var result = await departmentController.CreateDepartment(department);

			//Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));

		}

		[Fact]
		public async Task DepartmentController_CreateDepartment_ReturnOkObjectResult_Mock()
		{
			// Arrange
			var department = new Mock<Department>();
			var departmentRepository = new Mock<IDepartmentRepository>();
			departmentRepository.Setup(x => x.AddDepartment(department.Object));
			var departmentController = new DepartmentController(departmentRepository.Object);

			// Act
			var result = await departmentController.CreateDepartment(department.Object);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));


		}

		[Fact]
		public async Task DepartmentController_GetDepartment_ReturnOkObjectResult_Mock()
		{
			// Arrange
			var departmentId = 1;
			var departmentRepository = new Mock<IDepartmentRepository>();

			departmentRepository.Setup(x => x.GetDepartmentByIdAsync(departmentId)).Returns(Task.FromResult(new Department()));
			var departmentController = new DepartmentController(departmentRepository.Object);

			// Act
			var result = await departmentController.GetDepartment(departmentId);

			// Assertion
			result.Should().NotBeNull();
			result.Should().BeOfType(typeof(OkObjectResult));
		}

	}
}
