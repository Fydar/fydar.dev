using RPGCore.Packages;
using RPGCore.Packages.Pipeline;
using System;
using System.Collections.Generic;

namespace Portfolio.Pipeline
{
	public class LoggingImporter : IImportProcessor
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
