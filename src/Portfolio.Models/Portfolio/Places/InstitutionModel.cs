using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Portfolio.Models.Portfolio.Places;
using Portfolio.Models;

namespace Portfolio.Models.Portfolio.Places
{
	public abstract class InstitutionModel : IComparable<InstitutionModel>
	{
		public bool DisplayOnResume { get; set; } = true;
		public string Slug { get; set; } = string.Empty;
		public string Name { get; set; } = string.Empty;
		public string IconUrl { get; set; } = string.Empty;
		public string Page { get; set; } = string.Empty;
		public string Address { get; set; } = string.Empty;
		public string TwoWordAddress { get; set; } = string.Empty;
		public string Excerpt { get; set; } = string.Empty;

		[JsonIgnore]
		public int TotalMonths
		{
			get
			{
				int total = 0;
				foreach (var position in Positions)
				{
					total += (int)Math.Ceiling(position.Elapsed.TotalDays / 30.4167);
				}
				return total;
			}
		}

		[JsonIgnore]
		public int Months => TotalMonths % 12;

		[JsonIgnore]
		public int Years => TotalMonths / 12;

		[JsonIgnore]
		public abstract string Tagline { get; }

		public TimeSpan Elapsed
		{
			get
			{
				var total = TimeSpan.Zero;

				foreach (var position in Positions)
				{
					total += position.Elapsed;
				}
				return total;
			}
		}

		public List<PlacementModel> Positions { get; set; } = new List<PlacementModel>();

		public int CompareTo(InstitutionModel other)
		{
			return GetLatestEndTime().CompareTo(other.GetLatestEndTime());
		}

		public DateTimeOffset GetOldestStartTime()
		{
			var oldest = DateTimeOffset.UtcNow;

			foreach (var position in Positions)
			{
				if (position.StartTime < oldest)
				{
					oldest = position.StartTime;
				}
			}
			return oldest;
		}
		public DateTimeOffset GetLatestEndTime()
		{
			var newest = DateTimeOffset.MinValue;

			foreach (var position in Positions)
			{
				if (position.StartTime > newest)
				{
					newest = position.EndTime ?? DateTimeOffset.UtcNow;
				}
			}
			return newest;
		}
	}
}
