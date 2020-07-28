namespace Portfolio.Instance.Models
{
	public class BreadcrumbBarViewModel
	{
		public BreadcrumbModel[] Breadcrumbs { get; set; }

		public BreadcrumbBarViewModel(params BreadcrumbModel[] breadcrumbs)
		{
			Breadcrumbs = breadcrumbs;
		}
	}
}
