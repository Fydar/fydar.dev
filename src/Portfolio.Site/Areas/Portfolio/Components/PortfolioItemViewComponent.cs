using Microsoft.AspNetCore.Mvc;
using Portfolio.Models.Portfolio;

namespace Portfolio.Site.Areas.Portfolio.Components
{
	[ViewComponent(Name = "PortfolioItem")]
	public class PortfolioItemViewComponent : ViewComponent
	{
		public PortfolioItemViewComponent()
		{
		}

		public IViewComponentResult Invoke(ProjectModel project)
		{
			return View("PortfolioItem", project);
		}
	}
}
