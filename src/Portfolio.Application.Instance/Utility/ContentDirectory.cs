using System;
using System.IO;

namespace Portfolio.Instance.Utility
{
	public static class ContentDirectory
	{
		public static string ContentPath { get; }

		static ContentDirectory()
		{
			if (Directory.Exists("content"))
			{
				ContentPath = "content";
				return;
			}

			var solutionDirectory = new FileInfo(typeof(Program).Assembly.Location).Directory;

			while (solutionDirectory != null && solutionDirectory.Parent != null)
			{
				solutionDirectory = solutionDirectory.Parent;

				if (string.Equals(solutionDirectory.Name, "src", StringComparison.OrdinalIgnoreCase))
				{
					break;
				}
			}

			if (solutionDirectory == null)
			{
				throw new InvalidOperationException("Failed to locate package data to host");
			}

			string destinationPath = Path.Combine(solutionDirectory.FullName, "bin/built-content/content");

			if (Directory.Exists(destinationPath))
			{
				ContentPath = destinationPath;
			}
			else
			{
				throw new InvalidOperationException("Failed to locate package data to host");
			}
		}
	}
}
