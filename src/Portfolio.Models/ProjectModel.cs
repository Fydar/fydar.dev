using Newtonsoft.Json;
using RPGCore.Behaviour.Manifest;
using RPGCore.Packages;
using System.Linq;

namespace Portfolio.Models
{
	[EditorType]
	public class ProjectModel : ILoadResourceCallback
	{
		public string ProjectName { get; set; }
		public string Slug { get; set; }
		public string Institution { get; set; }
		public string FeaturedImage { get; set; }
		public string Excerpt { get; set; }
		public string[] Skills { get; set; }
		public long PublishedDate { get; set; }
		public MarkupElementModel Page { get; set; }

		[JsonIgnore] public string[] Tags { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			Tags = resource.Tags.ToArray();
		}
	}
}
