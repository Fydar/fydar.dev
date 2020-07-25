using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Portfolio.Instance.Services.ContentService;
using Portfolio.Instance.Utility;
using System.IO;

namespace Portfolio.Instance
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddSingleton<IContentService, LocalContentService>();

			services.AddControllers(options =>
			{
			});

			services.AddMvc(options =>
			{
				options.EnableEndpointRouting = false;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseStatusCodePagesWithReExecute("/error/{0}");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(env.ContentRootPath, ContentDirectory.Path, "data", "img")),
				RequestPath = "/img",
			});

			app.UseMvc();
			app.UseRouting();

			app.UseAuthorization();

			app.UseMiddleware<RequestLoggingMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}");
			});
		}
	}
}
