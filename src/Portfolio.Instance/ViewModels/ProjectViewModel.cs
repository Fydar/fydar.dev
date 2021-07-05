using Portfolio.Models;

namespace Portfolio.Instance.ViewModels
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
