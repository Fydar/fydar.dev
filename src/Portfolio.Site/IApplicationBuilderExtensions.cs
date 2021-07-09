using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using RPGCore.Packages;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Portfolio.Site
{
	public static class IApplicationBuilderExtensions
	{
		public static IApplicationBuilder UsePortfolioSite(this IApplicationBuilder app, PathString path)
		{
			return app.Map("", map =>
			{
				app.UseHttpsRedirection();
				app.UseResponseCompression();

				var env = app.ApplicationServices.GetService<IWebHostEnvironment>();
				var explorer = app.ApplicationServices.GetService<IExplorer>();

				if (explorer == null)
				{
					throw new InvalidOperationException($"No service of type {typeof(IExplorer).Name}.");
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

				app.UseStaticFiles();

				var assemblyDirectory = new FileInfo(Assembly.GetExecutingAssembly().Location).Directory;

				app.UseStaticFiles(new StaticFileOptions()
				{
					FileProvider = new PhysicalFileProvider(Path.Combine(assemblyDirectory?.FullName ?? "", "wwwroot")),
				});
				app.UseStaticContentImages(explorer);

				app.UseMiddleware<RequestLoggingMiddleware>();

				app.UseMvc();
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
			var tempDirectory = new DirectoryInfo("wwwimg");

			tempDirectory.Create();

			applicationBuilder.UseStaticFiles(new StaticFileOptions
			{
				FileProvider = new PhysicalFileProvider(tempDirectory.FullName),
				RequestPath = "/img",
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
