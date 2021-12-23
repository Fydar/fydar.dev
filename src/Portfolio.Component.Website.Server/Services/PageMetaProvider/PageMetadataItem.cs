using System.Collections.Generic;

namespace Portfolio.Component.Website.Server.Services.PageMetaProvider
{
	public class PageMetadataItem
	{
		public Dictionary<string, string> KeyValuePairs { get; }

		public PageMetadataItem(string? name = null, string? property = null, string? content = null)
		{
			KeyValuePairs = new Dictionary<string, string>();

			if (name != null)
			{
				KeyValuePairs.Add("name", name);
			}

			if (property != null)
			{
				KeyValuePairs.Add("property", property);
			}

			if (content != null)
			{
				KeyValuePairs.Add("content", content);
			}
		}
	}
}
