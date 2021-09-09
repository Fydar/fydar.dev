using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Portfolio;

namespace Portfolio.Site.Areas.Portfolio.Components
{
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
}
