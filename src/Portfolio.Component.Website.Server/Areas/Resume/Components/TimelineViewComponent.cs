using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.Areas.Resume.Models;

namespace Portfolio.Component.Website.Server.Areas.Resume.Components;

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
