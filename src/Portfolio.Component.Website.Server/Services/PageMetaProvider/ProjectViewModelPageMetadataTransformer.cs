using Portfolio.Component.Website.Server.Areas.Portfolio.Models;
using Portfolio.Services.Content.Portfolio;

namespace Portfolio.Component.Website.Server.Services.PageMetaProvider;

public static class ProjectViewModelPageMetadataTransformer
{
	public static StaticPageMetadata TransformMetadata(ProjectModel model)
	{
		string domain = $"anthonymarmont.com";
		string url = $"https://{domain}/portfolio/{model.Slug}";
		string imageUrl = $"https://{domain}/{model.FeaturedImage}";

		return new StaticPageMetadata()
		{
			new PageMetadataItem(name: "author", content: "Anthony Marmont"),
			new PageMetadataItem(property: "description", name: "description", content: model.Excerpt),

			new PageMetadataItem(property: "og:description", content: model.Excerpt),
			new PageMetadataItem(property: "og:title", name: "title", content: model.ProjectName),
			new PageMetadataItem(property: "og:image", name: "image", content: imageUrl),
			new PageMetadataItem(property: "og:image:alt", content: model.Excerpt),
			new PageMetadataItem(property: "og:type", content: "article"),
			new PageMetadataItem(property: "og:url", content: url),

			new PageMetadataItem(name: "twitter:card", content: "summary_large_image"),
			new PageMetadataItem(name: "twitter:title", content: model.ProjectName),
			new PageMetadataItem(name: "twitter:description", content: model.Excerpt),
			new PageMetadataItem(name: "twitter:image", content: imageUrl),
			new PageMetadataItem(name: "twitter:image:alt", content: model.Excerpt),
			new PageMetadataItem(name: "twitter:site", content: "@Fydarus"),
			new PageMetadataItem(name: "twitter:creator", content: "@Fydarus"),
			new PageMetadataItem(property: "twitter:domin", content: domain),
			new PageMetadataItem(property: "twitter:url", content: url),
		};
	}
}
