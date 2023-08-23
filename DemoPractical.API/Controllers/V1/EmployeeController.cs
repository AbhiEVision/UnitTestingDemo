using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractical.API.Controllers.V1
{
	[ApiVersion("1.0")]
	[EndpointGroupName("v1")]
	[Route("api/v{version:apiVersion}/[controller]/[action]")]
	[ApiController]
	public class EmployeeController : ControllerBase
	{
		private readonly IEmployeeRepository _employeeRepository;
		private readonly IDepartmentRepository _departmentRepository;

		public EmployeeController(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
		{
			_employeeRepository = employeeRepository;
			_departmentRepository = departmentRepository;
		}

		/// <summary>
		/// Get all employees 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetEmployees()
		{
			var employees = await _employeeRepository.GetEmployeesAsync();

			if (employees == null || employees.Count() == 0)
			{
				return Ok("No employees Found");
			}

			return Ok(employees);
		}

		/// <summary>
		/// Get the employee by id
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		[HttpGet]
		public async Task<IActionResult> GetEmployee(int id)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the valid id");
			}

			Employee employee = await _employeeRepository.GetEmployeeByIdAsync(id);

			if (employee == null)
			{
				return BadRequest("Requested data is not found!");
			}


			return Ok(employee);
		}

		/// <summary>
		/// Create employee using create employee DTO
		/// </summary>
		/// <param name="employee"></param>
		[HttpPost]
		public async Task<IActionResult> CreateEmployee(CreateEmployeeDTO employee)
		{
			if (employee == null)
			{
				return BadRequest("Please Enter the valid data");
			}

			Employee emp = await _employeeRepository.GetEmployeeByEmail(employee.Email);
			if (emp != null)
			{
				return BadRequest("User already exists");
			}

			try
			{
				await _employeeRepository.CreateEmployeeAsync(employee);

				return Ok("Employee Created!");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		/// <summary>
		/// Edit the Employee
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		[HttpPut]
		public async Task<IActionResult> EditEmployee([FromQuery] int id, [FromBody] EmployeeDetailsDTO employee)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the valid id");
			}

			if (employee == null)
			{
				return BadRequest("Please Enter the Valid data");
			}

			Employee emp = await _employeeRepository.GetEmployeeByIdAsync(id);

			if (emp == null)
			{
				return BadRequest("No such employee found!");
			}

			try
			{
				await _employeeRepository.EditEmployeeDetailsAsync(id, employee);
				return Ok($"{employee.Name} is successfully edited!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}

		}

		/// <summary>
		/// Delete the employee
		/// </summary>
		/// <param name="id"></param>
		[HttpDelete]
		public async Task<IActionResult> DeleteEmployee(int id)
		{
			if (id == null || id <= 0)
			{
				return BadRequest("Please enter the valid data");
			}

			Employee employee = await _employeeRepository.GetEmployeeByIdAsync(id);
			if (employee == null)
			{
				return BadRequest("No such data found!");
			}

			try
			{
				await _employeeRepository.DeleteEmployeeAsync(employee);
				return Ok($"{employee.Name} is successfully deleted!");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}



	}
}
