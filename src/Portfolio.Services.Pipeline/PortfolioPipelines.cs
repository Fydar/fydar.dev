using RPGCore.Projects;
using RPGCore.Projects.Extensions.MetaFiles;

namespace Portfolio.Services.Pipeline;

public static class PortfolioPipelines
{
	public static ImportPipeline Import { get; }
	public static BuildPipeline Build { get; }

	static PortfolioPipelines()
	{
		Import = ImportPipeline.Create()
			.UseJsonMetaFiles()
			.UseImporter(new ImageImporter())
			.UseImporter(new JsonImporter())
			.UseImporter(new MarkupImporter())
			.UseProcessor(new ImageProcessor())
			.UseProcessor(new LoggingImportProcessor())
			.Build();

		Build = new BuildPipeline()
		{
			ImportPipeline = Import
		};
	}
}
