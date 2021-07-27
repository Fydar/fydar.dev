using RPGCore.Packages;
using Portfolio.Models.Utilities;
using Portfolio.Models;

namespace Portfolio.Models.Utilities
{
	public interface ILoadResourceCallback
	{
		void OnAfterDeserializedFrom(ILoadedResourceCache cache, IResource resource);
	}
}
