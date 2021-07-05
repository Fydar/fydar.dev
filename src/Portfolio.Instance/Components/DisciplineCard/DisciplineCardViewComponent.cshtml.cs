using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.ViewModels;
using System.Threading.Tasks;

namespace Portfolio.Instance.Components.DisciplineCard
{
	[ViewComponent(Name = "DisciplineCard")]
	public class DisciplineCardViewComponent : ViewComponent
	{
		public DisciplineCardViewComponent()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(DisciplineViewModel discipline)
		{
			return View("DisciplineCardViewComponent", discipline);
		}
	}
}
