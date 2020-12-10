using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;
using RPGCore.Packages;
using System.IO;
using System.Linq;

namespace Portfolio.Instance.Utility
{
	public static class IApplicationBuilderExtensions
	{
		public static IApplicationBuilder UseStaticContentImages(this IApplicationBuilder applicationBuilder, IExplorer explorer)
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
