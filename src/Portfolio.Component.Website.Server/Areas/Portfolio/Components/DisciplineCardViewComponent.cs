using Microsoft.AspNetCore.Mvc;
using Portfolio.Component.Website.Server.Areas.Portfolio.Models;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Components;

[ViewComponent(Name = "DisciplineCard")]
public class DisciplineCardViewComponent : ViewComponent
{
	public DisciplineCardViewComponent()
	{
	}

	public IViewComponentResult Invoke(DisciplineViewModel discipline)
	{
		return View("DisciplineCardViewComponent", discipline);
	}
}
