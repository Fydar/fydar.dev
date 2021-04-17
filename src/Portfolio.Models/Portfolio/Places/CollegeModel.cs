using RPGCore.DataEditor.CSharp;
using System.Text.Json.Serialization;

namespace Portfolio.Models
{
	[EditorType]
	public class CollegeModel : InstitutionModel
	{
		[JsonIgnore]
		public override string Tagline => "";
	}
}
