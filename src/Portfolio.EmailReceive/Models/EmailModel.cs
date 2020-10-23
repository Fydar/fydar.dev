using MimeKit;

namespace Portfolio.EmailReceive.Services
{
	public class EmailModel
	{
		public EmailHeaderModel Event { get; set; }
		public MimeMessage Message { get; set; }
	}
}
