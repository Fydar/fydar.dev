using Newtonsoft.Json;
using Portfolio.Services.Content.Portfolio;
using Portfolio.Services.Content.Utilities;
using RPGCore.Packages;

namespace Portfolio.Services.Content.Blog
{
	public class BlogPostModel
	{
		public string PostTitle { get; set; } = string.Empty;
		public string PostSubtitle { get; set; } = string.Empty;
		public string Slug { get; set; } = string.Empty;
		public string FeaturedImage { get; set; } = string.Empty;
		public string Excerpt { get; set; } = string.Empty;
		public long PublishedDate { get; set; }
		public string Page { get; set; } = string.Empty;

		[JsonIgnore] public IResource? PageResource { get; private set; }
		[JsonIgnore] public IResource? FeaturedImageResource { get; private set; }

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
