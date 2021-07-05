using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;

namespace Portfolio.Instance.Components.DisciplineCard
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
