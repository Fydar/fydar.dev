using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.Instance.Models
{
	public class PortfolioIndexViewModel
	{
		public IReadOnlyList<ProjectModel> AllProjects { get; set; }
	}
}
