using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Services.ContentService;
using Portfolio.Instance.ViewModels;

namespace Portfolio.Instance.Controllers
{
	public class BlogController : Controller
	{
		private readonly IContentService contentService;

		public BlogController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		[Route("/blog")]
		public IActionResult Index()
		{
			return View(new PortfolioIndexViewModel(contentService.Projects));
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
