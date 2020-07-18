using RPGCore.Packages;

namespace Portfolio.Models
{
	public interface ILoadedResourceCache
	{
		T GetOrDeserialize<T>(IResource resource);
	}
}
