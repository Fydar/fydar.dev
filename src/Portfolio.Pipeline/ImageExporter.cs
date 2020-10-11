using RPGCore.Packages;
using RPGCore.Packages.Archives;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace Portfolio.Pipeline
{
	public class ImageSettings
	{
		public int Width { get; set; }
		public ImageFormat ImageFormat { get; set; }
	}

	public class ImageExporter : ResourceExporter
	{
		internal static readonly Dictionary<string, ImageSettings> resolutions = new Dictionary<string, ImageSettings>()
		{
			["blur"] = new ImageSettings()
			{
				Width = 48,
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
				Width = 360
			},
			["fullscreen"] = new ImageSettings()
			{
				Width = 1920
			},
		};

		public override bool CanExport(IResource resource)
		{
			return string.Equals(resource.Extension, ".png", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpg", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
		}

		public override void BuildResource(IResource resource, IArchiveDirectory destination)
		{
			Console.WriteLine($"Exporting {resource.FullName}...");

			var entry = destination.Files.GetFile(resource.Name);
			using (var zipStream = entry.OpenWrite())
			using (var readStream = resource.Content.LoadStream())
			{
				readStream.CopyTo(zipStream);
			}

			var sizes = new HashSet<string>();
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
				int width = imageSettings.Width;
				int height = int.MaxValue;

				using var readStream = resource.Content.LoadStream();
				var source = new Bitmap(readStream);
				var resized = Resize(source, width, height);

				var resizedEntry = destination.Files.GetFile(resource.TransformName(size));
				using var resizedZipStream = resizedEntry.OpenWrite();

				CompressImageSave(resized, resizedZipStream, resource.Extension, 90, imageSettings.ImageFormat);
			}
		}

		public static void CompressImageSave(Bitmap bitmap, Stream destination, string extension, int quality, ImageFormat imageFormat)
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
		}

		public static Bitmap Resize(Bitmap bitmap, int width, int height)
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
