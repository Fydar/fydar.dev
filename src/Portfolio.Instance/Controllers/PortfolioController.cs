using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Models;
using Portfolio.Instance.Services.ContentService;

namespace Portfolio.Instance.Controllers
{
	public class PortfolioController : Controller
	{
		private readonly IContentService contentService;

		public PortfolioController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		[Route("/")]
		[Route("/portfolio")]
		public IActionResult Index()
		{
			return View(new PortfolioIndexViewModel()
			{
				AllProjects = contentService.Projects
			});
		}

		[Route("/portfolio/{projectIdentifier}")]
		public IActionResult Item(string projectIdentifier)
		{
			var portfolioItem = contentService.GetProject(projectIdentifier);

			if (portfolioItem == null)
			{
				return NotFound();
			}

			return View(new PortfolioItemViewModel()
			{
				Project = portfolioItem
			});
		}
	}
}
