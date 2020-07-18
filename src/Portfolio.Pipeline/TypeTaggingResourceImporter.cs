using Newtonsoft.Json;
using Portfolio.Models;
using RPGCore.Packages.Pipeline;
using System.IO;

namespace Portfolio.Pipeline
{
	public class TypeTaggingResourceImporter : ImportProcessor
	{
		public override void ProcessImport(ProjectResourceImporter importer)
		{
			if (importer.ArchiveEntry.FullName.Contains("categories"))
			{
				importer.ImporterTags.Add("type-category");
			}
			else if (importer.ArchiveEntry.FullName.Contains("projects"))
			{
				importer.ImporterTags.Add("type-project");

				var loaded = Load<ProjectModel>(importer);
				importer.Dependencies.Register(loaded.FeaturedImage);
			}
			else if (importer.ArchiveEntry.FullName.Contains("education"))
			{
				importer.ImporterTags.Add("type-education");

				var loaded = Load<EducationalInstitutionModel>(importer);
				importer.Dependencies.Register(loaded.IconUrl);
			}
			else if (importer.ArchiveEntry.FullName.Contains("company"))
			{
				importer.ImporterTags.Add("type-company");

				var loaded = Load<CompanyModel>(importer);
				importer.Dependencies.Register(loaded.IconUrl);
			}


			if (importer.ArchiveEntry.FullName.Contains("game-jams"))
			{
				importer.ImporterTags.Add("category-gamejam");
			}
			else if (importer.ArchiveEntry.FullName.Contains("personal"))
			{
				importer.ImporterTags.Add("category-personal");
			}
			else if (importer.ArchiveEntry.FullName.Contains("professional"))
			{
				importer.ImporterTags.Add("category-professional");
			}
		}

		private static TModel Load<TModel>(ProjectResourceImporter importer)
		{
			var serializer = new JsonSerializer();
			using var file = importer.ArchiveEntry.OpenRead();
			using var sr = new StreamReader(file);
			using var reader = new JsonTextReader(sr);

			var model = serializer.Deserialize<TModel>(reader);
			return model;
		}
	}
}