using Newtonsoft.Json;
using RPGCore.Behaviour;
using RPGCore.Packages;
using System;
using System.Collections.Generic;

namespace Portfolio.Models
{
	[EditorType]
	public class DisciplineModel : ILoadResourceCallback, IComparable<DisciplineModel>
	{
		public string DisplayName { get; set; }
		public string Slug { get; set; }
		public string Description { get; set; }
		public string FeaturedImage { get; set; }
		public string IconImage { get; set; }
		public string Page { get; set; }
		public bool ShowOnHomePage { get; set; }
		public int Order { get; set; }

		[JsonIgnore] public IResource PageResource { get; private set; }
		[JsonIgnore] public List<ProjectModel> FeaturedProjects { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			FeaturedProjects = new List<ProjectModel>();

			PageResource = cache.GetResource(Page);
		}

		public int CompareTo(DisciplineModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
