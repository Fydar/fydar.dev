using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace Portfolio.Component.Website.Server.Components.Markup
{
	[ViewComponent(Name = "Markup")]
	public class MarkupViewComponent : ViewComponent
	{
		public IViewComponentResult Invoke(
			Stream stream)
		{
			return new MarkupViewComponentResult(stream);
		}
	}
}
