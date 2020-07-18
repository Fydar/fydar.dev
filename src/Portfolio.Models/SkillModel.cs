using Newtonsoft.Json;
using RPGCore.Behaviour;
using RPGCore.Packages;
using System.Collections.Generic;

namespace Portfolio.Models
{
	[EditorType]
	public class SkillModel : ILoadResourceCallback
	{
		public string DisplayName { get; set; }
		public string Slug { get; set; }
		public string Description { get; set; }

		[JsonIgnore] public List<ProjectModel> Projects { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			Projects = new List<ProjectModel>();
			foreach (var dependantResource in resource.Dependencies)
			{

			}
		}
	}
}
