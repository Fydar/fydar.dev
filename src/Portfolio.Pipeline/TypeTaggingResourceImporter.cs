using Newtonsoft.Json;
using Portfolio.Models;
using RPGCore.Packages.Pipeline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Portfolio.Pipeline
{
	public class TypeTaggingResourceImporter : ImportProcessor
	{
		private static readonly Dictionary<string, string> featuredImageMetadata = new Dictionary<string, string>()
		{
			["Size"] = "fullscreen,medium,thumbnail"
		};

		public override void ProcessImport(ProjectResourceImporter importer)
		{
			Console.WriteLine(importer.ArchiveEntry.FullName);
			if (importer.ArchiveEntry.FullName.StartsWith("data/projects/categories"))
			{
				importer.ImporterTags.Add("type-category");
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/projects/skills"))
			{
				importer.ImporterTags.Add("type-skill");
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/projects"))
			{
				importer.ImporterTags.Add("type-project");

				var loaded = LoadJson<ProjectModel>(importer);
				importer.Dependencies.Register(loaded.FeaturedImage, metadata: featuredImageMetadata);
				importer.Dependencies.Register(loaded.HoverImage, metadata: featuredImageMetadata);

				importer.Dependencies.Register(loaded.ProjectCategory);
				importer.Dependencies.Register(loaded.Page);

				if (!string.IsNullOrEmpty(loaded.Institution))
				{
					importer.Dependencies.Register(loaded.Institution);
				}

				foreach (string skill in loaded.Skills)
				{
					importer.Dependencies.Register(skill);
				}
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/education"))
			{
				importer.ImporterTags.Add("type-education");

				var loaded = LoadJson<EducationalInstitutionModel>(importer);
				importer.Dependencies.Register(loaded.IconUrl);
				importer.Dependencies.Register(loaded.Page);
			}
			else if (importer.ArchiveEntry.FullName.StartsWith("data/company"))
			{
				importer.ImporterTags.Add("type-company");

				var loaded = LoadJson<CompanyModel>(importer);
				importer.Dependencies.Register(loaded.IconUrl);
				importer.Dependencies.Register(loaded.Page);
			}
		}

		private static TModel LoadJson<TModel>(ProjectResourceImporter importer)
		{
			var serializer = new JsonSerializer();
			using var file = importer.ArchiveEntry.OpenRead();
			using var sr = new StreamReader(file);
			using var reader = new JsonTextReader(sr);

			var model = serializer.Deserialize<TModel>(reader);
			return model;
		}

		public static TModel ProcessXmlDocument<TModel>(ProjectResourceImporter importer)
			where TModel : class
		{
			static IEnumerable<XmlNode> AllNodes(XmlNode rootNode)
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

			var document = new XmlDocument();
			document.Load(importer.ArchiveEntry.OpenRead());

			var rootElement = document.DocumentElement;

			foreach (var imageElement in AllNodes(document.DocumentElement))
			{
				if (imageElement.Name == "img")
				{
					var srcAttribute = imageElement.Attributes["src"];

					importer.Dependencies.Register(srcAttribute.Value.Replace("../", ""));
				}
			}

			var element = rootElement.ChildNodes.Item(0);
			return JsonConvert.DeserializeObject<TModel>(element.InnerXml);
		}
	}
}
