using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Portfolio.Models;
using Portfolio.Models.Blog;
using RPGCore.FileTree;
using RPGCore.Projects;
using RPGCore.Projects.Pipeline;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Pipeline
{
	public class JsonImporter : IArchiveFileImporter
	{
		private class JsonContentWriter : IContentWriter
		{
			private readonly object content;

			public JsonContentWriter(object content)
			{
				this.content = content;
			}

			public Task WriteContentAsync(Stream destination)
			{
				var serializer = new JsonSerializer();
				using var streamWriter = new StreamWriter(destination);
				serializer.Serialize(streamWriter, content);
				return Task.CompletedTask;
			}
		}

		private static readonly Dictionary<string, string> featuredImageMetadata = new()
		{
			["Size"] = "fullscreen,medium,thumbnail,blur"
		};

		private static readonly Dictionary<string, string> otherImageMetadata = new()
		{
			["Size"] = "medium,thumbnail,blur"
		};

		public bool CanImport(IArchiveFile archiveFile)
		{
			return archiveFile.Extension == ".json";
		}

		public IEnumerable<ProjectResourceUpdate> ImportFile(ArchiveFileImporterContext context, IArchiveFile archiveFile)
		{
			var update = context.AuthorUpdate(archiveFile.FullName);

			object content = null;

			if (archiveFile.FullName.StartsWith("data/projects/categories"))
			{
				update.ImporterTags.Add("type-category");

				var loaded = LoadJson<ProjectCategoryModel>(archiveFile);
				content = loaded;

				update.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
			}
			else if (archiveFile.FullName.StartsWith("data/disciplines"))
			{
				update.ImporterTags.Add("type-discipline");

				var loaded = LoadJson<DisciplineModel>(archiveFile);
				content = loaded;

				update.Dependencies.Register(loaded.Page);
				update.Dependencies.Register(loaded.FeaturedImage);
				update.Dependencies.Register(loaded.IconImage);
			}
			else if (archiveFile.FullName.StartsWith("data/projects"))
			{
				update.ImporterTags.Add("type-project");

				var loaded = LoadJson<ProjectModel>(archiveFile);
				content = loaded;

				update.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
				update.Dependencies.Register(loaded.HoverImage, metadata: otherImageMetadata);

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
				update.ImporterTags.Add("type-college");

				var loaded = LoadJson<CollegeModel>(archiveFile);
				content = loaded;

				update.Dependencies.Register(loaded.IconUrl);
				update.Dependencies.Register(loaded.Page);
			}
			else if (archiveFile.FullName.StartsWith("data/company"))
			{
				update.ImporterTags.Add("type-company");

				var loaded = LoadJson<CompanyModel>(archiveFile);
				content = loaded;

				update.Dependencies.Register(loaded.IconUrl);
				update.Dependencies.Register(loaded.Page);
			}
			else if (archiveFile.FullName.StartsWith("data/blog/posts"))
			{
				update.ImporterTags.Add("type-blogpost");

				var loaded = LoadJson<BlogPostModel>(archiveFile);
				content = loaded;

				update.Dependencies.Register(loaded.FeaturedImage);
				update.Dependencies.Register(loaded.Page);
			}

			if (content == null)
			{
				content = LoadJObject(archiveFile);
			}

			update.WithContent(new JsonContentWriter(content));

			yield return update;
		}

		private static object LoadJObject(IArchiveFile importer)
		{
			var serializer = new JsonSerializer();
			using var file = importer.OpenRead();
			using var sr = new StreamReader(file);
			using var reader = new JsonTextReader(sr);

			var model = serializer.Deserialize<JObject>(reader);
			return model;
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
