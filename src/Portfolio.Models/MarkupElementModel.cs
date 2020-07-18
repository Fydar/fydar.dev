using System.Collections.Generic;

namespace Portfolio.Models
{
	public class MarkupElementModel
	{
		public string Layout { get; set; }
		public string[] AdditionalStyles { get; set; }
		public Dictionary<string, string> Parameters { get; set; }
		public Dictionary<string, MarkupElementModel> Children { get; set; }
	}
}
