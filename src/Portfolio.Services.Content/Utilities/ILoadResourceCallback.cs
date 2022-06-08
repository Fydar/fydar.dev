using RPGCore.Packages;

namespace Portfolio.Services.Content.Utilities;

public interface ILoadResourceCallback
{
	void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource);
}
