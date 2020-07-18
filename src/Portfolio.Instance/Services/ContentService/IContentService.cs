using Portfolio.Models;
using RPGCore.Packages;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.ContentService
{
	public interface IContentService
	{
		IResource GetResource(string fullname);
		IEnumerable<ProjectModel> AllProjects();
		ProjectModel GetProject(string slug);
	}
}
