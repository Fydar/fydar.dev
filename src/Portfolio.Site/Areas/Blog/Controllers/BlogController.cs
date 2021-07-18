using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Site.Services.ContentService;

namespace Portfolio.Site.Areas.Blog.Controllers
{
	[Area("Blog")]
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
			return View("Index", new PortfolioIndexViewModel(contentService.Projects));
		}
	}
}
