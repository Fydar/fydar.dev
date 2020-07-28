using Newtonsoft.Json;
using Portfolio.Instance.Utility;
using Portfolio.Models;
using RPGCore.Packages;
using System;
using System.Collections.Generic;
using System.IO;

namespace Portfolio.Instance.Services.ContentService
{
	public class LocalContentService : IContentService, IDisposable, ILoadedResourceCache
	{
		private readonly IExplorer contentExplorer;
		private readonly JsonSerializer serializer;
		private readonly Dictionary<IResource, object> deserializationCache;

		public List<ProjectModel> Projects { get; }
		public List<ProjectCategoryModel> Categories { get; }
		public List<DisciplineModel> Disciplines { get; }

		public LocalContentService()
		{
			contentExplorer = PackageExplorer.LoadFromDirectoryAsync(ContentDirectory.Path).Result;
			serializer = new JsonSerializer();
			deserializationCache = new Dictionary<IResource, object>();

			Projects = new List<ProjectModel>();
			foreach (var resource in contentExplorer.Tags["type-project"])
			{
				Projects.Add(GetOrDeserialize<ProjectModel>(resource));
			}
			Projects.Sort();

			Categories = new List<ProjectCategoryModel>();
			foreach (var resource in contentExplorer.Tags["type-category"])
			{
				var category = GetOrDeserialize<ProjectCategoryModel>(resource);
				category.Projects.Sort();
				Categories.Add(category);
			}
			Categories.Sort();

			Disciplines = new List<DisciplineModel>();
			foreach (var resource in contentExplorer.Tags["type-discipline"])
			{
				var discipline = GetOrDeserialize<DisciplineModel>(resource);
				discipline.FeaturedProjects.Sort();
				Disciplines.Add(discipline);
			}
			Disciplines.Sort();
		}

		public ProjectModel GetProject(string slug)
		{
			foreach (var project in Projects)
			{
				if (string.Equals(project.Slug, slug, StringComparison.OrdinalIgnoreCase))
				{
					return project;
				}
			}
			return null;
		}

		public ProjectCategoryModel GetCategory(string slug)
		{
			foreach (var categories in Categories)
			{
				if (string.Equals(categories.Slug, slug, StringComparison.OrdinalIgnoreCase))
				{
					return categories;
				}
			}
			return null;
		}

		public DisciplineModel GetDiscipline(string slug)
		{
			foreach (var discipline in Disciplines)
			{
				if (string.Equals(discipline.Slug, slug, StringComparison.OrdinalIgnoreCase))
				{
					return discipline;
				}
			}
			return null;
		}

		public void Dispose()
		{
			contentExplorer.Dispose();
		}

		public T GetOrDeserialize<T>(IResource resource)
		{
			lock (deserializationCache)
			{
				if (!deserializationCache.TryGetValue(resource, out object cached))
				{
					using var stream = resource.Content.LoadStream();
					using var sr = new StreamReader(stream);
					using var jsonReader = new JsonTextReader(sr);

					cached = serializer.Deserialize<T>(jsonReader);
					deserializationCache.Add(resource, cached);

					if (cached is ILoadResourceCallback callback)
					{
						callback.OnAfterDeserializedFrom(this, resource);
					}
				}
				return (T)cached;
			}
		}

		public IResource GetResource(string fullname)
		{
			if (!string.IsNullOrEmpty(fullname))
			{
				return contentExplorer.Resources[fullname];
			}
			return null;
		}
	}
}
