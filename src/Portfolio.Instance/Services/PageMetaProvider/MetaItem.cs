namespace Portfolio.Instance.Services.PageMetaProvider
{
	public struct MetaItem
	{
		public string Name { get; set; }
		public string Content { get; set; }

		public MetaItem(string name, string content)
		{
			Name = name;
			Content = content;
		}
	}
}
