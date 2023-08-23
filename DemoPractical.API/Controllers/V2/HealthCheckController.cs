using DemoPractical.API.Attributes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace DemoPractical.API.Controllers.V2
{
	[Log]
	[EndpointGroupName("v2")]
	[ApiVersion("2.0")]
	[Route("api/v{version:apiVersion}/[controller]/[action]")]
	[ApiController]
	public class HealthCheckController : ControllerBase
	{
		private readonly HealthCheckService _services;

		public HealthCheckController(HealthCheckService services)
		{
			_services = services;
		}

		[Authorize]
		[HttpGet]
		public async Task<IActionResult> GetHealthCheck()
		{
			var result = await _services.CheckHealthAsync();

			return Ok(result);
		}

	}
}
