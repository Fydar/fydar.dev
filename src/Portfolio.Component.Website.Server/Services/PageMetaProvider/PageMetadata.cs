using Microsoft.AspNetCore.Html;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;

namespace Portfolio.Component.Website.Server.Services.PageMetaProvider
{
	public class PageMetadata : IEnumerable<PageMetadataItem>, IHtmlContent
	{
		public List<PageMetadataItem> MetadataItems { get; } = new List<PageMetadataItem>();

		public void Add(PageMetadataItem pageMetadataItem)
		{
			MetadataItems.Add(pageMetadataItem);
		}

		public void WriteTo(TextWriter writer, HtmlEncoder encoder)
		{
			foreach (var metadataItem in MetadataItems)
			{
				writer.Write("<meta ");
				foreach (var keyValuePair in metadataItem.KeyValuePairs)
				{
					writer.Write(keyValuePair.Key);
					writer.Write("=\"");
					writer.Write(keyValuePair.Value);
					writer.Write("\"");
				}
				writer.WriteLine(" />");
			}
		}

		public IEnumerator<PageMetadataItem> GetEnumerator()
		{
			return MetadataItems.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return MetadataItems.GetEnumerator();
		}
	}
}
