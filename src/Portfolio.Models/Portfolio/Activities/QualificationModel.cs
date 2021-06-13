using RPGCore.Data;

namespace Portfolio.Models
{
	[EditableType]
	public class QualificationModel : ActivityModel
	{
		public string FullQualificationName { get; set; }
		public string Grade { get; set; }
		public string TimeSpan { get; set; }
	}
}
