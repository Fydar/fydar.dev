using RPGCore.Packages;
using RPGCore.Packages.Extensions.MetaFiles;

namespace Portfolio.Pipeline
{
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
				.UseProcessor(new ImageProcessor())
				.UseProcessor(new LoggingImporter())
				.Build();

			Build = new BuildPipeline()
			{
				ImportPipeline = Import
			};
			Build.Exporters.Add(new JsonExporter());
			Build.Exporters.Add(new MarkupExporter());
		}
	}
}
