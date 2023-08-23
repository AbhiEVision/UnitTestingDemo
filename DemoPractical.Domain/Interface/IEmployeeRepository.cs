using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;

namespace DemoPractical.Domain.Interface
{
	public interface IEmployeeRepository
	{
		Task<IEnumerable<Employee>> GetEmployeesAsync();

		Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeDetailsAsync();

		Task<Employee> GetEmployeeByIdAsync(int id);

		Task EditEmployeeDetailsAsync(int empId, EmployeeDetailsDTO employee);

		Task DeleteEmployeeAsync(Employee employee);

		Task CreateEmployeeAsync(CreateEmployeeDTO employee);

		Task<Department> GetEmployeeDepartmentAsync(int empId);

		Task ChangeEmployeeDepartmentAsync(int empId, int depId);

		Task<EmployeeDetailsDTO> GetEmployeeDetailsAsync(int empId);

		Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeWhichAreNotInAnyDepartmentAsync();

		Task<EmployeeSalaryDetails> GetEmployeeSalaryDetailsAsync(int empId);

		Task<Employee> GetEmployeeByEmail(string Email);

		Task<bool> CheckEmployeePassword(LoginDTO model);

		Task<List<string>> GetEmployeeRoles(int empId);
	}
}
