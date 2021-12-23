using Portfolio.Services.Content.Utilities;
using RPGCore.Data;
using RPGCore.Packages;
using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace Portfolio.Services.Content.Portfolio
{
	[EditableType]
	public class ProjectModel : ILoadResourceCallback, IComparable<ProjectModel>
	{
		public string ProjectName { get; set; } = string.Empty;
		public string ProjectSubtitle { get; set; } = string.Empty;
		public string ProjectCategory { get; set; } = string.Empty;
		public string Slug { get; set; } = string.Empty;
		public bool HideOnHomePage { get; set; }
		public bool ShowOnResume { get; set; }
		public string Institution { get; set; } = string.Empty;
		public string FeaturedImage { get; set; } = string.Empty;
		public string HoverImage { get; set; } = string.Empty;
		public string Excerpt { get; set; } = string.Empty;
		public string[] Disciplines { get; set; } = Array.Empty<string>();
		public long PublishedDate { get; set; }
		public string Page { get; set; } = string.Empty;
		public ExternalLinkModel[] Links { get; set; } = Array.Empty<ExternalLinkModel>();
		public BadgeEntry[] Badges { get; set; } = Array.Empty<BadgeEntry>();
		public int Order { get; set; }

		[JsonIgnore] public IResource? PageResource { get; private set; }
		[JsonIgnore] public IResource? FeaturedImageResource { get; private set; }
		[JsonIgnore] public IResource? HoverImageResource { get; private set; }
		[JsonIgnore] public ProjectCategoryModel? ProjectCategoryModel { get; private set; }
		[JsonIgnore] public IResource? ProjectCategoryResource { get; private set; }
		[JsonIgnore] public IResource[]? DisciplineResources { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			PageResource = cache.GetResource(Page);
			FeaturedImageResource = cache.GetResource(FeaturedImage);
			HoverImageResource = cache.GetResource(HoverImage);

			ProjectCategoryResource = cache.GetResource(ProjectCategory);

			if (ProjectCategoryResource == null)
			{
				throw new InvalidOperationException("Failed to determine project category");
			}

			ProjectCategoryModel = cache.GetOrDeserialize<ProjectCategoryModel>(ProjectCategoryResource);

			cache.GetOrDeserialize<ProjectCategoryModel>(ProjectCategoryResource).Projects.Add(this);

			if (Disciplines != null)
			{
				DisciplineResources = Disciplines.Select(discipline =>
				{
					return cache.GetResource(discipline) ?? throw new InvalidOperationException("Failed to determine project discipline.");
				}).ToArray();
				foreach (var discipline in DisciplineResources)
				{
					cache.GetOrDeserialize<DisciplineModel>(discipline).FeaturedProjects.Add(this);
				}
			}
		}

		public int CompareTo(ProjectModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
