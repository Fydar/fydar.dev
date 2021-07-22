using Portfolio.Models;
using System.Collections.Generic;

namespace Portfolio.Site.ViewModels
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
