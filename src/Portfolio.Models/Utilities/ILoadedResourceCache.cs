using RPGCore.Packages;
using Portfolio.Models;

namespace Portfolio.Models.Utilities
{
	public interface ILoadedResourceCache
	{
		IResource? GetResource(string fullname);
		T GetOrDeserialize<T>(IResource resource);
	}
}
