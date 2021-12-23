using RPGCore.Packages;

namespace Portfolio.Models.Utilities
{
	public interface ILoadedResourceCache
	{
		IResource? GetResource(string fullname);
		T GetOrDeserialize<T>(IResource resource);
	}
}
