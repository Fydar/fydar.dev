using Portfolio.Models;
using Portfolio.Models.Portfolio;

namespace Portfolio.Site.Areas.Portfolio.Models
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
