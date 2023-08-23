using DemoPractical._Domain.Interface;
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
	public class AccountController : ControllerBase
	{
		private readonly IEmployeeRepository _repository;
		private readonly IJwtServices _jwtServices;

		public AccountController(IEmployeeRepository repository, IJwtServices jwtServices)
		{
			_repository = repository;
			_jwtServices = jwtServices;
		}

		/// <summary>
		/// For Getting the JWT token which need for sending mail
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<IActionResult> Login(LoginDTO model)
		{
			if (model == null)
			{
				return BadRequest("Please enter valid data");
			}

			Employee emp = await _repository.GetEmployeeByEmail(model.Email);

			if (emp == null)
			{
				return BadRequest("Employee is not found!");
			}

			if (!await _repository.CheckEmployeePassword(model))
			{
				return BadRequest("Invalid Credentials");
			}

			List<string> roles = await _repository.GetEmployeeRoles(emp.Id);

			if (roles == null)
			{
				roles = new List<string>();
			}

			string token = _jwtServices.GetJwtToken(emp, roles);

			return Ok(token);

		}


	}
}
