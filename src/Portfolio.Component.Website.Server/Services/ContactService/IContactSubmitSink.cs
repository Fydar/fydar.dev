using Portfolio.Site.Areas.Contact.Models;
using System.Threading.Tasks;

namespace Portfolio.Site.Services.ContactService
{
	public interface IContactSubmitSink
	{
		public Task ProcessSubmitAsync(ContactSubmitModel contactSubmit);
	}
}
