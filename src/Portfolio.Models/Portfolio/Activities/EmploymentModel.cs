using RPGCore.Data;
using Portfolio.Models.Portfolio.Activities;
using Portfolio.Models;

namespace Portfolio.Models.Portfolio.Activities
{
	[EditableType]
	public class EmploymentModel : ActivityModel
	{
		public long EndTime { get; set; }
	}
}
