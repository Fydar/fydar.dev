using RPGCore.Data;

namespace Portfolio.Models
{
	[EditableType]
	public class EmploymentModel : ActivityModel
	{
		public long EndTime { get; set; }
	}
}
