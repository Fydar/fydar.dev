using Amazon.S3;
using Amazon.S3.Model;
using MimeKit;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Portfolio.Services.EmailTickets;

public class EmailReaderService
{
	private readonly IAmazonS3 amazonS3;
	private readonly EmailReaderServiceConfiguration configuration;

	public EmailReaderService(IAmazonS3 amazonS3, EmailReaderServiceConfiguration configuration)
	{
		this.amazonS3 = amazonS3;
		this.configuration = configuration;
	}

	public async Task<MimeMessage> ReadEmailAsync(string ticketId, CancellationToken cancellationToken = default)
	{
		var request = new GetObjectRequest()
		{
			BucketName = configuration.Bucket,
			Key = ticketId
		};
		var response = await amazonS3.GetObjectAsync(request, cancellationToken);

		MimeMessage message;
		using (var reader = new StreamReader(response.ResponseStream))
		{
			message = await MimeMessage.LoadAsync(response.ResponseStream, cancellationToken);
		}

		return message;
	}
}
