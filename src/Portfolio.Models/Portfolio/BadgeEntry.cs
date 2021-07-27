using RPGCore.Data;

namespace Portfolio.Models.Portfolio
{
	[EditableType]
	public struct BadgeEntry
	{
		public string Content { get; set; }
		public string Url { get; set; }
		public string Tooltip { get; set; }
		public string Icon { get; set; }
		public bool DisplayOnHomepage { get; set; }
	}
}
