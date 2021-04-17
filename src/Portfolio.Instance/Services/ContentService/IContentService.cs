using Portfolio.Models;
using Portfolio.Models.Blog;
using RPGCore.Packages;
using System;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.ContentService
{
	public interface IContentService
	{
		List<ProjectModel> Projects { get; }
		List<ProjectCategoryModel> Categories { get; }
		List<BlogPostModel> BlogPosts { get; }
		List<DisciplineModel> Disciplines { get; }
		List<CompanyModel> Companies { get; }
		List<CollegeModel> Colleges { get; }

		int ProfessionalMonths { get; }
		int ProfessionalTotalMonths { get; }
		int ProfessionalYears { get; }
		string ProfessionalExperience { get; }

		IResource GetResource(string fullname);
		ProjectModel GetProject(string slug);
		ProjectCategoryModel GetCategory(string slug);
		DisciplineModel GetDiscipline(string slug);
		BlogPostModel GetBlogPost(string slug);
	}
}
