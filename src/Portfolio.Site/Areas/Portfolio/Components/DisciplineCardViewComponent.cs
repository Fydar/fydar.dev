using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.ViewModels;
using Portfolio.Site.Areas.Portfolio.Models;

namespace Portfolio.Site.Areas.Portfolio.Components
{
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
}
