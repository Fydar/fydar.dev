using Microsoft.AspNetCore.Mvc;
using Portfolio.Models;
using System.Threading.Tasks;

namespace Portfolio.Instance.Components.PortfolioItem
{
	[ViewComponent(Name = "PortfolioItem")]
	public class PortfolioItemViewComponent : ViewComponent
	{
		public PortfolioItemViewComponent()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(ProjectModel project)
		{
			return View("PortfolioItemViewComponent", project);
		}
	}
}
