namespace Portfolio.Component.Website.Server.Services.PageMetaProvider
{
	public interface IPageMetadataTransformer<T>
	{
		public PageMetadata TransformMetadata(T model);
	}
}
