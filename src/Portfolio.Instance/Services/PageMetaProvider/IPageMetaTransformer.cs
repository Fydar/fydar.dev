using System.Collections.Generic;

namespace Portfolio.Instance.Services.PageMetaProvider
{
	public interface IPageMetaTransformer
	{
		IEnumerable<MetaItem> TransformMetaItems(PageMetaCollection pageMetaCollection);
	}
}