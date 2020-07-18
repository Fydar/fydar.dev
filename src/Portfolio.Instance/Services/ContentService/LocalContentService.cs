using Newtonsoft.Json;
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

		public LocalContentService()
		{
			contentExplorer = PackageExplorer.LoadFromDirectoryAsync(ContentDirectory.Path).Result;
			serializer = new JsonSerializer();
			deserializationCache = new Dictionary<IResource, object>();
		}

		public IEnumerable<ProjectModel> AllProjects()
		{
			foreach (var resource in contentExplorer.Tags["type-project"])
			{
				yield return GetOrDeserialize<ProjectModel>(resource);
			}
		}

		public IEnumerable<ProjectModel> AllProjectCategories()
		{
			foreach (var resource in contentExplorer.Tags["type-project-category"])
			{
				yield return GetOrDeserialize<ProjectModel>(resource);
			}
		}

		public void Dispose()
		{
			contentExplorer.Dispose();
		}

		public T GetOrDeserialize<T>(IResource resource)
		{
			if (!deserializationCache.TryGetValue(resource, out object cached))
			{
				using var stream = resource.Content.LoadStream();
				using var sr = new StreamReader(stream);
				using var jsonReader = new JsonTextReader(sr);

				cached = serializer.Deserialize<T>(jsonReader);

				if (cached is ILoadResourceCallback callback)
				{
					callback.OnAfterDeserializedFrom(this, resource);
				}
			}

			return (T)cached;
		}
	}
}
