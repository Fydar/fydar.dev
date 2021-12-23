using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Portfolio.Component.Website.Server.Services.PageMetaProvider
{
	public static class ViewDataDictionaryExtensions
	{
		public static void SetPageMetadata(this ViewDataDictionary viewData, PageMetadata metadata)
		{
			viewData["PageMetaSource"] = metadata;
		}

		public static PageMetadata? GetPageMetadata(this ViewDataDictionary viewData)
		{
			if (viewData.TryGetValue("PageMetaSource", out object? viewDataValue))
			{
				return viewDataValue as PageMetadata;
			}
			return null;
		}
	}
}
