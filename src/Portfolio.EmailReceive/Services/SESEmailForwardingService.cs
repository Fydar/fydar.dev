using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.EmailReceive.Services
{
	public class SESEmailForwardingService
	{
		private readonly IAmazonSimpleEmailService amazonSimpleEmailService;
		private readonly string destination;

		public SESEmailForwardingService(IAmazonSimpleEmailService amazonSimpleEmailService, string destination)
		{
			this.amazonSimpleEmailService = amazonSimpleEmailService;
			this.destination = destination;
		}

		public async Task<bool> ForwardEmailAsync(EmailModel email)
		{
			var forwardedMessage = new MimeMessage
			(
				from: new[] { new MailboxAddress("Anthony Marmont", "contact@anthonymarmont.com") },
				to: new[] { new MailboxAddress("Anthony Marmont", destination) },
				subject: email.Message.Subject,
				body: email.Message.Body
			);

			using var stream = new MemoryStream();
			await forwardedMessage.WriteToAsync(stream);

			var request = new SendRawEmailRequest()
			{
				Source = "contact@anthonymarmont.com",
				RawMessage = new RawMessage(stream),
				Destinations = new List<string>()
				{
					destination
				}
			};

			var response = await amazonSimpleEmailService.SendRawEmailAsync(request);

			return true;
		}
	}
}
