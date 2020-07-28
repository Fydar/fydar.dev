using RPGCore.Packages;
using RPGCore.Packages.Archives;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Portfolio.Pipeline
{
	public class MarkupExporter : ResourceExporter
	{
		public override bool CanExport(IResource resource)
		{
			return resource.Extension == ".html";
		}

		public override void BuildResource(IResource resource, IArchive archive)
		{
			Console.WriteLine($"Exporting {resource.FullName}...");

			var oldDocument = new XmlDocument();
			oldDocument.Load(resource.Content.LoadStream());

			foreach (var imageElement in AllNodes(oldDocument.DocumentElement))
			{
				if (imageElement.Name == "img")
				{
					var srcAttribute = imageElement.Attributes["src"];

					srcAttribute.Value = srcAttribute.Value.Replace("../", "");
				}
			}

			var entry = archive.Files.GetFile($"data/{resource.FullName}");
			using var output = entry.OpenWrite();
			oldDocument.Save(output);
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
	}
}
