using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Portfolio.Instance.Utility
{
	public class RequestLoggingMiddleware
	{
		private readonly RequestDelegate next;
		private readonly ILogger logger;

		public RequestLoggingMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
		{
			this.next = next;
			logger = loggerFactory.CreateLogger<RequestLoggingMiddleware>();
		}

		public async Task Invoke(HttpContext context)
		{
			try
			{
				await next(context);
			}
			finally
			{
				logger.LogInformation(
					"Request {method} {url} => {statusCode}",
					context.Request?.Method,
					context.Request?.Path.Value,
					context.Response?.StatusCode);
			}
		}
	}
}
