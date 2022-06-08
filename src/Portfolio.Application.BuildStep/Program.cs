using Portfolio.Services.Pipeline;
using RPGCore.FileTree.FileSystem;
using RPGCore.Packages;
using RPGCore.Projects;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Application.BuildStep;

internal class Program
{
	private static async Task<int> Main(string[] args)
	{
		try
		{
			var directory = new FileInfo(typeof(Program).Assembly.Location).Directory;
			var solutionDirectory = new FileInfo(typeof(Program).Assembly.Location).Directory;

			while (solutionDirectory != null && solutionDirectory.Parent != null)
			{
				solutionDirectory = solutionDirectory.Parent;

				if (string.Equals(solutionDirectory.Name, "src", StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
			}

			if (directory == null || solutionDirectory == null)
			{
				throw new InvalidOperationException();
			}

			string sourceProjectPath = Path.Combine(directory.FullName, "Content");
			var sourceProjectInfo = new DirectoryInfo(sourceProjectPath);

			string destinationPath = Path.Combine(solutionDirectory.FullName, "bin/built-content");
			var destinationInfo = new DirectoryInfo(destinationPath);

			Console.WriteLine($"Exporting portfolio content...");
			Console.WriteLine();

			Console.ForegroundColor = ConsoleColor.DarkGray;
			Console.WriteLine($"from   {sourceProjectInfo.FullName}");
			Console.WriteLine($"to     {destinationInfo.FullName}");
			Console.WriteLine();

			int warnings = 0;

			using (var project = await ProjectExplorer.LoadAsync(sourceProjectPath, PortfolioPipelines.Import))
			{
				foreach (var resource in project.Resources)
				{
					foreach (var dependency in resource.Dependencies)
					{
						if (string.IsNullOrEmpty(dependency.Key))
						{
							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.Write($"WARNING: Invalid Dependency! The resource '");

							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.Write(resource.FullName);

							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.Write($"' dependency '");

							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.Write(dependency.Key);

							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.WriteLine("' is invalid");

							warnings++;
						}
						else if (!project.Resources.Contains(dependency.Key))
						{
							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.Write($"WARNING: Missing Dependency! Unable to find '");

							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.Write(resource.FullName);

							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.Write($"' dependency '");

							Console.ForegroundColor = ConsoleColor.Yellow;
							Console.Write(dependency.Key);

							Console.ForegroundColor = ConsoleColor.DarkYellow;
							Console.WriteLine("'");

							warnings++;
						}
					}
				}

				var packageArchiveDirectory = project.ExportFoldersToDirectory(PortfolioPipelines.Build, destinationPath);

				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Successfully exported content.");
				Console.WriteLine();

				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine($"  {project.Resources.Count} resources exported");
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"  {warnings,2} warnings");
				Console.WriteLine();

				var packageArchive = new FileSystemArchive(packageArchiveDirectory, false);
				var package = await PackageExplorer.LoadAsync(packageArchive.RootDirectory);

				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("Tags");
				foreach (var tag in package.Tags)
				{
					Console.WriteLine($"  {tag.Value.Count,2} tagged '{tag.Key}'");
				}
				Console.WriteLine();
				Console.ForegroundColor = ConsoleColor.Gray;
				Console.WriteLine("File Types");

				var resourcesByExtension = package.Resources.GroupBy(resource => resource.Extension);
				foreach (var resourceExtension in resourcesByExtension)
				{
					Console.WriteLine($"  {resourceExtension.Count(),3} of type {resourceExtension.Key}");
				}
				Console.WriteLine();
			}

			Console.ResetColor();
			return 0;
		}
		catch (Exception exception)
		{
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine(exception);
			return 1;
		}
	}
}
