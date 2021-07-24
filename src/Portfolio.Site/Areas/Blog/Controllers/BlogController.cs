using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Site.Services.ContentService;

namespace Portfolio.Site.Areas.Blog.Controllers
{
	[ApiController]
	[Area("Blog")]
	[Route("/blog")]
	[ApiExplorerSettings(GroupName = "Blog")]
	public class BlogController : Controller
	{
		private readonly IContentService contentService;

		public BlogController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Index()
		{
			return View("Index", new PortfolioIndexViewModel(contentService.Projects));
		}
	}
}
