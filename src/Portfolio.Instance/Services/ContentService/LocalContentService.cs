using Newtonsoft.Json;
using Portfolio.Instance.Utility;
using Portfolio.Models;
using Portfolio.Models.Blog;
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
		public List<BlogPostModel> BlogPosts { get; }

		public LocalContentService()
		{
			contentExplorer = PackageExplorer.LoadFromDirectoryAsync(ContentDirectory.Path).Result;
			serializer = new JsonSerializer();
			deserializationCache = new Dictionary<IResource, object>();

			Projects = DeserializeAll<ProjectModel>("type-project");
			Categories = DeserializeAll<ProjectCategoryModel>("type-category");
			foreach (var category in Categories)
			{
				category.Projects.Sort();
			}

			Disciplines = DeserializeAll<DisciplineModel>("type-discipline");
			foreach (var discipline in Disciplines)
			{
				discipline.FeaturedProjects.Sort();
			}

			BlogPosts = DeserializeAll<BlogPostModel>("type-blogpost");
		}

		private List<T> DeserializeAll<T>(string tag)
		{
			var items = new List<T>();
			foreach (var resource in contentExplorer.Tags[tag])
			{
				var item = GetOrDeserialize<T>(resource);
				items.Add(item);
			}
			items.Sort();
			return items;
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

		public BlogPostModel GetBlogPost(string slug)
		{
			foreach (var blogPost in BlogPosts)
			{
				if (string.Equals(blogPost.Slug, slug, StringComparison.OrdinalIgnoreCase))
				{
					return blogPost;
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
