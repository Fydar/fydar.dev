namespace Portfolio.Models
{
	public static class ResourceHelper
	{
		public static string TransformName(string name, string insert)
		{
			if (name.EndsWith(".gif")
				|| name.EndsWith(".webp"))
			{
				return name;
			}

			return name.Insert(name.LastIndexOf('.'), $"-{insert}");
		}
	}
}