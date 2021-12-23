using Portfolio.Services.Content.Utilities;
using RPGCore.Packages;
using RPGCore.Projects;
using RPGCore.Projects.Pipeline;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Services.Pipeline
{
	public class ImageProcessor : IImportProcessor
	{
		public bool CanProcess(IResource resource)
		{
			return string.Equals(resource.Extension, ".png", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpg", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpeg", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".bmp", StringComparison.OrdinalIgnoreCase);
		}

		public IEnumerable<ProjectResourceUpdate> ProcessImport(ImportProcessorContext context, IResource resource)
		{
			lock (Console.Out)
			{
				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.Write("Discovering image '");

				Console.ForegroundColor = ConsoleColor.Gray;
				Console.Write(resource.FullName);

				Console.ForegroundColor = ConsoleColor.DarkGray;
				Console.WriteLine("'");
			}

			var sizes = new HashSet<string>
			{
				"blur"
			};

			// Find out how consumers use this resource...
			foreach (var dependency in resource.Dependants)
			{
				if (dependency.Metadata == null)
				{
					continue;
				}
				if (dependency.Metadata.TryGetValue("Size", out string sizeMetadata))
				{
					string[] dependencySizes = sizeMetadata.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

					foreach (string size in dependencySizes)
					{
						sizes.Add(size);
					}
				}
			}

			foreach (string size in sizes)
			{
				var imageSettings = ResourceHelper.resolutions[size];
				string name = $"img/{resource.TransformName(size)}";
				string extension = name.Substring(name.LastIndexOf('.'));

				var content = new ImageContentWriter(
					resource,
					resource.Content,
					imageSettings,
					extension);

				var update = context
					.AuthorUpdate(name)
					.WithContent(content);

				yield return update;
			}
		}

		private class ImageContentWriter : IContentWriter
		{
			private readonly IResource resource;
			private readonly IResourceContent source;
			private readonly ImageSettings imageSettings;
			private readonly string extension;

			public ImageContentWriter(
				IResource resource,
				IResourceContent source,
				ImageSettings imageSettings,
				string extension)
			{
				this.resource = resource;
				this.source = source;
				this.imageSettings = imageSettings;
				this.extension = extension;
			}

			public Task WriteContentAsync(Stream destination)
			{
				try
				{
					using var readStream = source.OpenRead();
					using var image = Image.Load(readStream);

					// Make sure the image doesn't exceed the max width.
					if (image.Width > imageSettings.MaxWidth)
					{
						int newWidth = imageSettings.MaxWidth;
						int newHeight = (int)(image.Height / (float)image.Width) * newWidth;

						// Resize the image to the desired size.
						image.Mutate(x => x.Resize(newWidth, newHeight));
					}

					// Save the image.
					if (string.Equals(extension, ".png", StringComparison.OrdinalIgnoreCase))
					{
						return image.SaveAsPngAsync(destination);
					}
					else if (string.Equals(extension, ".jpg", StringComparison.OrdinalIgnoreCase)
						|| string.Equals(extension, ".jpeg", StringComparison.OrdinalIgnoreCase))
					{
						return image.SaveAsJpegAsync(destination);
					}
					else if (string.Equals(extension, ".bmp", StringComparison.OrdinalIgnoreCase))
					{
						return image.SaveAsBmpAsync(destination);
					}
					else if (string.Equals(extension, ".gif", StringComparison.OrdinalIgnoreCase))
					{
						return image.SaveAsGifAsync(destination);
					}
					else
					{
						throw new InvalidOperationException($"Cannot compress a file of type '{extension}'.");
					}
				}
				catch (Exception exception)
				{
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine($"Failed to import image from file '{resource}'\n{exception}");
					throw;
				}
			}
		}
	}
}
