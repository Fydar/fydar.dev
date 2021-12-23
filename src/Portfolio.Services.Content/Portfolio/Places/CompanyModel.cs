using RPGCore.Data;
using System.Text;
using System.Text.Json.Serialization;

namespace Portfolio.Models.Portfolio.Places
{
	[EditableType]
	public class CompanyModel : InstitutionModel
	{
		public bool IsProfessional { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public StudioType StudioType { get; set; }

		[JsonConverter(typeof(JsonStringEnumConverter))]
		public JobTime JobTime { get; set; }

		[JsonIgnore]
		public override string Tagline
		{
			get
			{
				var sb = new StringBuilder();

				switch (JobTime)
				{
					case JobTime.None:
						break;

					case JobTime.FullTime:
						sb.Append("Full-time · ");
						break;

					case JobTime.PartTime:
						sb.Append("Part-itme · ");
						break;

					case JobTime.Contract:
						sb.Append("Contract · ");
						break;

					case JobTime.Volunteer:
						sb.Append("Volunteer · ");
						break;
				}

				switch (StudioType)
				{
					case StudioType.None:
						break;

					case StudioType.Indie:
						sb.Append("Indie · ");
						break;

					case StudioType.AA:
						sb.Append("AA · ");
						break;

					case StudioType.AAA:
						sb.Append("AAA · ");
						break;

					case StudioType.SelfEmployed:
						sb.Append("Self-Employed · ");
						break;
				}

				if (IsProfessional)
				{
					if (Years == 1)
					{
						sb.Append("1 year");
					}
					else if (Years != 0)
					{
						sb.Append($"{Years} years");
					}

					if (Months != 0 && Years != 0)
					{
						sb.Append(", ");
					}

					if (Months == 1)
					{
						sb.Append("1 month");
					}
					else if (Months != 0)
					{
						sb.Append($"{Months} months");
					}
				}
				else
				{
					sb.Append(Name);
				}

				return sb.ToString();
			}
		}
	}
}
