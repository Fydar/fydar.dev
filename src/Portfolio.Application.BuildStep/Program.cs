using RPGCore.FileTree.FileSystem;
using RPGCore.Packages;
using RPGCore.Projects;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Pipeline.BuildStep
{
	internal class Program
	{
		private static async Task<int> Main(string[] args)
		{
			try
			{
				string destination = "./output";
				var destinationDirectoryInfo = new DirectoryInfo(destination);

				var directory = new FileInfo(typeof(Program).Assembly.Location).Directory;

				string sourceProjectPath = Path.Combine(directory.FullName, "Content");

				Console.WriteLine($"Copying game data:\n- FROM:  {sourceProjectPath}\n- TO:    {destinationDirectoryInfo.FullName}");

				using (var project = await ProjectExplorer.LoadAsync(sourceProjectPath, PortfolioPipelines.Import))
				{
					foreach (var resource in project.Resources)
					{
						foreach (var dependency in resource.Dependencies)
						{
							if (string.IsNullOrEmpty(dependency.Key))
							{
								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Write($"ERROR: Invalid Dependency! The resource '");

								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write(resource.FullName);

								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Write($"' dependency '");

								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write(dependency.Key);

								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.WriteLine("' is invalid");
								// Console.WriteLine($"ERROR: Invalid Dependency! The resource '{resource.FullName}' dependency '{dependency.Key}' is invalid");
							}
							else if (!project.Resources.Contains(dependency.Key))
							{
								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Write($"ERROR: Missing Dependency! Unable to find '");

								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write(resource.FullName);

								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.Write($"' dependency '");

								Console.ForegroundColor = ConsoleColor.Red;
								Console.Write(dependency.Key);

								Console.ForegroundColor = ConsoleColor.DarkRed;
								Console.WriteLine("'");

								// Console.WriteLine($"ERROR: Missing Dependency! Unable to find '{resource.FullName}' dependency '{dependency.Key}'");
							}
						}
					}

					var packageArchiveDirectory = project.ExportFoldersToDirectory(PortfolioPipelines.Build, destination);

					Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.WriteLine("Successfully exported content.");
					Console.WriteLine();

					/*
					var packageArchive = new FileSystemArchive(packageArchiveDirectory, false);
					var package = await PackageExplorer.LoadAsync(packageArchive.RootDirectory);

					foreach (var tag in package.Tags)
					{
						Console.WriteLine($"{tag.Key}:");

						foreach (var taggedResource in tag.Value)
						{
							Console.WriteLine($" - {taggedResource.FullName}");
						}
					}*/
				}

				Console.ResetColor();
				return 0;
			}
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.DarkRed;
				Console.WriteLine(e);
				return 1;
			}
		}
	}
}
