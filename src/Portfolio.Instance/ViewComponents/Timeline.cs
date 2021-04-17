using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;
using System.Threading.Tasks;

namespace Portfolio.Instance.ViewComponents
{
	public class Timeline : ViewComponent
	{
		public Timeline()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(TimelineViewModel timelineViewModel)
		{
			return View(timelineViewModel);
		}
	}
}
