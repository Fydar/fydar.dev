using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;

namespace Portfolio.Instance.Components.PortfolioItem
{
	[ViewComponent(Name = "PortfolioItem")]
	public class PortfolioItemViewComponent : ViewComponent
	{
		public PortfolioItemViewComponent()
		{
		}

		public IViewComponentResult Invoke(ProjectModel project)
		{
			return View("PortfolioItemViewComponent", project);
		}
	}
}
