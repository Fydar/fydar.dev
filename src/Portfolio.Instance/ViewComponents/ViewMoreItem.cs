using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Models;
using System.Threading.Tasks;

namespace Portfolio.Instance.ViewComponents
{
	public class ViewMoreItem : ViewComponent
	{
		public ViewMoreItem()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(ViewMoreViewModel projectModel)
		{
			return View(projectModel);
		}
	}
}
