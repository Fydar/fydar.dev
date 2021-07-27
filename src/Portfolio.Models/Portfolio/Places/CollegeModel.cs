using RPGCore.Data;
using System.Text.Json.Serialization;
using Portfolio.Models;
using Portfolio.Models.Portfolio.Places;

namespace Portfolio.Models.Portfolio.Places
{
	[EditableType]
	public class CollegeModel : InstitutionModel
	{
		[JsonIgnore]
		public override string Tagline => "";
	}
}
