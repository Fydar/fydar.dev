using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.Logging;
using MimeKit;
using Portfolio.Site.ViewModels;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Portfolio.Site.Services.ContactService
{
	public class SaveTicketSubmitSink : IContactSubmitSink
	{
		private readonly ILogger<SaveTicketSubmitSink> logger;
		private readonly IAmazonS3 amazonS3;

		public SaveTicketSubmitSink(
			ILogger<SaveTicketSubmitSink> logger,
			IAmazonS3 amazonS3)
		{
			this.logger = logger;
			this.amazonS3 = amazonS3;
		}

		public async Task ProcessSubmitAsync(ContactSubmitModel contactSubmit)
		{
			var message = new MailMessage(new MailAddress(contactSubmit.UserEmail), new MailAddress($"form-{contactSubmit.FormName}@anthonymarmont.com"))
			{
				Body = contactSubmit.UserBody
			};

			var mimeMessage = MimeMessage.CreateFromMailMessage(message);

			string emailString;
			using (var ms = new MemoryStream())
			{
				await mimeMessage.WriteToAsync(ms);
				ms.Seek(0, SeekOrigin.Begin);

				using var streamReader = new StreamReader(ms);
				emailString = streamReader.ReadToEnd();
			}

			var request = new PutObjectRequest()
			{
				BucketName = "anthonymarmont.com-inbound-email",
				Key = contactSubmit.TicketId,
				ContentBody = emailString
			};
			var response = await amazonS3.PutObjectAsync(request);
		}
	}
}
