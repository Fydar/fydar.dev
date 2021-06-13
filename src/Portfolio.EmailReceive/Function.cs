using Amazon.Lambda.Core;
using Amazon.Lambda.SimpleEmailEvents;
using Amazon.Lambda.SimpleEmailEvents.Actions;
using Amazon.S3;
using Amazon.SimpleEmail;
using Portfolio.EmailReceive.Services;
using Portfolio.Services.EmailTickets;
using Portfolio.Services.EmailTickets.Models;
using System;
using System.Threading.Tasks;

namespace Portfolio.EmailReceive
{
	public class Function
	{
		private readonly EmailReaderService emailReaderService;
		private readonly IEmailSinkService emailSinkService;

		public Function()
		{
			string emailBuckt = Environment.GetEnvironmentVariable("CONFIG_EMAILBUCKET");
			string forwardTo = Environment.GetEnvironmentVariable("CONFIG_FORWARDTO");

			var amazonS3 = new AmazonS3Client();
			var amazonSimpleEmail = new AmazonSimpleEmailServiceClient();

			emailReaderService = new EmailReaderService(amazonS3, new EmailReaderServiceConfiguration()
			{
				Bucket = emailBuckt
			});
			//emailSinkService = new SESEmailForwardingService(amazonSimpleEmail, forwardTo);
			emailSinkService = new SESNotifyingService(amazonSimpleEmail, forwardTo);
		}

		/// <summary>
		/// A simple function that takes a string and does a ToUpper
		/// </summary>
		/// <param name="sesEvent">The lambda event to process.</param>
		/// <param name="context">Context for the execution of this lambda function.</param>
		/// <returns></returns>
		public async Task<string> FunctionHandler(SimpleEmailEvent<LambdaReceiptAction> sesEvent, ILambdaContext context)
		{
			foreach (var record in sesEvent.Records)
			{
				foreach (string from in record.Ses.Mail.CommonHeaders.From)
				{
					if (from.EndsWith("amazonses.com", StringComparison.OrdinalIgnoreCase))
					{
						context.Logger.LogLine("Email was from amazonses.com, ignoring it.");
						continue;
					}
				}

				var emailHeader = new EmailHeaderModel()
				{
					MessageId = record.Ses.Mail.MessageId,
					Timestamp = record.Ses.Mail.Timestamp,
					From = record.Ses.Mail.CommonHeaders.From,
					To = record.Ses.Mail.CommonHeaders.To,
					Subject = record.Ses.Mail.CommonHeaders.Subject
				};

				var mimeMessage = await emailReaderService.ReadEmailAsync(emailHeader.MessageId);

				await emailSinkService.ForwardEmailAsync(new EmailModel()
				{
					Event = emailHeader,
					Message = mimeMessage
				});
			}

			return "CONTINUE";
		}
	}
}
