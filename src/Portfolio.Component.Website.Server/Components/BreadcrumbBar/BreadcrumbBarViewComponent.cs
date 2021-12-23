using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.ViewModels;

namespace Portfolio.Component.Website.Server.Components.BreadcrumbBar
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
