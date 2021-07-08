using RPGCore.Packages;

namespace Portfolio.Site.ViewModels
{
	public class ContentImageViewModel
	{
		public IResourceContent? PlaceholderContent { get; set; }
		public string? ImageUrl { get; set; }
		public bool Pixel { get; set; }
	}
}
