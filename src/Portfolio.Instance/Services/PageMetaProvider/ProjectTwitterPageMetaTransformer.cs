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
				yield return new MetaItem("twitter:card", "summary_large_image");

				yield return new MetaItem("twitter:title", projectViewModel.Project.ProjectName);
				yield return new MetaItem("twitter:description", projectViewModel.Project.Excerpt);

				yield return new MetaItem("twitter:image", $"https://anthonymarmont.com/{projectViewModel.Project.FeaturedImage}");
				yield return new MetaItem("twitter:image:alt", "");

				yield return new MetaItem("twitter:site", "@Fydarus");
				yield return new MetaItem("twitter:creator", "@Fydarus");
			}
		}
	}
}
