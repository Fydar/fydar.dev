using Portfolio.Models;

namespace Portfolio.Site.Areas.Portfolio.Models
{
	public class ProjectViewModel
	{
		public ProjectModel Project { get; set; }

		public ProjectViewModel(ProjectModel project)
		{
			Project = project;
		}
	}
}
