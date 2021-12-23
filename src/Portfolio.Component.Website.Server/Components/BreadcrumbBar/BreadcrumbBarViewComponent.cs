using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.ViewModels;

namespace Portfolio.Site.Components.BreadcrumbBar
{
	[ViewComponent(Name = "BreadcrumbBar")]
	public class BreadcrumbBarViewComponent : ViewComponent
	{
		public BreadcrumbBarViewComponent()
		{
		}

		public IViewComponentResult Invoke(BreadcrumbBarViewModel breadcrumbBar)
		{
			return View("BreadcrumbBar", breadcrumbBar);
		}
	}
}
