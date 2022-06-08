using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Components;

[ViewComponent(Name = "PortfolioGrid")]
public class PortfolioGridViewComponent : ViewComponent
{
	public PortfolioGridViewComponent()
	{
	}

	public IViewComponentResult Invoke(ProjectCategoryModel category)
	{
		return View("PortfolioGrid", category);
	}
}
