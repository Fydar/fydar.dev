using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Net.Http.Headers;
using RPGCore.Packages;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Portfolio.Component.Website.Server
{
	public static class IApplicationBuilderExtensions
	{

		private const int cacheTTL = 60 * 60 * 3;

		public static IApplicationBuilder UsePortfolioSite(this IApplicationBuilder app, PathString path)
		{
			return app.Map(path, map =>
			{
				app.UseHttpsRedirection();
				app.UseResponseCompression();

				var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
				var explorer = app.ApplicationServices.GetService<IExplorer>();

				if (explorer == null)
				{
					throw new InvalidOperationException($"No service of type '{typeof(IExplorer).Name}'.");
				}

				// Exception handling
				if (env.IsDevelopment())
				{
					app.UseDeveloperExceptionPage();
				}
				else
				{
					app.UseStatusCodePagesWithReExecute($"{path}/error/{0}");
				}

				app.UseStaticFiles(new StaticFileOptions()
				{
					OnPrepareResponse = ctx =>
					{
						ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={cacheTTL}";
					}
				});

				var assemblyDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

				app.UseStaticFiles(new StaticFileOptions()
				{
					FileProvider = new PhysicalFileProvider(Path.Combine(assemblyDirectory?.FullName ?? "", "wwwroot")),
					OnPrepareResponse = ctx =>
					{
						ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={cacheTTL}";
					}
				});
				app.UseStaticContentImages(explorer);

				app.UseMiddleware<RequestLoggingMiddleware>();

				app.UseRouting();

				app.UseAuthorization();

				app.UseEndpoints(endpoints =>
				{
					endpoints.MapControllerRoute(
						name: "default",
						pattern: path + "{controller=Portfolio}/{action=Index}");
				});
			});
		}

		private static IApplicationBuilder UseStaticContentImages(this IApplicationBuilder applicationBuilder, IExplorer explorer)
		{
			var assemblyDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;
			var tempDirectory = new DirectoryInfo(Path.Combine(assemblyDirectory?.FullName ?? "", "wwwimg"));

			tempDirectory.Create();

			applicationBuilder.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(tempDirectory.FullName),
				RequestPath = "/img",
				OnPrepareResponse = ctx =>
				{
					ctx.Context.Response.Headers[HeaderNames.CacheControl] = $"public,max-age={cacheTTL}";
				}
			});

			var imageDirectory = explorer.RootDirectory.Directories.First(d => d.Name == "img");

			static void CopyDirectoryRecursive(IDirectory directory, DirectoryInfo target)
			{
				target.Create();

				string targetFullName = target.FullName;

				foreach (var resource in directory.Resources)
				{
					string filePath = Path.Combine(targetFullName, resource.Name);
					var file = new FileInfo(filePath);

					if (file.Exists)
					{
						file.Delete();
					}

					using var fs = file.OpenWrite();
					using var sr = resource.Content.OpenRead();
					sr.CopyTo(fs);
				}

				foreach (var childDirectory in directory.Directories)
				{
					string targetChildDirectoryPath = Path.Combine(targetFullName, childDirectory.Name);
					var targetChildDirectory = new DirectoryInfo(targetChildDirectoryPath);

					CopyDirectoryRecursive(childDirectory, targetChildDirectory);
				}
			}

			CopyDirectoryRecursive(imageDirectory, tempDirectory);

			return applicationBuilder;
		}
	}
}
