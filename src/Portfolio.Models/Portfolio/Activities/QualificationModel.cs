using RPGCore.Data;
using Portfolio.Models.Portfolio.Activities;
using Portfolio.Models;

namespace Portfolio.Models.Portfolio.Activities
{
	[EditableType]
	public class QualificationModel : ActivityModel
	{
		public string FullQualificationName { get; set; } = string.Empty;
		public string Grade { get; set; } = string.Empty;
		public string TimeSpan { get; set; } = string.Empty;
	}
}
