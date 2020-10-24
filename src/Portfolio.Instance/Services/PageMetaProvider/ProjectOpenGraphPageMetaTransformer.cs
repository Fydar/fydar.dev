﻿using Portfolio.Instance.ViewModels;
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
				yield return new MetaItem("og:title", projectViewModel.Project.ProjectName);
				yield return new MetaItem("og:type", "article");
				yield return new MetaItem("og:url", $"https://anthonymarmont.com/portfolio/{projectViewModel.Project.Slug}");
				yield return new MetaItem("og:image", $"https://anthonymarmont.com/{projectViewModel.Project.FeaturedImage}");
			}
		}
	}
}