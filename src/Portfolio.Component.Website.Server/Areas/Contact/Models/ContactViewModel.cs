namespace Portfolio.Component.Website.Server.Areas.Contact.Models
{
	public class ContactViewModel : ContactSubmitRequestModel
	{
		public bool Sent { get; set; }
		public string SentToEmail { get; set; } = string.Empty;
	}
}
