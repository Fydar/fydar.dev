using RPGCore.Packages;

namespace Portfolio.Services.Content.Utilities;

public interface ILoadedResourceCache
{
	IResource? GetResource(string fullname);
	T GetOrDeserialize<T>(IResource resource);
}
