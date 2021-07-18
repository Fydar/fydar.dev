using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Site.Services.ContentService;
using Portfolio.Site.ViewModels;

namespace Portfolio.Site.Areas.Portfolio.Controllers
{
	[ApiController]
	[Area("Portfolio")]
	[Route("/portfolio/{identifier}")]
	public class PortfolioItemController : Controller
	{
		private readonly IContentService contentService;

		public PortfolioItemController(IContentService contentService)
		{
			this.contentService = contentService;
		}

		[HttpGet]
		public IActionResult Index([FromRoute] string identifier)
		{
			var category = contentService.GetCategory(identifier);
			if (category != null)
			{
				return View("Category", new CategoryViewModel(category));
			}

			var project = contentService.GetProject(identifier);
			if (project != null)
			{
				return View("Project", new ProjectViewModel(project));
			}

			var discipline = contentService.GetDiscipline(identifier);
			if (discipline != null)
			{
				return View("Discipline", new DisciplineViewModel(discipline));
			}

			return NotFound();
		}
	}
}
