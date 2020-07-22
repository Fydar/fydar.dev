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
	public class ProjectModel : ILoadResourceCallback, IComparable<ProjectModel>
	{
		public string ProjectName { get; set; }
		public string ProjectSubtitle { get; set; }
		public string ProjectCategory { get; set; }
		public string Slug { get; set; }
		public string Institution { get; set; }
		public string FeaturedImage { get; set; }
		public string HoverImage { get; set; }
		public string Excerpt { get; set; }
		public string[] Skills { get; set; }
		public long PublishedDate { get; set; }
		public string Page { get; set; }
		public ExternalLinkModel[] Links { get; set; }
		public int Order { get; set; }

		[JsonIgnore] public IResource PageResource { get; set; }
		[JsonIgnore] public IResource ProjectCategoryResource { get; set; }
		[JsonIgnore] public IResource[] SkillsPages { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			PageResource = cache.GetResource(Page);

			ProjectCategoryResource = cache.GetResource(ProjectCategory);
			cache.GetOrDeserialize<ProjectCategoryModel>(ProjectCategoryResource).Projects.Add(this);

			SkillsPages = Skills.Select(tag => cache.GetResource(tag)).ToArray();
			foreach (var skillPage in SkillsPages)
			{
				cache.GetOrDeserialize<ProjectCategoryModel>(skillPage).Projects.Add(this);
			}
		}

		public int CompareTo(ProjectModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
