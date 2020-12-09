using RPGCore.FileTree;
using RPGCore.Projects;
using RPGCore.Projects.Pipeline;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;

namespace Portfolio.Pipeline
{
	public class MarkupImporter : IArchiveFileImporter
	{
		private class XmlDocumentWriter : IContentWriter
		{
			private readonly XmlDocument document;

			public XmlDocumentWriter(XmlDocument document)
			{
				this.document = document;
			}

			public Task WriteContentAsync(Stream destination)
			{
				document.Save(destination);
				return Task.CompletedTask;
			}
		}

		public class Heading
		{
			public Heading Parent { get; set; }
			public string Headingtext { get; set; }
			public string HeadingIdentifier { get; set; }
			public int HeadingDepth { get; set; }
			public List<Heading> Subheadings { get; } = new List<Heading>();
		}

		public bool CanImport(IArchiveFile archiveFile)
		{
			return archiveFile.Extension == ".html";
		}

		public IEnumerable<ProjectResourceUpdate> ImportFile(ArchiveFileImporterContext context, IArchiveFile archiveFile)
		{
			var update = context.AuthorUpdate(archiveFile.FullName);
			update.ImporterTags.Add("page");

			var document = new XmlDocument();
			document.Load(archiveFile.OpenRead());

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

					var title = document.CreateNode(XmlNodeType.Element, "p", null);
					var titleStyle = document.CreateAttribute("class");
					titleStyle.Value = "toc-title";
					title.InnerText = "Contents";
					title.Attributes.Append(titleStyle);

					container.AppendChild(title);
					WriteHeadings(container, headings);

					imageElement.ParentNode.ReplaceChild(container, imageElement);
				}
			}

			update.WithContent(new XmlDocumentWriter(document));

			yield return update;
		}

		private void WriteHeadings(XmlNode xmlNode, IEnumerable<Heading> headings, string prefix = "")
		{
			var listElement = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "ul", null);

			int index = 0;
			foreach (var heading in headings)
			{
				string thisPrefix;
				if (!string.IsNullOrEmpty(prefix))
				{
					thisPrefix = $"{prefix}.{index + 1}";
				}
				else
				{
					thisPrefix = $"{index + 1}";
				}

				var listItem = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "li", "");
				var linkElement = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "a", "");
				if (!string.IsNullOrEmpty(heading.HeadingIdentifier))
				{
					var href = xmlNode.OwnerDocument.CreateAttribute("href");
					href.Value = "#" + heading.HeadingIdentifier;

					linkElement.Attributes.Append(href);
				}

				var tocNumberElement = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "span", "");
				var tocNumberElementStyle = xmlNode.OwnerDocument.CreateAttribute("class");
				tocNumberElementStyle.Value = "toc-number";
				tocNumberElement.Attributes.Append(tocNumberElementStyle);
				tocNumberElement.InnerText = thisPrefix;

				var tocTextElement = xmlNode.OwnerDocument.CreateNode(XmlNodeType.Element, "span", "");
				var tocTextElementStyle = xmlNode.OwnerDocument.CreateAttribute("class");
				tocTextElementStyle.Value = "toc-text";
				tocTextElement.Attributes.Append(tocTextElementStyle);
				tocTextElement.InnerText = heading.Headingtext;

				linkElement.AppendChild(tocNumberElement);
				linkElement.AppendChild(tocTextElement);
				listItem.AppendChild(linkElement);
				listElement.AppendChild(listItem);

				if (heading.Subheadings.Count > 0)
				{
					WriteHeadings(listItem, heading.Subheadings, thisPrefix);
				}
				index++;
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
						while (newHeading.HeadingDepth <= currentHeading.HeadingDepth)
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
