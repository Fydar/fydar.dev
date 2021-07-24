using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Site.Services.ContentService;

namespace Portfolio.Site.Areas.Portfolio.Controllers
{
	[ApiController]
	[Area("Portfolio")]
	[Route("/")]
	[Route("/portfolio")]
	[ApiExplorerSettings(GroupName = "Portfolio")]
	public class PortfolioController : Controller
	{
		private readonly IContentService contentService;

		public PortfolioController(IContentService contentService)
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
