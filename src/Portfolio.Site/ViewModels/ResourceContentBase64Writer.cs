using Microsoft.AspNetCore.Html;
using RPGCore.Packages;
using System;
using System.IO;
using System.Text.Encodings.Web;

namespace Portfolio.Site.ViewModels
{
	public class ResourceContentBase64Writer : IHtmlContent
	{
		private readonly IResourceContent resourceContent;

		public ResourceContentBase64Writer(IResourceContent resourceContent)
		{
			this.resourceContent = resourceContent;
		}

		public void WriteTo(TextWriter writer, HtmlEncoder encoder)
		{
			using var stream = resourceContent.OpenRead();

			byte[] bytes;
			using (var memoryStream = new MemoryStream())
			{
				stream.CopyTo(memoryStream);
				bytes = memoryStream.ToArray();
			}

			string base64 = Convert.ToBase64String(bytes);
			writer.Write(base64);
		}
	}
}
