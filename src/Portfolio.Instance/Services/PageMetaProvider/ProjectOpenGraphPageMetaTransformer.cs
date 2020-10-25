using Portfolio.Instance.ViewModels;
using System.Collections.Generic;

namespace Portfolio.Instance.Services.PageMetaProvider
{
	public class ProjectOpenGraphPageMetaTransformer : IPageMetaTransformer
	{
		public IEnumerable<MetaItem> TransformMetaItems(PageMetaCollection pageMetaCollection)
		{
			var projectViewModel = pageMetaCollection.GetModel<ProjectViewModel>();
			if (projectViewModel != null)
			{
				yield return new MetaItem(name: "author", content: "Anthony Marmont");

				yield return new MetaItem(name: "title", property: "og:title", content: projectViewModel.Project.ProjectName);
				yield return new MetaItem(name: "image", property: "og:image", content: $"https://anthonymarmont.com/{projectViewModel.Project.FeaturedImage}");
				yield return new MetaItem(property: "og:type", content: "article");
				yield return new MetaItem(property: "og:url", content: $"https://anthonymarmont.com/portfolio/{projectViewModel.Project.Slug}");
			}
		}
	}
}
