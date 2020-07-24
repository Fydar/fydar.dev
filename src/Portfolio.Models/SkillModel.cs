using Newtonsoft.Json;
using RPGCore.Behaviour;
using RPGCore.Packages;
using System;
using System.Collections.Generic;

namespace Portfolio.Models
{
	[EditorType]
	public class SkillModel : ILoadResourceCallback, IComparable<SkillModel>
	{
		public string DisplayName { get; set; }
		public string Slug { get; set; }
		public string Description { get; set; }
		public int Order { get; set; }

		[JsonIgnore] public List<ProjectModel> Projects { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			Projects = new List<ProjectModel>();
		}

		public int CompareTo(SkillModel other)
		{
			return Order.CompareTo(other?.Order ?? 0);
		}
	}
}
