using Newtonsoft.Json;
using Portfolio.Models.Blog;
using Portfolio.Models.Portfolio;
using Portfolio.Models.Portfolio.Places;
using Portfolio.Models.Utilities;
using RPGCore.Packages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Portfolio.Services.Content
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
		public List<CompanyModel> Companies { get; }
		public List<CollegeModel> Colleges { get; }

		public string ProfessionalExperience
		{
			get
			{
				var sb = new StringBuilder();
				if (ProfessionalYears == 1)
				{
					sb.Append("1 year");
				}
				else if (ProfessionalYears != 0)
				{
					sb.Append($"{ProfessionalYears} years");
				}

				if (ProfessionalMonths != 0 && ProfessionalYears != 0)
				{
					sb.Append(", ");
				}

				if (ProfessionalMonths == 1)
				{
					sb.Append("1 month");
				}
				else if (ProfessionalMonths != 0)
				{
					sb.Append($"{ProfessionalMonths} months");
				}
				return sb.ToString();
			}
		}

		public int ProfessionalTotalMonths
		{
			get
			{
				int total = 0;
				foreach (var company in Companies)
				{
					if (company.IsProfessional)
					{
						total += (int)Math.Ceiling(company.Elapsed.TotalDays / 30.4167);
					}
				}
				return total;
			}
		}

		public int ProfessionalMonths => ProfessionalTotalMonths % 12;
		public int ProfessionalYears => ProfessionalTotalMonths / 12;

		public LocalContentService(IExplorer content)
		{
			contentExplorer = content;

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

			Companies = DeserializeAll<CompanyModel>("type-company");
			foreach (var company in Companies)
			{
				company.Positions.Sort();
				company.Positions.Reverse();
			}
			Companies.Sort();
			Companies.Reverse();

			Colleges = DeserializeAll<CollegeModel>("type-college");
			foreach (var college in Colleges)
			{
				college.Positions.Sort();
				college.Positions.Reverse();
			}
			Colleges.Sort();
			Colleges.Reverse();
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

		public ProjectModel? GetProject(string slug)
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

		public ProjectCategoryModel? GetCategory(string slug)
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

		public DisciplineModel? GetDiscipline(string slug)
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

		public BlogPostModel? GetBlogPost(string slug)
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

		public T GetOrDeserialize<T>(IResource resource)
		{
			lock (deserializationCache)
			{
				if (!deserializationCache.TryGetValue(resource, out object? cached))
				{
					using var stream = resource.Content.OpenRead();
					using var sr = new StreamReader(stream);
					using var jsonReader = new JsonTextReader(sr);

					var deserialized = serializer.Deserialize<T>(jsonReader);
					if (deserialized == null)
					{
						throw new InvalidOperationException("Failed to deserialize resource content");
					}

					cached = deserialized;
					deserializationCache.Add(resource, cached);

					if (cached is ILoadResourceCallback callback)
					{
						callback.OnAfterDeserializedFrom(this, resource);
					}
				}
				return (T)cached;
			}
		}

		public IResource? GetResource(string fullname)
		{
			if (!string.IsNullOrEmpty(fullname))
			{
				return contentExplorer.Resources[fullname];
			}
			return null;
		}

		public void Dispose()
		{
			contentExplorer.Dispose();
		}
	}
}
