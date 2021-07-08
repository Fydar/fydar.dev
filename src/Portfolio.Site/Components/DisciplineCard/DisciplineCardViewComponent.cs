using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.ViewModels;

namespace Portfolio.Site.Components.DisciplineCard
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
