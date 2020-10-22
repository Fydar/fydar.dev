using System;

namespace Portfolio.Instance.Models
{
	public class ContactEmailViewModel
	{
		public string FormName { get; set; }
		public string UserEmail { get; set; }
		public string UserSubject { get; set; }
		public string UserBody { get; set; }
		public DateTimeOffset SubmitTime { get; set; }
	}
}
