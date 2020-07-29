using RPGCore.Packages;
using RPGCore.Packages.Archives;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Portfolio.Pipeline
{
	public class MarkupExporter : ResourceExporter
	{
		public class Heading
		{
			public Heading Parent { get; set; }
			public string Headingtext { get; set; }
			public string HeadingIdentifier { get; set; }
			public int HeadingDepth { get; set; }
			public List<Heading> Subheadings { get; } = new List<Heading>();
		}

		public override bool CanExport(IResource resource)
		{
			return resource.Extension == ".html";
		}

		public override void BuildResource(IResource resource, IArchive archive)
		{
			Console.WriteLine($"Exporting {resource.FullName}...");

			var document = new XmlDocument();
			document.Load(resource.Content.LoadStream());

			var headings = ReadHeadings(document.DocumentElement);

			foreach (var imageElement in AllNodes(document.DocumentElement))
			{
				if (imageElement.Name == "img")
				{
					var srcAttribute = imageElement.Attributes["src"];

					srcAttribute.Value = srcAttribute.Value.Replace("../", "");
				}
				else if (imageElement.Name == "TableOfContents")
				{
					var container = document.CreateNode(XmlNodeType.Element, "div", null);
					var containerStyle = document.CreateAttribute("class");
					containerStyle.Value = "toc-container";
					container.Attributes.Append(containerStyle);

					WriteHeadings(container, headings);

					imageElement.ParentNode.ReplaceChild(container, imageElement);
				}
			}

			var entry = archive.Files.GetFile($"data/{resource.FullName}");
			using var output = entry.OpenWrite();
			document.Save(output);
		}

		void WriteHeadings(XmlNode xmlNode, IEnumerable<Heading> headings)
		{
			var listElement = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "ul", null);

			foreach (var heading in headings)
			{
				var listItem = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "li", "");
				var linkElement = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "a", "");
				linkElement.InnerText = heading.Headingtext;

				if (!string.IsNullOrEmpty(heading.HeadingIdentifier))
				{
					var href = xmlNode.OwnerDocument.CreateAttribute("href");
					href.Value = "#" + heading.HeadingIdentifier;

					linkElement.Attributes.Append(href);
				}

				if (heading.Subheadings.Count > 0)
				{
					WriteHeadings(listItem, heading.Subheadings);
				}

				listItem.AppendChild(linkElement);
				listElement.AppendChild(listItem);
			}

			xmlNode.AppendChild(listElement);
		}

		private static IEnumerable<XmlNode> AllNodes(XmlNode rootNode)
		{
			for (int i = 0; i < rootNode.ChildNodes.Count; i++)
			{
				var childNode = rootNode.ChildNodes[i];
				yield return childNode;

				foreach (var node in AllNodes(childNode))
				{
					yield return node;
				}
			}
		}

		private static List<Heading> ReadHeadings(XmlNode rootNode)
		{
			var rootHeadings = new List<Heading>();
			Heading currentHeading = null;

			foreach (var node in AllNodes(rootNode))
			{
				if (node.Name.Length == 2
					&& node.Name[0] == 'h'
					&& char.IsDigit(node.Name[1]))
				{
					var newHeading = new Heading()
					{
						HeadingIdentifier = node.Attributes["id"]?.Value,
						Headingtext = node.InnerText,
						HeadingDepth = int.Parse(node.Name[1].ToString())
					};

					if (currentHeading == null)
					{
						rootHeadings.Add(newHeading);
					}
					else
					{
						while (newHeading.HeadingDepth < currentHeading.HeadingDepth)
						{
							currentHeading = currentHeading.Parent;
							if (currentHeading == null)
							{
								break;
							}
						}
						if (currentHeading == null)
						{
							rootHeadings.Add(newHeading);
						}
						else
						{
							currentHeading.Subheadings.Add(newHeading);
							newHeading.Parent = currentHeading;
						}
					}
					currentHeading = newHeading;
				}
			}
			return rootHeadings;
		}
	}
}
