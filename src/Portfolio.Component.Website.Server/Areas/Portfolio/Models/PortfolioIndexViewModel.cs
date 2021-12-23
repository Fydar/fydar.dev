using Portfolio.Models.Portfolio;
using System.Collections.Generic;

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
