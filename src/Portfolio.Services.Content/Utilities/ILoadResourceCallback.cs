using RPGCore.Packages;

namespace Portfolio.Models.Utilities
{
	public interface ILoadResourceCallback
	{
		void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource);
	}
}
