using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Site.Components.PortfolioItem
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
