using Portfolio.Component.Website.Server.ViewModels;

namespace Portfolio.Component.Website.Server.Services.PageMetaProvider;

public class StaticPageViewModel
{
	public string Title { get; set; } = string.Empty;
	public StaticPageMetadata Metadata { get; set; }
	public StaticPageBreadcrumbs? Breadcrumbs { get; set; }
}
