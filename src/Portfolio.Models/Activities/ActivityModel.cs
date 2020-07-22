using System;

namespace Portfolio.Models
{
	public abstract class ActivityModel : IComparable<ActivityModel>
	{
		public string Title { get; set; }
		public string Slug { get; set; }
		public string Excerpt { get; set; }
		public string Page { get; set; }
		public string Institution { get; set; }
		public long StartTime { get; set; }

		public int CompareTo(ActivityModel other)
		{
			return StartTime.CompareTo(other?.StartTime ?? 0);
		}
	}
}
