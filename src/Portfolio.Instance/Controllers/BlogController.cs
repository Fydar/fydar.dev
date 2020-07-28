using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Models;
using Portfolio.Instance.Services.ContentService;

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
			return View(new PortfolioIndexViewModel()
			{
				AllProjects = contentService.Projects
			});
		}

		[Route("/blog/{identifier}")]
		public IActionResult Item(string identifier)
		{
			var category = contentService.GetCategory(identifier);
			if (category != null)
			{
				return View("Category", new CategoryViewModel()
				{
					Category = category
				});
			}

			var project = contentService.GetProject(identifier);
			if (project != null)
			{
				return View("Project", new ProjectViewModel()
				{
					Project = project
				});
			}

			return NotFound();
		}
	}
}
