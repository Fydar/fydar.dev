namespace Portfolio.Site.Services.PageMetaProvider
{
	public interface IPageMetadataTransformer<T>
	{
		public PageMetadata TransformMetadata(T model);
	}
}
