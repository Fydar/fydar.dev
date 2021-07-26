using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace Portfolio.Api
{
	public static class IApplicationBuilderExtensions
	{
		public static IApplicationBuilder UsePortfolioApi(this IApplicationBuilder app, PathString path)
		{
			return app.Map(path, map =>
			{
				app.UseStatusCodePagesWithReExecute($"{path}/error/{0}");

				app.UseMiddleware<RequestLoggingMiddleware>();

				app.UseRouting();

				app.UseEndpoints(options =>
				{
					options.MapControllers();
				});
			});
		}
	}
}
