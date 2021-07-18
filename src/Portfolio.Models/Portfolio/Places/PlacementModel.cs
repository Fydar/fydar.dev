using System;
using System.Text;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
	public class PlacementModel : IComparable<PlacementModel>
	{
		public string Identifier { get; set; } = string.Empty;
		public string PlacementTitle { get; set; } = string.Empty;
		public string PlacementSubtitle { get; set; } = string.Empty;
		public string PlacementDescriptionHtml { get; set; } = string.Empty;

		public long StartTimestamp { get; set; }
		public long? EndTimestamp { get; set; }

		public bool DisplayMonth { get; set; } = true;

		[JsonIgnore]
		public DateTimeOffset StartTime => DateTimeOffset.FromUnixTimeSeconds(StartTimestamp);

		[JsonIgnore]
		public DateTimeOffset? EndTime => EndTimestamp != null ? DateTimeOffset.FromUnixTimeSeconds(EndTimestamp.Value) : null;

		[JsonIgnore]
		public TimeSpan Elapsed => (EndTime ?? DateTimeOffset.UtcNow) - StartTime;

		[JsonIgnore]
		public int TotalMonths => (int)Math.Ceiling(Elapsed.TotalDays / 30.4167);

		[JsonIgnore]
		public int Months => TotalMonths % 12;

		[JsonIgnore]
		public int Years => TotalMonths / 12;

		[JsonIgnore]
		public string Tagline
		{
			get
			{
				var sb = new StringBuilder();

				string startTime = ToStringUtility.DateToString(StartTime, DisplayMonth);
				string endTime = ToStringUtility.DateToString(EndTime, DisplayMonth);

				sb.Append(startTime);

				if (startTime != endTime)
				{
					sb.Append(" – ");
					sb.Append(endTime);
				}

				if (DisplayMonth)
				{
					sb.Append(" · ");

					if (Years == 1)
					{
						sb.Append("1 yr ");
					}
					else if (Years != 0)
					{
						sb.Append($"{Years} yrs ");
					}

					if (Months == 1)
					{
						sb.Append("1 mo");
					}
					else if (Months != 0)
					{
						sb.Append($"{Months} mos");
					}
				}

				return sb.ToString();
			}
		}

		public int CompareTo(PlacementModel other)
		{
			return StartTime.ToUnixTimeSeconds().CompareTo(other?.StartTime.ToUnixTimeSeconds() ?? 0);
		}
	}
}
