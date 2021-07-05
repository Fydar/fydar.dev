using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;
using System.Threading.Tasks;

namespace Portfolio.Instance.Components.BreadcrumbBar
{
	[ViewComponent(Name = "BreadcrumbBar")]
	public class BreadcrumbBarViewComponent : ViewComponent
	{
		public BreadcrumbBarViewComponent()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(BreadcrumbBarViewModel breadcrumbBar)
		{
			return View("BreadcrumbBarViewComponent", breadcrumbBar);
		}
	}
}
