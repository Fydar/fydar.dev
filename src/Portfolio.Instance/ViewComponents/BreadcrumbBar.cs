using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Models;
using Portfolio.Instance.ViewModels;
using System.Threading.Tasks;

namespace Portfolio.Instance.ViewComponents
{
	public class BreadcrumbBar : ViewComponent
	{
		public BreadcrumbBar()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(BreadcrumbBarViewModel breadcrumbBarViewModel)
		{
			return View(breadcrumbBarViewModel);
		}
	}
}
