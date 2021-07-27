using RPGCore.Data;
using RPGCore.Packages;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Portfolio.Models.Portfolio;
using Portfolio.Models;
using Portfolio.Models.Utilities;

namespace Portfolio.Models.Portfolio
{
	[EditableType]
	public class DisciplineModel : ILoadResourceCallback, IComparable<DisciplineModel>
	{
		public string DisplayName { get; set; } = string.Empty;
		public string Slug { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public string FeaturedImage { get; set; } = string.Empty;
		public string IconImage { get; set; } = string.Empty;
		public string Page { get; set; } = string.Empty;
		public bool ShowOnHomePage { get; set; }
		public int Order { get; set; }

		[JsonIgnore] public IResource? PageResource { get; private set; }
		[JsonIgnore] public List<ProjectModel> FeaturedProjects { get; private set; } = new List<ProjectModel>();

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			PageResource = cache.GetResource(Page);
		}

		public int CompareTo(DisciplineModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
