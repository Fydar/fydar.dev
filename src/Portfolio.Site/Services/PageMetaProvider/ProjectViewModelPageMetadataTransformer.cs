using Portfolio.Site.Areas.Portfolio.Models;

namespace Portfolio.Site.Services.PageMetaProvider
{
	public class ProjectViewModelPageMetadataTransformer : IPageMetadataTransformer<ProjectViewModel>
	{
		public PageMetadata TransformMetadata(ProjectViewModel model)
		{
			string domain = $"anthonymarmont.com";
			string url = $"https://{domain}/portfolio/{model.Project.Slug}";
			string imageUrl = $"https://{domain}/{model.Project.FeaturedImage}";

			return new PageMetadata()
			{
				new PageMetadataItem(name: "author", content: "Anthony Marmont"),
				new PageMetadataItem(property: "description", name: "description", content: model.Project.Excerpt),

				new PageMetadataItem(property: "og:description", content: model.Project.Excerpt),
				new PageMetadataItem(property: "og:title", name: "title", content: model.Project.ProjectName),
				new PageMetadataItem(property: "og:image", name: "image", content: imageUrl),
				new PageMetadataItem(property: "og:image:alt", content: model.Project.Excerpt),
				new PageMetadataItem(property: "og:type", content: "article"),
				new PageMetadataItem(property: "og:url", content: url),

				new PageMetadataItem(name: "twitter:card", content: "summary_large_image"),
				new PageMetadataItem(name: "twitter:title", content: model.Project.ProjectName),
				new PageMetadataItem(name: "twitter:description", content: model.Project.Excerpt),
				new PageMetadataItem(name: "twitter:image", content: imageUrl),
				new PageMetadataItem(name: "twitter:image:alt", content: model.Project.Excerpt),
				new PageMetadataItem(name: "twitter:site", content: "@Fydarus"),
				new PageMetadataItem(name: "twitter:creator", content: "@Fydarus"),
				new PageMetadataItem(property: "twitter:domin", content: domain),
				new PageMetadataItem(property: "twitter:url", content: url),
			};
		}
	}
}
