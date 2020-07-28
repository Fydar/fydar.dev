namespace Portfolio.Instance.Models
{
	public struct BreadcrumbModel
	{
		public string Text { get; set; }
		public string Controller { get; set; }
		public string Action { get; set; }
		public object RouteParameters { get; set; }
	}
}
