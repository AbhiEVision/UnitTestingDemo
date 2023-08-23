using DemoPractical.API.Attributes;
using DemoPractical.Domain.Interface;
using DemoPractical.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DemoPractical.API.Controllers.V2
{
	[Log]
	[ApiVersion("2.0")]
	[EndpointGroupName("v2")]
	[Route("api/v{version:apiVersion}/[controller]/[action]")]
	[ApiController]
	public class EmailController : ControllerBase
	{
		private readonly IEmailService _emailService;

		public EmailController(IEmailService emailService)
		{
			_emailService = emailService;
		}

		/// <summary>
		/// This is used for sending mail to someone but it can only access by the Admin Role
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[Authorize(Roles = "Admin")]
		[HttpPost]
		public IActionResult SendEmail(EmailModel model)
		{
			try
			{
				bool test = _emailService.SendMail(model);
				if (!test)
				{
					return BadRequest("Not Send!");
				}
				return Ok("Mail Send Successfully!");

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
	}
}
