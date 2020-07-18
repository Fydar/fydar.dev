using RPGCore.Packages;
using RPGCore.Packages.Archives;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

namespace Portfolio.Pipeline
{
	public class RemoveMetaResourceExporter : ResourceExporter
	{
		public override bool CanExport(IResource resource)
		{
			return resource.Extension == ".html";
		}

		public override void BuildResource(IResource resource, IArchiveEntry contentEntry)
		{
			var serializer = new JsonSerializer()
			{
				Formatting = Newtonsoft.Json.Formatting.None
			};

			var oldDocument = new XmlDocument();
			oldDocument.Load(resource.Content.LoadStream());

			oldDocument.DocumentElement.RemoveChild(oldDocument.DocumentElement["script"]);

			foreach (var imageElement in AllNodes(oldDocument.DocumentElement))
			{
				if (imageElement.Name == "img")
				{
					var srcAttribute = imageElement.Attributes["src"];

					srcAttribute.Value = srcAttribute.Value.Replace("../", "");
				}
			}

			var newDocument = new XmlDocument();
			var newRoot = newDocument.CreateElement("div");
			var classAttribute = newDocument.CreateAttribute("class");
			classAttribute.Value = "article-content";
			newRoot.Attributes.Append(classAttribute);
			newDocument.AppendChild(newRoot);

			newRoot.InnerXml = oldDocument.DocumentElement["body"].InnerXml;

			/*
			var bodyChildren = document.DocumentElement["body"].ChildNodes;
			for (int i = 0; i < bodyChildren.Count; i++)
			{
				var bodyChild = bodyChildren.Item(i);

				newRoot.AppendChild(bodyChild.Clone());
			}
			*/

			using (var output = contentEntry.OpenWrite())
			{
				newDocument.Save(output);
			}

			/*JObject document;
			using (var sr = new StreamReader(resource.Content.LoadStream()))
			using (var reader = new JsonTextReader(sr))
			{
				document = serializer.Deserialize<JObject>(reader);
			}

			using var zipStream = contentEntry.OpenWrite();
			using var streamWriter = new StreamWriter(zipStream);
			serializer.Serialize(streamWriter, document);*/
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