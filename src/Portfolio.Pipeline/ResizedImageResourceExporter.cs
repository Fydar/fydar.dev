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
	public class ResizedImageResourceExporter : ResourceExporter
	{
		private static readonly Dictionary<string, int> resolutions = new Dictionary<string, int>()
		{
			["tiny"] = 64,
			["thumbnail"] = 150,
			["medium"] = 500,
			["fullscreen"] = 1920,
		};

		public override bool CanExport(IResource resource)
		{
			return string.Equals(resource.Extension, ".png", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpg", StringComparison.OrdinalIgnoreCase)
				|| string.Equals(resource.Extension, ".jpeg", StringComparison.OrdinalIgnoreCase);
		}

		public override void BuildResource(IResource resource, IArchive archive)
		{
			var entry = archive.Files.GetFile($"data/{resource.FullName}");
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
				int width = resolutions[size];
				int height = int.MaxValue;

				using var readStream = resource.Content.LoadStream();
				var source = new Bitmap(readStream);
				var resized = Resize(source, width, height);

				var resizedEntry = archive.Files.GetFile($"data/{resource.TransformName(size)}");
				using var resizedZipStream = resizedEntry.OpenWrite();

				CompressImageSave(resized, resizedZipStream, resource.Extension, 90);
			}
		}

		public static void CompressImageSave(Bitmap bitmap, Stream destination, string extension, int quality)
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
			ImageCodecInfo encoder;
			switch (extension)
			{
				case ".png":
				case ".PNG":
					encoder = GetEncoder(ImageFormat.Png);
					break;

				case ".jpeg":
				case ".jpg":
				case ".JPEG":
				case ".JPG":
					encoder = GetEncoder(ImageFormat.Jpeg);
					break;

				case ".bmp":
				case ".BMP":
					encoder = GetEncoder(ImageFormat.Bmp);
					break;

				default:
					throw new InvalidOperationException("Cannot compress a file of type " + extension);
			}

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