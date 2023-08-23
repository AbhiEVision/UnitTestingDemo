using DemoPractical.DataAccessLayer.Data;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoPractical.DataAccessLayer.Repositories
{
	/// <summary>
	/// Department Repository for headlining the operations of the database
	/// </summary>
	public class DepartmentRepository : IDepartmentRepository
	{
		private readonly ApplicationDataContext _db;

		public DepartmentRepository(ApplicationDataContext db)
		{
			_db = db;
		}

		/// <summary>
		/// Adding the department 
		/// </summary>
		/// <param name="department"></param>
		public async Task AddDepartment(Department department)
		{
			department.DepartmentName = department.DepartmentName.ToUpper();
			await _db.Departments.AddAsync(department);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Check if there is department exists or not
		/// </summary>
		/// <param name="depId"></param>
		public async Task<bool> IsDepartmentExists(int depId)
		{
			Department dep = await _db.Departments.FirstOrDefaultAsync(x => x.Id == depId);
			if (dep == null)
			{
				return false;
			}
			return true;
		}

		/// <summary>
		/// Deleting the department
		/// </summary>
		/// <param name="depId"></param>
		public async Task DeleteDepartment(int depId)
		{
			Department dep = await GetDepartmentByIdAsync(depId);

			if (dep != null)
			{
				_db.Departments.Remove(dep);
				await _db.SaveChangesAsync();
			}
		}

		/// <summary>
		/// Edit the department
		/// </summary>
		/// <param name="department"></param>
		public async Task EditDepartment(Department department)
		{
			Department existingDep = await GetDepartmentByIdAsync(department.Id);
			if (existingDep == null)
			{
				return;
			}
			existingDep.DepartmentName = department.DepartmentName.ToUpper();
			_db.Update(existingDep);
			await _db.SaveChangesAsync();
		}

		/// <summary>
		/// Get department by id
		/// </summary>
		/// <param name="id"></param>
		public async Task<Department> GetDepartmentByIdAsync(int id)
		{
			Department? department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == id);
			return department;

		}

		/// <summary>
		/// Get the all departments 
		/// </summary>
		public async Task<IEnumerable<Department>> GetDepartmentsAsync()
		{
			var departments = await _db.Departments.ToListAsync();
			return departments;
		}

		/// <summary>
		/// Return the employees from given department id
		/// </summary>
		/// <param name="departmentId"></param>
		/// <returns></returns>
		public async Task<IEnumerable<EmployeeDetailsDTO>> GetEmployeeOfDepartment(int departmentId)
		{
			Department department = await _db.Departments.FirstOrDefaultAsync(x => x.Id == departmentId);

			if (department == null)
			{
				return Enumerable.Empty<EmployeeDetailsDTO>();
			}

			var employees = _db.Departments
				.Where(x => x.Id == departmentId)
				.Join
				(
					_db.Employees,
					x => x.Id,
					x => x.DepartmentId,
					(dep, emp) => new EmployeeDetailsDTO()
					{
						Name = emp.Name,
						Email = emp.Email,
						PhoneNumber = emp.PhoneNumber,
					}
				);

			return employees;
		}
	}
}
