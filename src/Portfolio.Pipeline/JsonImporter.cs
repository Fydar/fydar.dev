using Newtonsoft.Json;
using Portfolio.Models;
using RPGCore.Packages.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Portfolio.Pipeline
{
	public class JsonImporter : ImportProcessor
	{
		private static readonly Dictionary<string, string> featuredImageMetadata = new Dictionary<string, string>()
		{
			["Size"] = "fullscreen,medium,thumbnail"
		};

		public override void ProcessImport(ProjectResourceImporter importer)
		{
			Console.WriteLine($"Importing {importer.ArchiveEntry.FullName}...");
			if (importer.ArchiveEntry.FullName.StartsWith("data/projects/categories"))
			{
				importer.ImporterTags.Add("type-category");

				var loaded = LoadJson<ProjectCategoryModel>(importer);

				importer.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/disciplines"))
			{
				importer.ImporterTags.Add("type-discipline");

				var loaded = LoadJson<DisciplineModel>(importer);

				importer.Dependencies.Register(loaded.Page);
				importer.Dependencies.Register(loaded.FeaturedImage);
				importer.Dependencies.Register(loaded.IconImage);
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/projects"))
			{
				importer.ImporterTags.Add("type-project");

				var loaded = LoadJson<ProjectModel>(importer);
				importer.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
				importer.Dependencies.Register(loaded.HoverImage, metadata: featuredImageMetadata);

				importer.Dependencies.Register(loaded.ProjectCategory);
				importer.Dependencies.Register(loaded.Page);

				if (!string.IsNullOrEmpty(loaded.Institution))
				{
					importer.Dependencies.Register(loaded.Institution);
				}

				foreach (string disciplines in loaded.Disciplines)
				{
					importer.Dependencies.Register(disciplines);
				}
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/education"))
			{
				importer.ImporterTags.Add("type-education");

				var loaded = LoadJson<CollegeModel>(importer);

				importer.Dependencies.Register(loaded.IconUrl);
				importer.Dependencies.Register(loaded.Page);
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/company"))
			{
				importer.ImporterTags.Add("type-company");

				var loaded = LoadJson<CompanyModel>(importer);

				importer.Dependencies.Register(loaded.IconUrl);
				importer.Dependencies.Register(loaded.Page);
			}
		}

		private static TModel LoadJson<TModel>(ProjectResourceImporter importer)
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
