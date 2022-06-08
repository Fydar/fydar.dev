using Microsoft.AspNetCore.Html;
using System.IO;
using System.Reflection;
using System.Text.Encodings.Web;

namespace Portfolio.Component.Website.Server.ViewModels;

public class StaticPageBreadcrumbs : IHtmlContent
{
	public BreadcrumbViewModel[] Breadcrumbs { get; set; }

	public StaticPageBreadcrumbs(params BreadcrumbViewModel[] breadcrumbs)
	{
		Breadcrumbs = breadcrumbs;
	}

	public void WriteTo(TextWriter writer, HtmlEncoder encoder)
	{
		string domain = $"anthonymarmont.com";

		writer.Write("<script type=\"application/ld+json\">{");
		writer.Write("\"@context\": \"https://schema.org\",");
		writer.Write("\"@type\": \"BreadcrumbList\",");
		writer.Write("\"itemListElement\": [");

		for (int i = 0; i < Breadcrumbs.Length; i++)
		{
			var breadcrumb = Breadcrumbs[i];

			writer.Write("{");
			writer.Write("\"@type\": \"ListItem\",");
			writer.Write($"\"position\": {i + 1},");
			writer.Write($"\"name\": \"{breadcrumb.Text}\"");

			if (i != Breadcrumbs.Length - 1)
			{
				writer.Write(",");
				writer.Write($"\"item\": \"https://{domain}{breadcrumb.PageUrl}\"");
			}

			writer.Write("}");

			if (i != Breadcrumbs.Length - 1)
			{
				writer.Write(",");
			}
		}
		writer.WriteLine("]}");
		writer.Write("</script>");
	}
}
