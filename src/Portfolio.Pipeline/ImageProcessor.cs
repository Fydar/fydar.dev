using RPGCore.Packages;
using RPGCore.Projects;
using RPGCore.Projects.Pipeline;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.Pipeline
{
	public class ImageProcessor : IImportProcessor
	{
		internal static readonly Dictionary<string, ImageSettings> resolutions = new Dictionary<string, ImageSettings>()
		{
			["blur"] = new ImageSettings()
			{
				Width = 32,
				ImageFormat = ImageFormat.Jpeg
			},
			["tiny"] = new ImageSettings()
			{
				Width = 64
			},
			["thumbnail"] = new ImageSettings()
			{
				Width = 150
			},
			["medium"] = new ImageSettings()
			{
				Width = 400,
				ImageFormat = ImageFormat.Jpeg
			},
			["fullscreen"] = new ImageSettings()
			{
				Width = 1200
			},
		};

		public bool CanProcess(IResource resource)
		{
			return string.Equals(resource.Extension, ".png", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpg", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
		}

		public IEnumerable<ProjectResourceUpdate> ProcessImport(ImportProcessorContext context, IResource resource)
		{
			Console.WriteLine($"Discovering Resources {resource.FullName}");

			var sizes = new HashSet<string>
			{
				"blur"
			};
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
				var imageSettings = resolutions[size];
				string name = $"img/{resource.TransformName(size)}";

				var content = new ImageContentWriter()
				{
					extension = name.Substring(name.IndexOf('.')),
					imageSettings = imageSettings,
					source = resource.Content
				};

				var update = context
					.AuthorUpdate(name)
					.WithContent(content);

				yield return update;
			}
		}

		private class ImageContentWriter : IContentWriter
		{
			internal IResourceContent source;
			internal ImageSettings imageSettings;
			internal string extension;

			public Task WriteContentAsync(Stream destination)
			{
				using var readStream = source.OpenRead();
				var sourceBitmap = new Bitmap(readStream);

				int width = Math.Min(imageSettings.Width, sourceBitmap.Width);
				int height = int.MaxValue;

				var resized = Resize(sourceBitmap, width, height);

				return CompressImageSave(resized, destination, extension, 90, imageSettings.ImageFormat);
			}

			private static async Task CompressImageSave(Bitmap bitmap, Stream destination, string extension, int quality, ImageFormat imageFormat)
			{
				await Task.Run(() =>
				{
					static ImageCodecInfo GetEncoder(ImageFormat format)
					{
						var codecs = ImageCodecInfo.GetImageDecoders();
						foreach (var codec in codecs)
						{
							if (codec.FormatID == format.Guid)
							{
								return codec;
							}
						}
						return null;
					}

					if (imageFormat == null)
					{
						switch (extension)
						{
							case ".png":
							case ".PNG":
								imageFormat = ImageFormat.Png;
								break;

							case ".jpeg":
							case ".jpg":
							case ".JPEG":
							case ".JPG":
								imageFormat = ImageFormat.Jpeg;
								break;

							case ".bmp":
							case ".BMP":
								imageFormat = ImageFormat.Bmp;
								break;

							default:
								throw new InvalidOperationException("Cannot compress a file of type " + extension);
						}
					}
					var encoder = GetEncoder(imageFormat);

					var parameters = new EncoderParameters(1);
					parameters.Param[0] = new EncoderParameter(Encoder.Quality, quality);

					bitmap.Save(destination, encoder, parameters);
				});
			}

			private static Bitmap Resize(Bitmap bitmap, int width, int height)
			{
				static Size CalculateResizedDimensions(Image image, int desiredWidth, int desiredHeight)
				{
					double widthScale = (double)desiredWidth / image.Width;
					double heightScale = (double)desiredHeight / image.Height;

					double scale = widthScale < heightScale ? widthScale : heightScale;

					return new Size
					{
						Width = (int)(scale * image.Width),
						Height = (int)(scale * image.Height)
					};
				}

				var newSize = CalculateResizedDimensions(bitmap, width, height);

				var resizedImage = new Bitmap(newSize.Width, newSize.Height, PixelFormat.Format24bppRgb);
				resizedImage.SetResolution(72, 72);

				using var graphics = Graphics.FromImage(resizedImage);

				// set parameters to create a high-quality thumbnail
				graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
				graphics.SmoothingMode = SmoothingMode.AntiAlias;
				graphics.CompositingQuality = CompositingQuality.HighQuality;
				graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

				using var attribute = new ImageAttributes();
				attribute.SetWrapMode(WrapMode.TileFlipXY);

				graphics.DrawImage(bitmap,
					new Rectangle(new Point(0, 0), newSize),
					0, 0, bitmap.Width, bitmap.Height, GraphicsUnit.Pixel, attribute);
				return resizedImage;
			}
		}
	}
}
