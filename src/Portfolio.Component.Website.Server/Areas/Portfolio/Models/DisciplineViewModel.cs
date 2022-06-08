using Portfolio.Component.Website.Server.Services.PageMetaProvider;
using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Models;

public class DisciplineViewModel : StaticPageViewModel
{
	public DisciplineModel Discipline { get; set; }

	public DisciplineViewModel(DisciplineModel discipline)
	{
		Discipline = discipline;
	}
}
