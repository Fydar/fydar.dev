using Portfolio.Instance.ViewModels;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.PageMetaProvider
{
	public class ProjectPageMetaTransformer : IPageMetaTransformer
	{
		public IEnumerable<MetaItem> TransformMetaItems(PageMetaCollection pageMetaCollection)
		{
			var projectViewModel = pageMetaCollection.GetModel<ProjectViewModel>();
			if (projectViewModel != null)
			{
				string domain = $"anthonymarmont.com";
				string url = $"https://{domain}/portfolio/{projectViewModel.Project.Slug}";
				string imageUrl = $"https://{domain}/{projectViewModel.Project.FeaturedImage}";

				yield return new MetaItem(name: "author", content: "Anthony Marmont");
				yield return new MetaItem(property: "og:description", name: "description", content: projectViewModel.Project.Excerpt);

				yield return new MetaItem(property: "og:title", name: "title", content: projectViewModel.Project.ProjectName);
				yield return new MetaItem(property: "og:image", name: "image", content: imageUrl);
				yield return new MetaItem(property: "og:type", content: "article");
				yield return new MetaItem(property: "og:url", content: url);

				yield return new MetaItem(name: "twitter:card", content: "summary_large_image");
				yield return new MetaItem(name: "twitter:title", content: projectViewModel.Project.ProjectName);
				yield return new MetaItem(name: "twitter:description", content: projectViewModel.Project.Excerpt);
				yield return new MetaItem(name: "twitter:image", content: imageUrl);
				yield return new MetaItem(name: "twitter:image:alt", content: "");
				yield return new MetaItem(name: "twitter:site", content: "@Fydarus");
				yield return new MetaItem(name: "twitter:creator", content: "@Fydarus");
				yield return new MetaItem(property: "twitter:domin", content: domain);
				yield return new MetaItem(property: "twitter:url", content: url);
			}
		}
	}
}
