using Portfolio.Services.Content.Utilities;
using RPGCore.Packages;

namespace Portfolio.Services.Pipeline;

public static class IResourceExtensions
{
	public static string TransformName(this IResource resource, string insert)
	{
		return ResourceHelper.TransformName(resource.Name, insert);
	}
}
