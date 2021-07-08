using System.Collections.Generic;

namespace Portfolio.Site.Services.PageMetaProvider
{
	public interface IPageMetaTransformer
	{
		IEnumerable<MetaItem> TransformMetaItems(PageMetaCollection pageMetaCollection);
	}
}