using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.ViewModels;

namespace Portfolio.Site.Components.Timeline
{
	[ViewComponent(Name = "Timeline")]
	public class TimelineViewComponent : ViewComponent
	{
		public TimelineViewComponent()
		{
		}

		public IViewComponentResult Invoke(TimelineViewModel timeline)
		{
			return View("Timeline", timeline);
		}
	}
}
