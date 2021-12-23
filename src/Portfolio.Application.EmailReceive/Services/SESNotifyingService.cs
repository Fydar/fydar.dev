using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using MimeKit;
using Portfolio.Services.EmailTickets.Models;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Portfolio.EmailReceive.Services
{
	public class SESNotifyingService : IEmailSinkService
	{
		private readonly IAmazonSimpleEmailService amazonSimpleEmailService;
		private readonly string destination;

		public SESNotifyingService(IAmazonSimpleEmailService amazonSimpleEmailService, string destination)
		{
			this.amazonSimpleEmailService = amazonSimpleEmailService;
			this.destination = destination;
		}

		public async Task<bool> ForwardEmailAsync(EmailModel email)
		{
			var body = new BodyBuilder
			{
				TextBody = $"You have new unread messages from a user using your contact email address.\n" +
					$"To view the message; use the administrator contact panel.\n" +
					$"Your new message can be found here. https://anthonymarmont.com/ticket/{email.Header.MessageId}"
			};

			var notificationMessage = new MimeMessage
			(
				from: new[] { new MailboxAddress("Anthony Marmont", "contact@anthonymarmont.com") },
				to: new[] { new MailboxAddress("Anthony Marmont", destination) },
				subject: "You have new unread messages",
				body: body.ToMessageBody()
			);

			using var stream = new MemoryStream();
			await notificationMessage.WriteToAsync(stream);

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
