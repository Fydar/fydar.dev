using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Models;

public class DisciplineViewModel
{
	public DisciplineModel Discipline { get; set; }

	public DisciplineViewModel(DisciplineModel discipline)
	{
		Discipline = discipline;
	}
}
