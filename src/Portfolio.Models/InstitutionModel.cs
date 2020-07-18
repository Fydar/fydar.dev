namespace Portfolio.Models
{
	public abstract class InstitutionModel
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Slug { get; set; }
		public string Excerpt { get; set; }
		public string Page { get; set; }
		public string Address { get; set; }
	}
}
