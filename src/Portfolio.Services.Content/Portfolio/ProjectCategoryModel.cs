using Newtonsoft.Json;
using Portfolio.Services.Content.Utilities;
using RPGCore.Data;
using RPGCore.Packages;
using System;
using System.Collections.Generic;

namespace Portfolio.Services.Content.Portfolio;

[EditableType]
public class ProjectCategoryModel : ILoadResourceCallback, IComparable<ProjectCategoryModel>
{
	public string DisplayName { get; set; } = string.Empty;
	public string Slug { get; set; } = string.Empty;
	public string Description { get; set; } = string.Empty;
	public string FeaturedImage { get; set; } = string.Empty;
	public int Order { get; set; }

	[JsonIgnore] public List<ProjectModel> Projects { get; private set; } = new List<ProjectModel>();

	public void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource)
	{
	}

	public int CompareTo(ProjectCategoryModel other)
	{
		return Order.CompareTo(other?.Order ?? 0);
	}
}
