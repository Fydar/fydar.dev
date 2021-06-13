using System;

namespace Portfolio.Instance.Models
{
	public class ContactSubmitModel
	{
		public string TicketId { get; set; }
		public string FormName { get; set; }
		public string UserEmail { get; set; }
		public string UserSubject { get; set; }
		public string UserBody { get; set; }
		public DateTimeOffset SubmitTime { get; set; }
	}
}
