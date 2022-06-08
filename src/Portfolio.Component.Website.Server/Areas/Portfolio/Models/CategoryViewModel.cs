using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Areas.Portfolio.Models;

public class CategoryViewModel
{
	public ProjectCategoryModel Category { get; set; }

	public CategoryViewModel(ProjectCategoryModel category)
	{
		Category = category;
	}
}
