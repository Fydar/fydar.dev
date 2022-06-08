using System;

namespace Portfolio.Component.Website.Server.Areas.Contact.Models;

public class ContactSubmitModel
{
	public string TicketId { get; set; } = string.Empty;
	public string FormName { get; set; } = string.Empty;
	public string UserEmail { get; set; } = string.Empty;
	public string UserSubject { get; set; } = string.Empty;
	public string UserBody { get; set; } = string.Empty;
	public DateTimeOffset SubmitTime { get; set; }
}
