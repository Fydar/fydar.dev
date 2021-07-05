using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;
using System.Threading.Tasks;

namespace Portfolio.Instance.Components.Timeline
{
	[ViewComponent(Name = "Timeline")]
	public class TimelineViewComponent : ViewComponent
	{
		public TimelineViewComponent()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(TimelineViewModel timeline)
		{
			return View("TimelineViewComponent", timeline);
		}
	}
}
