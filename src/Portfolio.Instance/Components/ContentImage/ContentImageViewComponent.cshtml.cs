using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Services.ContentService;
using Portfolio.Instance.ViewModels;
using Portfolio.Models;
using System.Threading.Tasks;

namespace Portfolio.Instance.Components.DisciplineCard
{
	[ViewComponent(Name = "ContentImage")]
	public class ContentImageViewComponent : ViewComponent
	{
		private readonly IContentService contentService;

		public ContentImageViewComponent(IContentService contentService)
		{
			this.contentService = contentService;
		}

		public async Task<IViewComponentResult> InvokeAsync(
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
				var placeholderContent = contentService.GetResource(placeholderContentName).Content;
				imageModel.PlaceholderContent = placeholderContent;
			}

			return View("ContentImageViewComponent", imageModel);
		}
	}
}
