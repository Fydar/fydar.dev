using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;

namespace Portfolio.Instance.Components.Timeline
{
	[ViewComponent(Name = "Timeline")]
	public class TimelineViewComponent : ViewComponent
	{
		public TimelineViewComponent()
		{
		}

		public IViewComponentResult Invoke(TimelineViewModel timeline)
		{
			return View("TimelineViewComponent", timeline);
		}
	}
}
