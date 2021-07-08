using Portfolio.Site.Models;

namespace Portfolio.Site.ViewModels
{
	public class ContactViewModel : ContactSubmitRequestModel
	{
		public bool Sent { get; set; }
		public string SentToEmail { get; set; } = string.Empty;
	}
}
