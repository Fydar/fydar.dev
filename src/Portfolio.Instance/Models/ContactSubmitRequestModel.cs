using System.ComponentModel.DataAnnotations;

namespace Portfolio.Instance.Models
{
	public class ContactSubmitRequestModel
	{
		[Required]
		[EmailAddress]
		[DataType(DataType.EmailAddress)]
		public string UserEmail { get; set; }

		[Required]
		[MinLength(10)]
		[DataType(DataType.Text)]
		public string UserSubject { get; set; }

		[Required]
		[MinLength(10)]
		[DataType(DataType.MultilineText)]
		public string UserBody { get; set; }
	}
}
