using Portfolio.Component.Website.Server.Services.PageMetaProvider;

namespace Portfolio.Component.Website.Server.Areas.Contact.Models;

public class ContactViewModel : StaticPageViewModel
{
	public bool Sent { get; set; }
	public string SentToEmail { get; set; } = string.Empty;
	public ContactSubmitRequestModel ContactForm { get; set; }
}
