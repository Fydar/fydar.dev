using System.Collections;
using System.Text;

namespace Fydar.Dev.WebApp;

public class LinkData : IReadOnlyList<ILinkDataElement>
{
	private readonly List<ILinkDataElement> elements = new();

	public ILinkDataElement this[int index] => elements[index];

	public int Count => elements.Count;

	public void Add(ILinkDataElement element)
	{
		elements.Add(element);
	}

	/// <inheritdoc/>
	public IEnumerator<ILinkDataElement> GetEnumerator()
	{
		return elements.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return elements.GetEnumerator();
	}

	public string ToJson()
	{
		var stringBuilder = new StringBuilder();

		stringBuilder.AppendLine("""
[{
""");

		for (int i = 0; i < elements.Count; i++)
		{
			var linkDataElement = elements[i];

			if (linkDataElement is LinkDataBreadcrumbList breadcrumbList)
			{
				stringBuilder.AppendLine("""
	"@context": "https://schema.org",
	"@type": "BreadcrumbList",
	"itemListElement": [{
""");

				for (int j = 0; j < breadcrumbList.Count; j++)
				{
					var breadcrumb = breadcrumbList[j];

					stringBuilder.Append($"""
		"@type": "ListItem",
		"position": {j + 1},
		"name": "{breadcrumb.Name}"
""");
					if (!string.IsNullOrWhiteSpace(breadcrumb.Item))
					{
						stringBuilder.AppendLine($"""
,
		"item": "{breadcrumb.Item}"
""");
					}
					else
					{
						stringBuilder.AppendLine();
					}

					if (j != breadcrumbList.Count - 1)
					{
						stringBuilder.AppendLine("""
	}, {
""");
					}
				}

				stringBuilder.AppendLine("""
	}]
""");
			}

			if (i != elements.Count - 1)
			{
				stringBuilder.AppendLine("""
}, {
""");
			}
		}

		stringBuilder.AppendLine("""
}]
""");

		return stringBuilder.ToString();
	}
	/*
<script type="application/ld+json">
[{
    "@context": "https://schema.org",
    "@type": "BreadcrumbList",
    "itemListElement": [{
		"@type": "ListItem",
		"position": 1,
		"name": "Books",
		"item": "https://example.com/books"
    },{
		"@type": "ListItem",
		"position": 2,
		"name": "Science Fiction",
		"item": "https://example.com/books/sciencefiction"
    },{
		"@type": "ListItem",
		"position": 3,
		"name": "Award Winners"
    }]
},
{
    "@context": "https://schema.org",
	"@type": "BreadcrumbList",
	"itemListElement": [{
		"@type": "ListItem",
		"position": 1,
		"name": "Literature",
		"item": "https://example.com/literature"
    },{
		"@type": "ListItem",
		"position": 2,
		"name": "Award Winners"
    }]
}]
</script>
*/
}

public interface ILinkDataElement
{

}

public class LinkDataBreadcrumbList : ILinkDataElement, IReadOnlyList<LinkDataBreadcrumbListItem>
{
	private readonly List<LinkDataBreadcrumbListItem> elements = new();

	public LinkDataBreadcrumbListItem this[int index] => elements[index];

	public int Count => elements.Count;

	public void Add(LinkDataBreadcrumbListItem element)
	{
		elements.Add(element);
	}

	/// <inheritdoc/>
	public IEnumerator<LinkDataBreadcrumbListItem> GetEnumerator()
	{
		return elements.GetEnumerator();
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		return elements.GetEnumerator();
	}
}

public class LinkDataBreadcrumbListItem
{
	public string Name { get; set; }
	public string Item { get; set; }

	public LinkDataBreadcrumbListItem()
	{
		Name = string.Empty;
		Item = string.Empty;
	}

	public LinkDataBreadcrumbListItem(string name)
	{
		Name = name;
		Item = string.Empty;
	}

	public LinkDataBreadcrumbListItem(string name, string item)
	{
		Name = name;
		Item = item;
	}
}

