using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Instance.Models
{
	public class ContactSubmitRequestModel
	{
		public string RequestId { get; set; }

		[DisplayName("Email")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "An 'Email' is required.")]
		[EmailAddress(ErrorMessage = "The 'Email' must be a valid email address.")]
		[DataType(DataType.EmailAddress, ErrorMessage = "The 'Email' must be a valid email address.")]
		public string UserEmail { get; set; }

		[DisplayName("Subject")]
		[Required(ErrorMessage = "A 'Subject' is required.")]
		[MinLength(10, ErrorMessage = "The 'Subject' is too short.")]
		[DataType(DataType.Text)]
		public string UserSubject { get; set; }

		[DisplayName("Body")]
		[Required(ErrorMessage = "A 'Body' is required.")]
		[MinLength(10, ErrorMessage = "The 'Body' is too short.")]
		[DataType(DataType.MultilineText)]
		public string UserBody { get; set; }
	}
}
