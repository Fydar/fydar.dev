using Portfolio.Instance.Models;

namespace Portfolio.Instance.ViewModels
{
	public class ContactViewModel : ContactSubmitRequestModel
	{
		public bool Sent { get; set; }
		public string SentToEmail { get; set; } = string.Empty;
	}
}
