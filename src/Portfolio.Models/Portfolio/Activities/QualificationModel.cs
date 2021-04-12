using RPGCore.DataEditor.CSharp;

namespace Portfolio.Models
{
	[EditorType]
	public class QualificationModel : ActivityModel
	{
		public string FullQualificationName { get; set; }
		public string Grade { get; set; }
		public string TimeSpan { get; set; }
	}
}
