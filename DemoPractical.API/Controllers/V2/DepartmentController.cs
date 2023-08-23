using DemoPractical.API.Attributes;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractical.API.Controllers.V2
{
	[Log]
	[ApiVersion("2.0")]
	[EndpointGroupName("v2")]
	[Route("api/v{version:apiVersion}/[controller]/[action]")]
	[ApiController]
	public class DepartmentController : ControllerBase
	{
		private IDepartmentRepository _departmentRepository;

		public DepartmentController(IDepartmentRepository departmentRepository)
		{
			_departmentRepository = departmentRepository;
		}

		/// <summary>
		/// Give all departments
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		[ResponseCache(Duration = 100)]
		public async Task<IActionResult> GetDepartments()
		{
			var departments = await _departmentRepository.GetDepartmentsAsync();

			if (departments == null || departments.Count() == 0)
			{
				return Ok("No departments found!");
			}

			return Ok(departments);
		}

		/// <summary>
		/// Give department by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseCache(Duration = 100)]
		public async Task<IActionResult> GetDepartment(int id)
		{
			if (id == null || id == 0 || id < -1)
			{
				return BadRequest("Invalid ID");
			}
			var department = await _departmentRepository.GetDepartmentByIdAsync(id);
			if (department == null)
			{
				return BadRequest("No Such Data found!");
			}
			return Ok(department);
		}

		/// <summary>
		/// Create new Department 
		/// </summary>
		/// <param name="department"></param>
		/// <returns></returns>
		[Log]
		[HttpPost]
		public async Task<IActionResult> CreateDepartment(Department department)
		{
			if (department == null)
			{
				return BadRequest("Please Enter the valid data");
			}
			try
			{
				await _departmentRepository.AddDepartment(department);
				return Ok($"Department Crated: {department.DepartmentName}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Delete the existing the department
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteDepartment(int id)
		{
			if (id == null || id == 0 || id < -1)
			{
				return BadRequest("Please enter the valid data");
			}
			Department department = await _departmentRepository.GetDepartmentByIdAsync(id);
			if (department == null)
			{
				return NotFound("Department is not found!");
			}
			try
			{
				await _departmentRepository.DeleteDepartment(id);
				return Ok($"'{department.DepartmentName}' department deleted successfully");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Update the existing department 
		/// </summary>
		/// <param name="department"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> UpdateDepartment(Department department)
		{
			if (department == null)
			{
				return BadRequest("Please enter the data");
			}

			Department dep = await _departmentRepository.GetDepartmentByIdAsync(department.Id);

			if (dep == null)
			{
				return BadRequest("Requested data is not available!");
			}
			string oldName = dep.DepartmentName;

			try
			{
				await _departmentRepository.EditDepartment(department);
				return Ok($"Department has been Updated {oldName} to {department.DepartmentName.ToUpper()}");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Gives employee list according to the departmentId
		/// </summary>
		/// <param name="departmentID"></param>
		/// <returns></returns>
		[HttpGet]
		[ResponseCache(Duration = 100)]
		public async Task<IActionResult> GiveEmployeeByDepartment(int departmentID)
		{
			if (departmentID == null || departmentID < 1)
			{
				return BadRequest("Please enter the valid data!");
			}

			Department department = await _departmentRepository.GetDepartmentByIdAsync(departmentID);

			if (department == null)
			{
				return BadRequest("No such Department found!");
			}

			var employeeList = await _departmentRepository.GetEmployeeOfDepartment(departmentID);

			if (employeeList == null || employeeList.Count() == 0)
			{
				return Ok($"No employees Found in {department.DepartmentName} department");
			}

			return Ok(employeeList);
		}
	}
}
