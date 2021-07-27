using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using Portfolio.Services.Content;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Models.Utilities;

namespace Portfolio.Site.Components.ContentImage
{
	[ViewComponent(Name = "ContentImage")]
	public class ContentImageViewComponent : ViewComponent
	{
		private readonly IContentService contentService;

		public ContentImageViewComponent(IContentService contentService)
		{
			this.contentService = contentService;
		}

		public IViewComponentResult Invoke(
			string image,
			string size,
			bool usePlaceholder = true,
			bool pixel = true)
		{
			var imageModel = new ContentImageViewModel()
			{
				Pixel = pixel
			};

			if (size != "blur")
			{
				string imageUrl = "/" + ResourceHelper.TransformName(image, size);
				imageModel.ImageUrl = imageUrl;
			}

			if (usePlaceholder)
			{
				string placeholderContentName = ResourceHelper.TransformName(image, "blur");
				var placeholderContent = contentService.GetResource(placeholderContentName)?.Content;
				imageModel.PlaceholderContent = placeholderContent;
			}

			return View("ContentImage", imageModel);
		}
	}
}
