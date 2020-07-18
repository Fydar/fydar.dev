using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.ContentService
{
	public interface IContentService
	{
		IEnumerable<ProjectModel> AllProjects();
	}
}
