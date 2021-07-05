using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.Instance.ViewModels
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
