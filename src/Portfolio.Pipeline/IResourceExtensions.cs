using Portfolio.Models;

namespace Portfolio.Pipeline
{
	public static class IResourceExtensions
	{
		public static string TransformName(this IResource resource, string insert)
		{
			return ResourceHelper.TransformName(resource.Name, insert);
		}
	}
}
