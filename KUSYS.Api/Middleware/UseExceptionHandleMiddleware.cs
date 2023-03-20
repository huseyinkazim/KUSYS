using KUSYS.Data.DTO;

namespace KUSYS.Api.Middleware
{
	public static class MiddleWare
	{
		public static IApplicationBuilder UseLogMiddleware(this WebApplication app)
		{
			return app.UseMiddleware<MyExceptionMiddleware>();
		}
	}
}

public class MyExceptionMiddleware:IMiddleware
{
	private readonly ILogger<MyExceptionMiddleware> _logger;

	public MyExceptionMiddleware(ILogger<MyExceptionMiddleware> logger)
	{
		_logger = logger;
	}

	public async Task InvokeAsync(HttpContext httpContext, RequestDelegate _next)
	{
		try
		{
			await _next(httpContext);
		}
		catch (Exception ex)
		{
			_logger.LogError(ex.ToString());

			httpContext.Response.StatusCode = 500;
			ServiceResponse<bool> response = new ServiceResponse<bool>
			{
				IsSuccess = false,
				Error = ex.Message
			};

			await httpContext.Response.WriteAsJsonAsync<ServiceResponse<bool>>(response);
		}
		finally
		{
			_logger.LogInformation(
				"Request {method} {url} => {statusCode}",
				httpContext.Request?.Method,
				httpContext.Request?.Path.Value,
				httpContext.Response?.StatusCode);
		}
	}
}