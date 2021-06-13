using Newtonsoft.Json;
using RPGCore.Data;
using RPGCore.Packages;
using System;
using System.Collections.Generic;

namespace Portfolio.Models
{
	[EditableType]
	public class ProjectCategoryModel : ILoadResourceCallback, IComparable<ProjectCategoryModel>
	{
		public string DisplayName { get; set; }
		public string Slug { get; set; }
		public string Description { get; set; }
		public string FeaturedImage { get; set; }
		public int Order { get; set; }

		[JsonIgnore] public List<ProjectModel> Projects { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			Projects = new List<ProjectModel>();
		}

		public int CompareTo(ProjectCategoryModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
