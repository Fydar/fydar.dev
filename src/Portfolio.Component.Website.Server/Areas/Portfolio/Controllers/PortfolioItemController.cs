using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.Areas.Portfolio.Models;
using Portfolio.Component.Website.Server.Services.PageMetaProvider;
using Portfolio.Component.Website.Server.ViewModels;
using Portfolio.Services.Content;
using System;
using System.Collections.Generic;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Controllers;

[ApiController]
[Area("Portfolio")]
[Route("/portfolio/{identifier}")]
[ApiExplorerSettings(GroupName = "Portfolio")]
public class PortfolioItemController : Controller
{
	private readonly IContentService contentService;

	public PortfolioItemController(
		IContentService contentService)
	{
		this.contentService = contentService;
	}

	/// <summary>
	/// The website page for a portfolio item.
	/// </summary>
	/// <returns>A view representing the page.</returns>
	/// <response code="200">A portfolio item page.</response>
	/// <response code="404">When no portfolio item with the identifier could be found.</response>
	[HttpGet]
	public IActionResult Index([FromRoute] string identifier)
	{
		var category = contentService.GetCategory(identifier);
		if (category != null)
		{
			var categoryViewModel = new CategoryViewModel(category)
			{
				Breadcrumbs = new StaticPageBreadcrumbs(
				  new BreadcrumbViewModel()
				  {
					  PageUrl = Url.Action("Index", "Portfolio"),
					  Text = "Portfolio"
				  },
				  new BreadcrumbViewModel()
				  {
					  Text = category.DisplayName
				  }
				)
			};

			return View("Category", categoryViewModel);
		}

		var project = contentService.GetProject(identifier);
		if (project != null)
		{
			var projectViewModel = new ProjectViewModel(project)
			{
				Metadata = ProjectViewModelPageMetadataTransformer.TransformMetadata(project),
				Breadcrumbs = new StaticPageBreadcrumbs(
				  new BreadcrumbViewModel()
				  {
					  PageUrl = Url.Action("Index", "Portfolio"),
					  Text = "Portfolio"
				  },
				  new BreadcrumbViewModel()
				  {
					  Text = project.ProjectName
				  }
				)
			};

			return View("Project", projectViewModel);
		}

		var discipline = contentService.GetDiscipline(identifier);
		if (discipline != null)
		{
			var disciplineViewModel = new DisciplineViewModel(discipline)
			{
				Breadcrumbs = new StaticPageBreadcrumbs(
				  new BreadcrumbViewModel()
				  {
					  PageUrl = Url.Action("Index", "Portfolio"),
					  Text = "Portfolio"
				  },
				  new BreadcrumbViewModel()
				  {
					  Text = discipline.DisplayName
				  }
				)
			};

			return View("Discipline", disciplineViewModel);
		}

		return NotFound();
	}
}
