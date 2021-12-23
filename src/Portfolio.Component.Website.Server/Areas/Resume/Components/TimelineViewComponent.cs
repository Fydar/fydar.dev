using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Resume.Models;

namespace Portfolio.Site.Areas.Resume.Components
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
