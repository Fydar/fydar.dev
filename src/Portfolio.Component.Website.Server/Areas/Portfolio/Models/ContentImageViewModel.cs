using RPGCore.Packages;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Models
{
	public class ContentImageViewModel
	{
		public IResourceContent? PlaceholderContent { get; set; }
		public string? ImageUrl { get; set; }
		public bool Pixel { get; set; }
	}
}
