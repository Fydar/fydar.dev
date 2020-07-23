using Portfolio.Models;
using RPGCore.Packages;

namespace Portfolio.Pipeline
{
	public static class IResourceExtensions
	{
		public static string TransformName(this IResource resource, string insert)
		{
			return ResourceHelper.TransformName(resource.FullName, insert);
		}
	}
}