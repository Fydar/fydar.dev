using Newtonsoft.Json;
using RPGCore.Behaviour.Manifest;
using RPGCore.Packages;

namespace Portfolio.Models
{
	[EditorType]
	public class ProjectCategoryModel : ILoadResourceCallback
	{
		public string DisplayName { get; set; }
		public string Slug { get; set; }

		[JsonIgnore] public ProjectModel[] Projects { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			foreach (var dependantResource in resource.Dependencies)
			{

			}
		}
	}
}
