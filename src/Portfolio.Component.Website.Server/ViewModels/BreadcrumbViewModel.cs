namespace Portfolio.Component.Website.Server.ViewModels;

public struct BreadcrumbViewModel
{
	public string Text { get; set; }
	public string Controller { get; set; }
	public string Action { get; set; }
	public object RouteParameters { get; set; }
}
