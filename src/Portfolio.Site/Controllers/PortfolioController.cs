using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Services.ContentService;
using Portfolio.Site.ViewModels;

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
			return View(new PortfolioIndexViewModel(contentService.Projects));
		}

		[Route("/portfolio/{identifier}")]
		public IActionResult Item(string identifier)
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
