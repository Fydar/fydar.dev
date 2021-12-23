using Portfolio.Models.Portfolio;

namespace Portfolio.Site.Areas.Portfolio.Models
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
