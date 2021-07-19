using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Portfolio.Models;
using Portfolio.Site.Services.ContentService;
using Portfolio.Site.Services.PageMetaProvider;
using Portfolio.Site.ViewModels;

namespace Portfolio.Site.Areas.Portfolio.Controllers
{
	[ApiController]
	[Area("Portfolio")]
	[Route("/portfolio/{identifier}")]
	public class PortfolioItemController : Controller
	{
		private readonly IContentService contentService;
		private readonly IPageMetadataTransformer<ProjectViewModel> pageMetadataTransformer;

		public PortfolioItemController(
			IContentService contentService,
			IPageMetadataTransformer<ProjectViewModel> pageMetadataTransformer)
		{
			this.contentService = contentService;
			this.pageMetadataTransformer = pageMetadataTransformer;
		}

		[HttpGet]
		public IActionResult Index([FromRoute] string identifier)
		{
			var category = contentService.GetCategory(identifier);
			if (category != null)
			{
				var categoryViewModel = new CategoryViewModel(category);

				return View("Category", categoryViewModel);
			}

			var project = contentService.GetProject(identifier);
			if (project != null)
			{
				var projectViewModel = new ProjectViewModel(project);
				ViewData.SetPageMetadata(pageMetadataTransformer.TransformMetadata(projectViewModel));

				return View("Project", projectViewModel);
			}

			var discipline = contentService.GetDiscipline(identifier);
			if (discipline != null)
			{
				var disciplineViewModel = new DisciplineViewModel(discipline);

				return View("Discipline", disciplineViewModel);
			}

			return NotFound();
		}
	}
}
