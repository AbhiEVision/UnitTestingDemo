using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;

namespace DemoPractical.Domain.Interface
{
	public interface IDepartmentRepository
	{

		// Get all the department
		Task<IEnumerable<Department>> GetDepartmentsAsync();

		// Get the Department name by id
		Task<Department> GetDepartmentByIdAsync(int id);

		// Edit the Department 
		Task EditDepartment(Department department);

		// Delete Department by id
		Task DeleteDepartment(int depId);

		Task AddDepartment(Department department);

		Task<bool> IsDepartmentExists(int depId);

		Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeOfDepartment(int departmentId);
	}
}
