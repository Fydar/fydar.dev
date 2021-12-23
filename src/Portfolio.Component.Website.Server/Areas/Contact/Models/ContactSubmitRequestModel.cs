using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Portfolio.Component.Website.Server.Areas.Contact.Models
{
	public class ContactSubmitRequestModel
	{
		[Required]
		public string RequestId { get; set; } = string.Empty;

		[DisplayName("Email")]
		[Required(AllowEmptyStrings = false, ErrorMessage = "An email is required.")]
		[EmailAddress(ErrorMessage = "The email must be in a valid format.")]
		[DataType(DataType.EmailAddress, ErrorMessage = "The email must be in a valid format.")]
		public string UserEmail { get; set; } = string.Empty;

		[DisplayName("Subject")]
		[Required(ErrorMessage = "A subject is required.")]
		[MinLength(10, ErrorMessage = "The subject is too short.")]
		[DataType(DataType.Text)]
		public string UserSubject { get; set; } = string.Empty;

		[DisplayName("Body")]
		[Required(ErrorMessage = "A body is required.")]
		[MinLength(10, ErrorMessage = "The body is too short.")]
		[DataType(DataType.MultilineText)]
		public string UserBody { get; set; } = string.Empty;
	}
}
