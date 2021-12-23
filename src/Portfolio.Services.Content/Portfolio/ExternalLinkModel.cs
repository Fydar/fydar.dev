using RPGCore.Data;

namespace Portfolio.Models.Portfolio
{
	[EditableType]
	public struct ExternalLinkModel
	{
		public string SiteName { get; set; }
		public string Url { get; set; }
		public string Icon { get; set; }
	}
}
