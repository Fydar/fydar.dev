using System.Collections.Generic;

namespace Portfolio.Models.Utilities
{
	public static class ResourceHelper
	{
		public static readonly Dictionary<string, ImageSettings> resolutions = new()
		{
			["blur"] = new ImageSettings()
			{
				MaxWidth = 32,
				ForceExtension = ".jpg"
			},
			["tiny"] = new ImageSettings()
			{
				MaxWidth = 64,
				ForceExtension = ".jpg"
			},
			["thumbnail"] = new ImageSettings()
			{
				MaxWidth = 150,
				ForceExtension = ".jpg"
			},
			["medium"] = new ImageSettings()
			{
				MaxWidth = 400,
				ForceExtension = ".jpg"
			},
			["large"] = new ImageSettings()
			{
				MaxWidth = 720,
				ForceExtension = ".jpg"
			},
			["fullscreen"] = new ImageSettings()
			{
				MaxWidth = 1200,
				ForceExtension = ".jpg"
			},
		};

		public static string TransformName(string name, string sizeKey)
		{
			if (string.IsNullOrEmpty(sizeKey))
			{
				return name;
			}

			if (name.EndsWith(".gif")
				|| name.EndsWith(".webp"))
			{
				return name;
			}

			if (resolutions.TryGetValue(sizeKey, out var settings))
			{
				if (!string.IsNullOrEmpty(settings.ForceExtension))
				{
					name = $"{name.Substring(0, name.LastIndexOf('.'))}{settings.ForceExtension}";
				}
			}

			return name.Insert(name.LastIndexOf('.'), $"-{sizeKey}");
		}
	}
}
