using Portfolio.Instance.ViewModels;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.PageMetaProvider
{
	public class ProjectTwitterPageMetaTransformer : IPageMetaTransformer
	{
		public IEnumerable<MetaItem> TransformMetaItems(PageMetaCollection pageMetaCollection)
		{
			var projectViewModel = pageMetaCollection.GetModel<ProjectViewModel>();
			if (projectViewModel != null)
			{
				yield return new MetaItem(name: "twitter:card", content: "summary_large_image");

				yield return new MetaItem(name: "twitter:title", content: projectViewModel.Project.ProjectName);
				yield return new MetaItem(name: "twitter:description", content: projectViewModel.Project.Excerpt);

				yield return new MetaItem(name: "twitter:image", content: $"https://anthonymarmont.com/{projectViewModel.Project.FeaturedImage}");
				yield return new MetaItem(name: "twitter:image:alt", content: "");

				yield return new MetaItem(name: "twitter:site", content: "@Fydarus");
				yield return new MetaItem(name: "twitter:creator", content: "@Fydarus");
			}
		}
	}
}
