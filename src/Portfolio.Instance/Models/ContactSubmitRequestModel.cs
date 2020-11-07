using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Instance.Models
{
	public class ContactSubmitRequestModel
	{
		public string RequestId { get; set; }

		[DisplayName("Email")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "An email is required.")]
		[EmailAddress(ErrorMessage = "The email must be in a valid format.")]
		[DataType(DataType.EmailAddress, ErrorMessage = "The email must be in a valid format.")]
		public string UserEmail { get; set; }

		[DisplayName("Subject")]
		[Required(ErrorMessage = "A subject is required.")]
		[MinLength(10, ErrorMessage = "The subject is too short.")]
		[DataType(DataType.Text)]
		public string UserSubject { get; set; }

		[DisplayName("Body")]
		[Required(ErrorMessage = "A body is required.")]
		[MinLength(10, ErrorMessage = "The body is too short.")]
		[DataType(DataType.MultilineText)]
		public string UserBody { get; set; }
	}
}
