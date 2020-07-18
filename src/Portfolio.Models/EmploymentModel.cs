using RPGCore.Behaviour.Manifest;

namespace Portfolio.Models
{
	[EditorType]
	public class EmploymentModel : ActivityModel
	{
		public long StartTime { get; set; }
		public long EndTime { get; set; }
	}
}
