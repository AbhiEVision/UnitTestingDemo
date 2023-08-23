using Sentry;

namespace DemoPractical.API.Middleware
{
	public class ErrorHandalingMiddleware : IMiddleware
	{
		public async Task InvokeAsync(HttpContext context, RequestDelegate next)
		{
			try
			{
				await next.Invoke(context);
			}
			catch (Exception ex)
			{
				context.Response.StatusCode = 500;
				await context.Response.WriteAsync(ex.Message);
				SentrySdk.CaptureException(ex);
				//SentrySdk.CaptureEvent(x);
			}
		}
	}
}
