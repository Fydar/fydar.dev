using Microsoft.AspNetCore.Mvc;
using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Components
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
