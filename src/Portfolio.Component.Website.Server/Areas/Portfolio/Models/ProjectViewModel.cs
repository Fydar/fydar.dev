using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Models
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
