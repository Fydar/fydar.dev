using RPGCore.Packages;
using RPGCore.Projects;
using RPGCore.Projects.Pipeline;
using System;
using System.Collections.Generic;

namespace Portfolio.Pipeline
{
	public class LoggingImportProcessor : IImportProcessor
	{
		public bool CanProcess(IResource resource)
		{
			return true;
		}

		public IEnumerable<ProjectResourceUpdate> ProcessImport(ImportProcessorContext context, IResource resource)
		{
			Console.WriteLine($"Imported {resource.FullName}...");

			return null;
		}
	}
}
