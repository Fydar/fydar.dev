using Portfolio.Models;
using RPGCore.Packages;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.ContentService
{
	public interface IContentService
	{
		List<ProjectModel> Projects { get; }
		List<ProjectCategoryModel> Categories { get; }
		List<DisciplineModel> Disciplines { get; }

		IResource GetResource(string fullname);
		ProjectModel GetProject(string slug);
		ProjectCategoryModel GetCategory(string slug);
		DisciplineModel GetDiscipline(string slug);
	}
}
