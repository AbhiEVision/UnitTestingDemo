using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace DemoPractical.API.Attributes
{
	[AttributeUsage(AttributeTargets.All)]
	public class LogAttribute : Attribute, IActionFilter
	{

		public void OnActionExecuted(ActionExecutedContext context)
		{
			var controller = context.Controller.GetType().Name;
			string actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
			Log.Logger.Information($"{controller} -> {actionName} -> Executing");

		}

		public void OnActionExecuting(ActionExecutingContext context)
		{
			var controller = context.Controller.GetType().Name;
			string actionName = ((ControllerActionDescriptor)context.ActionDescriptor).ActionName;
			Log.Logger.Information($"{controller} -> {actionName} -> Executed ");
		}
	}
}
