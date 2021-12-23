using RPGCore.Data;

namespace Portfolio.Services.Content.Portfolio
{
	[EditableType]
	public struct ExternalLinkModel
	{
		public string SiteName { get; set; }
		public string Url { get; set; }
		public string Icon { get; set; }
	}
}
