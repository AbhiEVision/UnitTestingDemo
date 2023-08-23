using DemoPractical.Models.DTOs;
using DemoPractical.Models.Models;

namespace DemoPractical.Models.Mappers
{
	public static class MappingModels
	{
		public static EmployeeDetailsDTO MapEmployeeToDTO(Employee employee)
		{
			return new EmployeeDetailsDTO()
			{
				Email = employee.Email,
				Name = employee.Name,
				PhoneNumber = employee.PhoneNumber,
			};
		}

		public static Employee MapEmployeeDTOToEmployee(EmployeeDetailsDTO dto, Employee employee)
		{
			employee.Name = dto.Name;
			employee.PhoneNumber = dto.PhoneNumber;
			employee.Email = dto.Email;
			return employee;
		}

		public static Employee MapCreateEmployeeClassToEmployeeClass(CreateEmployeeDTO dto)
		{
			return new Employee()
			{
				Name = dto.Name,
				Email = dto.Email,
				Password = dto.Password,
				DepartmentId = dto.DepartmentId,
				EmployeeTypeId = dto.EmployeeTypeId,
				PhoneNumber = dto.PhoneNumber
			};
		}

		public static PermentEmployee MapCeateEmployeeClassToPermanentEmployeeClass(int id, CreateEmployeeDTO dto)
		{
			return new PermentEmployee()
			{
				EmployeeId = id,
				Salary = dto.Salary ?? 0
			};
		}

		public static ConractBaseEmployee MapCreateEmployeeToContratBaseEmployeeClass(int id, CreateEmployeeDTO dto)
		{
			return new ConractBaseEmployee()
			{
				EmployeeID = id,
				HourlyPaid = dto.HourlyPaid ?? 0
			};
		}

		public static EmployeeSalaryDetails MapEmployeeSalaryDetailsAndItsPermanentEmployeeDetails(EmployeeDetailsDTO dto, PermentEmployee permentEmployee)
		{
			return new EmployeeSalaryDetails()
			{
				EmployeeName = dto.Name,
				EmployeeEmail = dto.Email,
				TypeOfEmployee = "Permanent",
				Salary = permentEmployee.Salary,
				HourlyPaid = null
			};
		}

		public static EmployeeSalaryDetails MapEmployeeSalaryDetailsAndItsContractBaseEmployeeDetails(EmployeeDetailsDTO dto, ConractBaseEmployee conractBaseEmployee)
		{
			return new EmployeeSalaryDetails()
			{
				EmployeeName = dto.Name,
				EmployeeEmail = dto.Email,
				TypeOfEmployee = "Permanent",
				HourlyPaid = conractBaseEmployee.HourlyPaid,
				Salary = null
			};
		}
	}
}
