using Amazon.Lambda.Core;
using Amazon.Lambda.SimpleEmailEvents;
using Amazon.Lambda.SimpleEmailEvents.Actions;
using Amazon.S3;
using Amazon.SimpleEmail;
using Portfolio.EmailReceive.Services;
using System;
using System.Threading.Tasks;

namespace Portfolio.EmailReceive
{
	public class Function
	{
		private readonly EmailReaderService emailReaderService;
		private readonly SESEmailForwardingService emailForwardingService;

		public Function()
		{
			string emailBuckt = Environment.GetEnvironmentVariable("CONFIG_EMAILBUCKET");
			string forwardTo = Environment.GetEnvironmentVariable("CONFIG_FORWARDTO");

			var amazonS3 = new AmazonS3Client();
			var amazonSimpleEmail = new AmazonSimpleEmailServiceClient();

			emailReaderService = new EmailReaderService(amazonS3, emailBuckt);
			emailForwardingService = new SESEmailForwardingService(amazonSimpleEmail, forwardTo);
		}

		/// <summary>
		/// A simple function that takes a string and does a ToUpper
		/// </summary>
		/// <param name="input"></param>
		/// <param name="context"></param>
		/// <returns></returns>
		public async Task<string> FunctionHandler(SimpleEmailEvent<LambdaReceiptAction> sesEvent, ILambdaContext context)
		{
			foreach (var record in sesEvent.Records)
			{
				var emailHeader = new EmailHeaderModel()
				{
					MessageId = record.Ses.Mail.MessageId,
					Timestamp = record.Ses.Mail.Timestamp,
					From = record.Ses.Mail.CommonHeaders.From,
					To = record.Ses.Mail.CommonHeaders.To,
					Subject = record.Ses.Mail.CommonHeaders.Subject
				};

				var email = await emailReaderService.ReadEmailAsync(emailHeader);

				await emailForwardingService.ForwardEmailAsync(email);
			}

			return "CONTINUE";
		}
	}
}
