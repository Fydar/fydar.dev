using Portfolio.Models;

namespace Portfolio.Instance.ViewModels
{
	public class CategoryViewModel
	{
		public ProjectCategoryModel Category { get; set; }

		public CategoryViewModel(ProjectCategoryModel category)
		{
			Category = category;
		}
	}
}
