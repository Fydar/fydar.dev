using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System.Buffers;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Site.Components.Markup
{
	internal class MarkupViewComponentResult : IViewComponentResult
	{
		private readonly Stream stream;

		public MarkupViewComponentResult(Stream stream)
		{
			this.stream = stream;
		}

		public void Execute(ViewComponentContext context)
		{
		}

		public async Task ExecuteAsync(ViewComponentContext context)
		{
			using var streamReader = new StreamReader(stream);

			char[] buffer = ArrayPool<char>.Shared.Rent(2048);
			while (!streamReader.EndOfStream)
			{
				int bytesRead = await streamReader.ReadBlockAsync(buffer, 0, buffer.Length);
				context.Writer.Write(buffer, 0, bytesRead);
			}
			ArrayPool<char>.Shared.Return(buffer);
		}
	}
}
