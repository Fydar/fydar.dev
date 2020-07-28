using Microsoft.AspNetCore.Mvc;
using Portfolio.Instance.Models;
using System.Threading.Tasks;

namespace Portfolio.Instance.ViewComponents
{
	public class DisciplineCard : ViewComponent
	{
		public DisciplineCard()
		{
		}

		public async Task<IViewComponentResult> InvokeAsync(DisciplineViewModel disciplineViewModel)
		{
			return View(disciplineViewModel);
		}
	}
}
