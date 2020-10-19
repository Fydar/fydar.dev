using Newtonsoft.Json;
using Portfolio.Models;
using Portfolio.Models.Blog;
using RPGCore.Packages;
using RPGCore.Packages.Archives;
using RPGCore.Packages.Pipeline;
using System.Collections.Generic;
using System.IO;

namespace Portfolio.Pipeline
{
	public class JsonImporter : IArchiveFileImporter
	{
		private static readonly Dictionary<string, string> featuredImageMetadata = new Dictionary<string, string>()
		{
			["Size"] = "fullscreen,medium,thumbnail,blur"
		};

		public bool CanImport(IArchiveFile archiveFile)
		{
			return archiveFile.Extension == ".json";
		}

		public IEnumerable<ProjectResourceUpdate> ImportFile(ArchiveFileImporterContext context, IArchiveFile archiveFile)
		{
			var update = context.AuthorUpdate(archiveFile.FullName)
				.WithContent(archiveFile);

			if (archiveFile.FullName.StartsWith("data/projects/categories"))
			{
				update.ImporterTags.Add("type-category");

				var loaded = LoadJson<ProjectCategoryModel>(archiveFile);

				update.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
			}
			else if (archiveFile.FullName.StartsWith("data/disciplines"))
			{
				update.ImporterTags.Add("type-discipline");

				var loaded = LoadJson<DisciplineModel>(archiveFile);

				update.Dependencies.Register(loaded.Page);
				update.Dependencies.Register(loaded.FeaturedImage);
				update.Dependencies.Register(loaded.IconImage);
			}
			else if (archiveFile.FullName.StartsWith("data/projects"))
			{
				update.ImporterTags.Add("type-project");

				var loaded = LoadJson<ProjectModel>(archiveFile);
				update.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
				update.Dependencies.Register(loaded.HoverImage, metadata: featuredImageMetadata);

				update.Dependencies.Register(loaded.ProjectCategory);
				update.Dependencies.Register(loaded.Page);

				if (!string.IsNullOrEmpty(loaded.Institution))
				{
					update.Dependencies.Register(loaded.Institution);
				}

				foreach (string disciplines in loaded.Disciplines)
				{
					update.Dependencies.Register(disciplines);
				}
			}
			else if (archiveFile.FullName.StartsWith("data/education"))
			{
				update.ImporterTags.Add("type-education");

				var loaded = LoadJson<CollegeModel>(archiveFile);

				update.Dependencies.Register(loaded.IconUrl);
				update.Dependencies.Register(loaded.Page);
			}
			else if (archiveFile.FullName.StartsWith("data/company"))
			{
				update.ImporterTags.Add("type-company");

				var loaded = LoadJson<CompanyModel>(archiveFile);

				update.Dependencies.Register(loaded.IconUrl);
				update.Dependencies.Register(loaded.Page);
			}
			else if (archiveFile.FullName.StartsWith("data/blog/posts"))
			{
				update.ImporterTags.Add("type-blogpost");

				var loaded = LoadJson<BlogPostModel>(archiveFile);

				update.Dependencies.Register(loaded.FeaturedImage);
				update.Dependencies.Register(loaded.Page);
			}

			yield return update;
		}

		private static TModel LoadJson<TModel>(IArchiveFile archiveFile)
		{
			var serializer = new JsonSerializer();
			using var file = archiveFile.OpenRead();
			using var sr = new StreamReader(file);
			using var reader = new JsonTextReader(sr);

			var model = serializer.Deserialize<TModel>(reader);
			return model;
		}
	}
}
