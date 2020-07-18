using RPGCore.Packages;

namespace Portfolio.Models
{
	public interface ILoadResourceCallback
	{
		void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource);
	}
}
