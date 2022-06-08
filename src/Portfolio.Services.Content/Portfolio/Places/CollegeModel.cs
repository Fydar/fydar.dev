using RPGCore.Data;
using System.Text.Json.Serialization;

namespace Portfolio.Services.Content.Portfolio.Places;

[EditableType]
public class CollegeModel : InstitutionModel
{
	[JsonIgnore]
	public override string Tagline => "";
}
