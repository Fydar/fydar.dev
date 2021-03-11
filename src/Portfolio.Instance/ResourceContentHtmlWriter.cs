using Microsoft.AspNetCore.Html;
using RPGCore.Packages;
using System.Buffers;
using System.IO;
using System.Text.Encodings.Web;

namespace Portfolio.Instance
{
	public class ResourceContentHtmlWriter : IHtmlContent
	{
		private readonly IResourceContent resourceContent;

		public ResourceContentHtmlWriter(IResourceContent resourceContent)
		{
			this.resourceContent = resourceContent;
		}

		public void WriteTo(TextWriter writer, HtmlEncoder encoder)
		{
			using var stream = resourceContent.OpenRead();
			using var streamReader = new StreamReader(stream);

			char[] buffer = ArrayPool<char>.Shared.Rent(2048);
			while (!streamReader.EndOfStream)
			{
				int bytesRead = streamReader.ReadBlock(buffer, 0, buffer.Length);
				writer.Write(buffer, 0, bytesRead);
			}
			ArrayPool<char>.Shared.Return(buffer);
		}
	}
}
