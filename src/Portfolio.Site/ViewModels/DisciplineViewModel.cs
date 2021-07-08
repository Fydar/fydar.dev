using Portfolio.Models;

namespace Portfolio.Site.ViewModels
{
	public class DisciplineViewModel
	{
		public DisciplineModel Discipline { get; set; }

		public DisciplineViewModel(DisciplineModel discipline)
		{
			Discipline = discipline;
		}
	}
}
