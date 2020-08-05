using Newtonsoft.Json;
using RPGCore.Behaviour;
using RPGCore.Packages;
using System;
using System.Linq;

namespace Portfolio.Models
{
	[EditorType]
	public struct ExternalLinkModel
	{
		public string SiteName { get; set; }
		public string Url { get; set; }
		public string Icon { get; set; }
	}

	[EditorType]
	public struct BadgeEntry
	{
		public string Content { get; set; }
		public string Url { get; set; }
		public string Tooltip { get; set; }
		public string Icon { get; set; }
		public bool DisplayOnHomepage { get; set; }
	}

	[EditorType]
	public class ProjectModel : ILoadResourceCallback, IComparable<ProjectModel>
	{
		public string ProjectName { get; set; }
		public string ProjectSubtitle { get; set; }
		public string ProjectCategory { get; set; }
		public string Slug { get; set; }
		public bool HideOnHomePage { get; set; }
		public string Institution { get; set; }
		public string FeaturedImage { get; set; }
		public string HoverImage { get; set; }
		public string Excerpt { get; set; }
		public string[] Disciplines { get; set; }
		public long PublishedDate { get; set; }
		public string Page { get; set; }
		public ExternalLinkModel[] Links { get; set; }
		public BadgeEntry[] Badges { get; set; }
		public int Order { get; set; }

		[JsonIgnore] public IResource PageResource { get; private set; }
		[JsonIgnore] public IResource FeaturedImageResource { get; private set; }
		[JsonIgnore] public IResource HoverImageResource { get; private set; }
		[JsonIgnore] public ProjectCategoryModel ProjectCategoryModel { get; private set; }
		[JsonIgnore] public IResource ProjectCategoryResource { get; private set; }
		[JsonIgnore] public IResource[] DisciplineResources { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			PageResource = cache.GetResource(Page);
			FeaturedImageResource = cache.GetResource(FeaturedImage);
			HoverImageResource = cache.GetResource(HoverImage);

			ProjectCategoryResource = cache.GetResource(ProjectCategory);
			ProjectCategoryModel = cache.GetOrDeserialize<ProjectCategoryModel>(ProjectCategoryResource);
			cache.GetOrDeserialize<ProjectCategoryModel>(ProjectCategoryResource).Projects.Add(this);

			if (Disciplines != null)
			{
				DisciplineResources = Disciplines.Select(tag => cache.GetResource(tag)).ToArray();
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
