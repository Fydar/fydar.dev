using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Services.ContentService;

namespace Portfolio.Site.Areas.Blog.Controllers
{
	[Area("Blog")]
	public class BlogPostController : Controller
	{
		private readonly IContentService contentService;

		public BlogPostController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		[Route("/blog/{identifier}")]
		public IActionResult Item(string identifier)
		{
			var blogPost = contentService.GetBlogPost(identifier);
			if (blogPost != null)
			{
				return View("BlogPost", blogPost);
			}

			return NotFound();
		}
	}
}
