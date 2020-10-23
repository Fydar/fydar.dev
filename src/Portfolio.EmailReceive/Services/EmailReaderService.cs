using Amazon.S3;
using Amazon.S3.Model;
using MimeKit;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.EmailReceive.Services
{
	public class EmailReaderService
	{
		private readonly IAmazonS3 amazonS3;
		private readonly string bucket;

		public EmailReaderService(IAmazonS3 amazonS3, string bucket)
		{
			this.amazonS3 = amazonS3;
			this.bucket = bucket;
		}

		public async Task<EmailModel> ReadEmailAsync(EmailHeaderModel header, CancellationToken cancellationToken = default)
		{
			var request = new GetObjectRequest()
			{
				BucketName = bucket,
				Key = header.MessageId
			};
			var response = await amazonS3.GetObjectAsync(request, cancellationToken);

			using var reader = new StreamReader(response.ResponseStream);

			return new EmailModel()
			{
				Event = header,
				Message = await MimeMessage.LoadAsync(response.ResponseStream, cancellationToken)
			};
		}
	}
}
