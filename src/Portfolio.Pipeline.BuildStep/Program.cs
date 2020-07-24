using RPGCore.Packages;
using System;
using System.IO;

namespace Portfolio.Pipeline.BuildStep
{
	internal class Program
	{
		private static int Main(string[] args)
		{
			try
			{
				string destination = args[0].Trim('"');

				var directory = new FileInfo(typeof(Program).Assembly.Location).Directory;

				string sourceProjectPath = Path.Combine(directory.FullName, "Content");

				Console.WriteLine($"Copying game data from {sourceProjectPath} to {destination}...");

				using (var project = ProjectExplorer.Load(sourceProjectPath, PortfolioPipelines.Import))
				{
					foreach (var resource in project.Resources)
					{
						foreach (var dependency in resource.Dependencies)
						{
							if (string.IsNullOrEmpty(dependency.Key))
							{
								Console.WriteLine($"ERROR: Invalid Dependency! The resource {resource.FullName}'s dependency \"{dependency.Key}\" is invalid");
							}
							else if (!project.Resources.Contains(dependency.Key))
							{
								Console.WriteLine($"ERROR: Missing Dependency! Unable to find {resource.FullName}'s dependency \"{dependency.Key}\"");
							}
						}
					}

					project.ExportFoldersToDirectory(PortfolioPipelines.Build, destination);
				}

				return 0;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
				return 1;
			}
		}
	}
}
