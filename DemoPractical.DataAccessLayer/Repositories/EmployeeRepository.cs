using DemoPractical.DataAccessLayer.Data;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Mappers;
using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Repositories
{
	/// <summary>
	/// Employee Repository which maintains the working with the database operations related to the employee table
	/// </summary>
	public class EmployeeRepository : IEmployeeRepository
	{
		private readonly ApplicationDataContext _db;
		private readonly IDepartmentRepository _departmentRepository;

		public EmployeeRepository(ApplicationDataContext db, IDepartmentRepository departmentRepository)
		{
			_db = db;
			_departmentRepository = departmentRepository;
		}

		/// <summary>
		/// Changing the department of employee
		/// </summary>
		/// <param name="empId">employee id for which we want to change the department</param>
		/// <param name="depId">department id first check if there is exists or not after that we will update it!</param>
		/// <returns>Nothing!</returns>
		public async Task ChangeEmployeeDepartmentAsync(int empId, int depId)
		{
			bool depExists = await _departmentRepository.IsDepartmentExists(depId);
			if (depExists)
			{
				Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == empId);
				if (employee == null)
				{
					return;
				}
				employee.DepartmentId = depId;
				await _db.SaveChangesAsync();
			}
			return;
		}

		/// <summary>
		/// Create employee
		/// </summary>
		/// <param name="employee">create this employee</param>
		public async Task CreateEmployeeAsync(CreateEmployeeDTO employee)
		{
			// employee creation logic
			Employee toAdd = MappingModels.MapCreateEmployeeClassToEmployeeClass(employee);
			_db.Employees.Add(toAdd);
			await _db.SaveChangesAsync();
			var lastestEmpID = _db.Employees.Max(x => x.Id);

			// adding employee type logic
			if (employee.EmployeeTypeId == 1)
			{
				PermentEmployee permentEmployee = MappingModels.MapCeateEmployeeClassToPermanentEmployeeClass(lastestEmpID, employee);
				_db.PermentEmployees.Add(permentEmployee);
				await _db.SaveChangesAsync();
			}
			else if (employee.EmployeeTypeId == 2)
			{
				ConractBaseEmployee conractBaseEmployee = MappingModels.MapCreateEmployeeToContratBaseEmployeeClass(lastestEmpID, employee);
				_db.ConractBaseEmployees.Add(conractBaseEmployee);
				await _db.SaveChangesAsync();
			}

			// adding employee role logic
			await AddEmployeeToRole(lastestEmpID, employee.RoleId);
		}

		/// <summary>
		/// Delete Employee using class
		/// </summary>
		public async Task DeleteEmployeeAsync(Employee employee)
		{
			Employee getEmp = await _db.Employees.FirstOrDefaultAsync(x => x.Id == employee.Id);
			if (getEmp == null)
			{
				return;
			}

			_db.Remove(employee);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Edit the existing employee
		/// </summary>
		public async Task EditEmployeeDetailsAsync(int empId, EmployeeDetailsDTO employee)
		{
			Employee oldEmployee = await GetEmployeeByIdAsync(empId);
			if (oldEmployee == null)
			{
				return;
			}
			oldEmployee.Name = employee.Name;
			oldEmployee.Email = employee.Email;
			oldEmployee.PhoneNumber = employee.PhoneNumber;
			_db.Update(oldEmployee);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Returns the employee using its id
		/// </summary>
		/// <param name="id">id for fetch</param>
		/// <returns >Employee object from database </returns>
		public async Task<Employee> GetEmployeeByIdAsync(int id)
		{
			Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == id);
			return employee;
		}

		/// <summary>
		/// Returns the department of the employee
		/// </summary>
		/// <param name="empId"></param>
		/// <returns>The Department object</returns>
		public async Task<Department> GetEmployeeDepartmentAsync(int empId)
		{
			Employee emp = await GetEmployeeByIdAsync(empId);
			if (emp == null || emp.DepartmentId == null)
			{
				return null;
			}

			Department department = await _departmentRepository.GetDepartmentByIdAsync(emp.DepartmentId ?? 1);
			return department;
		}

		/// <summary>
		/// Returns the all Employees
		/// </summary>
		/// <returns>List of Employee</returns>
		public async Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeDetailsAsync()
		{
			var employees = await _db.Employees.ToListAsync();

			return MapListToEmployeeDetails(employees);
		}

		/// <summary>
		/// Getting the employee details using its id
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		public async Task<EmployeeDetailsDTO> GetEmployeeDetailsAsync(int empId)
		{
			Employee employee = await _db.Employees.FirstOrDefaultAsync(x => x.Id == empId);
			if (employee == null)
			{
				return null;
			}

			return MapEmployeeToItsDTO(employee);
		}

		/// <summary>
		/// Mapping list of employees into employeeDTO
		/// </summary>
		/// <param name="employees"></param>
		/// <returns></returns>
		private List<EmployeeDetailsDTO> MapListToEmployeeDetails(List<Employee> employees)
		{
			var employeeDTOs = new List<EmployeeDetailsDTO>();

			foreach (var employee in employees)
			{
				employeeDTOs.Add(new EmployeeDetailsDTO()
				{
					Email = employee.Email,
					Name = employee.Name,
					PhoneNumber = employee.PhoneNumber,
				});
			}

			return employeeDTOs;

		}

		/// <summary>
		/// Map Employee to its DTO
		/// </summary>
		/// <param name="employee"></param>
		/// <returns></returns>
		private EmployeeDetailsDTO MapEmployeeToItsDTO(Employee employee)
		{
			var employeeDTO = new EmployeeDetailsDTO();
			employeeDTO.Email = employee.Email;
			employeeDTO.Name = employee.Name;
			employeeDTO.PhoneNumber = employee.PhoneNumber;

			return employeeDTO;
		}

		/// <summary>
		/// Getting the employees which are not in any department
		/// </summary>
		/// <returns></returns>
		public async Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeWhichAreNotInAnyDepartmentAsync()
		{
			var employees = await _db.Employees.Where(x => x.DepartmentId == null).ToListAsync();

			var employeeDTOs = MapListToEmployeeDetails(employees);

			return employeeDTOs;
		}

		/// <summary>
		/// Getting salary details according to its type
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		public async Task<EmployeeSalaryDetails> GetEmployeeSalaryDetailsAsync(int empId)
		{
			var employee = await GetEmployeeByIdAsync(empId);

			if (employee == null)
			{
				return null;
			}

			var employeeSalaryDetails = new EmployeeSalaryDetails();

			if (employee.EmployeeTypeId == 1)
			{
				employeeSalaryDetails = MappingModels
					.MapEmployeeSalaryDetailsAndItsPermanentEmployeeDetails(
						MapEmployeeToItsDTO(employee),
						await _db.PermentEmployees.Where(x => x.EmployeeId == employee.Id).FirstAsync()
					);
			}
			else
			{
				employeeSalaryDetails = MappingModels
					.MapEmployeeSalaryDetailsAndItsContractBaseEmployeeDetails(
						MapEmployeeToItsDTO(employee),
						await _db.ConractBaseEmployees.Where(x => x.EmployeeID == employee.Id).FirstAsync()
					);
			}

			return employeeSalaryDetails;

		}

		/// <summary>
		/// Add Employee to its Role in EmployeeRole Table
		/// </summary>
		/// <param name="empId"></param>
		/// <param name="roleId"></param>
		private async Task AddEmployeeToRole(int empId, int roleId)
		{
			await _db.EmployeeRoles.AddAsync(new EmployeeRole() { EmployeeId = empId, RoleId = roleId });
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Get Employee By its Email
		/// </summary>
		/// <param name="Email"></param>
		/// <returns></returns>
		public async Task<Employee> GetEmployeeByEmail(string Email)
		{
			Employee employee = await _db.Employees.SingleOrDefaultAsync(x => x.Email == Email);

			if (employee == null)
			{
				return null;
			}

			return employee;
		}

		/// <summary>
		/// Check whether the password is correct or not!
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public async Task<bool> CheckEmployeePassword(LoginDTO model)
		{
			Employee employee = await _db.Employees.SingleOrDefaultAsync(x => x.Email == model.Email);

			if (employee == null)
			{
				return false;
			}

			if (employee.Password == model.Password)
			{
				return true;
			}
			else
			{
				return false;
			}

		}

		/// <summary>
		/// Returning the roles of employee
		/// </summary>
		/// <param name="empId"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<List<string>> GetEmployeeRoles(int empId)
		{
			Employee employee = await _db.Employees.SingleOrDefaultAsync(x => x.Id == empId);

			if (employee == null)
			{
				return null;
			}

			var roles = _db.Employees
				.Where(x => x.Id == empId)
				.Join
				(
					_db.EmployeeRoles,
					x => x.Id,
					x => x.EmployeeId,
					(x, y) => new
					{
						RolesId = y.RoleId
					}
				)
				.Join
				(
					_db.Roles,
					x => x.RolesId,
					x => x.Id,
					(x, y) => y.RoleName
				);

			return await roles.ToListAsync();
		}

		public async Task<IEnumerable<Employee>> GetEmployeesAsync()
		{
			return await _db.Employees.ToListAsync();
		}
	}
}
