namespace Portfolio.Site.ViewModels
{
	public class BreadcrumbBarViewModel
	{
		public BreadcrumbViewModel[] Breadcrumbs { get; set; }

		public BreadcrumbBarViewModel(params BreadcrumbViewModel[] breadcrumbs)
		{
			Breadcrumbs = breadcrumbs;
		}
	}
}
