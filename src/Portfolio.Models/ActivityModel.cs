namespace Portfolio.Models
{
	public abstract class ActivityModel
	{
		public string Title { get; set; }
		public string Slug { get; set; }
		public string Excerpt { get; set; }
		public MarkupElementModel Page { get; set; }
		public string Institution { get; set; }
	}
}
