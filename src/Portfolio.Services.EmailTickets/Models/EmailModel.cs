using MimeKit;

namespace Portfolio.Services.EmailTickets.Models
{
	public class EmailModel
	{
		public EmailHeaderModel Event { get; set; }
		public MimeMessage Message { get; set; }
	}
}
