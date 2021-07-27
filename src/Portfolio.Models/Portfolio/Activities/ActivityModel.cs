using System;

namespace Portfolio.Models.Portfolio.Activities
{
	public abstract class ActivityModel : IComparable<ActivityModel>
	{
		public string Title { get; set; } = string.Empty;
		public string Slug { get; set; } = string.Empty;
		public string Excerpt { get; set; } = string.Empty;
		public string Page { get; set; } = string.Empty;
		public string Institution { get; set; } = string.Empty;
		public long StartTime { get; set; }

		public int CompareTo(ActivityModel other)
		{
			return StartTime.CompareTo(other?.StartTime ?? 0);
		}
	}
}
