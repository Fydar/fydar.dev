using Portfolio.Models;
using System.Collections.Generic;
using Portfolio.Models.Portfolio.Places;
using Portfolio.Site.ViewModels;

namespace Portfolio.Site.Areas.Resume.Models
{
	public class TimelineViewModel
	{
		public IEnumerable<InstitutionModel> Institutions { get; set; }
		public bool FocusPosition { get; set; }

		public TimelineViewModel(
			IEnumerable<InstitutionModel> institutions,
			bool focusPosition = false)
		{
			Institutions = institutions;
			FocusPosition = focusPosition;
		}
	}
}
