using Newtonsoft.Json;
using RPGCore.Packages;

namespace Portfolio.Models.Blog
{
	public class BlogPostModel
	{
		public string PostTitle { get; set; }
		public string PostSubtitle { get; set; }
		public string Slug { get; set; }
		public string FeaturedImage { get; set; }
		public string Excerpt { get; set; }
		public long PublishedDate { get; set; }
		public string Page { get; set; }

		[JsonIgnore] public IResource PageResource { get; private set; }
		[JsonIgnore] public IResource FeaturedImageResource { get; private set; }

		public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
		{
			PageResource = cache.GetResource(Page);
			FeaturedImageResource = cache.GetResource(FeaturedImage);
		}

		public int CompareTo(ProjectModel other)
		{
			return PublishedDate.CompareTo(other?.PublishedDate ?? 0);
		}
	}
}
