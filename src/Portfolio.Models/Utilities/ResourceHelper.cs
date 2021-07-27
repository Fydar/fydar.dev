using Portfolio.Models;
namespace Portfolio.Models.Utilities
{
	public static class ResourceHelper
	{
		public static string TransformName(string name, string insert)
		{
			if (string.IsNullOrEmpty(insert))
			{
				return name;
			}

			if (name.EndsWith(".gif")
				|| name.EndsWith(".webp"))
			{
				return name;
			}

			if (insert == "blur"
				|| insert == "medium")
			{
				name = name.Substring(0, name.LastIndexOf('.')) + ".jpg";
			}

			return name.Insert(name.LastIndexOf('.'), $"-{insert}");
		}
	}
}
