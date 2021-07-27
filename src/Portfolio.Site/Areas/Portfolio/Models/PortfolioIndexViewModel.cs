using Portfolio.Models;
using System.Collections.Generic;
using Portfolio.Models.Portfolio;

namespace Portfolio.Site.Areas.Portfolio.Models
{
	public class PortfolioIndexViewModel
	{
		public IReadOnlyList<ProjectModel> AllProjects { get; set; }

		public PortfolioIndexViewModel(IReadOnlyList<ProjectModel> allProjects)
		{
			AllProjects = allProjects;
		}
	}
}
