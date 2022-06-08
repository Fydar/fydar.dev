using Portfolio.Services.Content.Portfolio;
using System.Collections.Generic;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Models;

public class PortfolioIndexViewModel
{
	public IReadOnlyList<ProjectModel> AllProjects { get; set; }

	public PortfolioIndexViewModel(IReadOnlyList<ProjectModel> allProjects)
	{
		AllProjects = allProjects;
	}
}
