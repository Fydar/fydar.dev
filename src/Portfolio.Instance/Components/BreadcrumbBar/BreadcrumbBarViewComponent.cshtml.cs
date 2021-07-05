using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;

namespace Portfolio.Instance.Components.BreadcrumbBar
{
	[ViewComponent(Name = "BreadcrumbBar")]
	public class BreadcrumbBarViewComponent : ViewComponent
	{
		public BreadcrumbBarViewComponent()
		{
		}

		public IViewComponentResult Invoke(BreadcrumbBarViewModel breadcrumbBar)
		{
			return View("BreadcrumbBarViewComponent", breadcrumbBar);
		}
	}
}
