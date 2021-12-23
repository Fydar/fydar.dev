using Portfolio.Component.Website.Server.Areas.Contact.Models;
using System.Threading.Tasks;

namespace Portfolio.Component.Website.Server.Services.ContactService
{
	public interface IContactSubmitSink
	{
		public Task ProcessSubmitAsync(ContactSubmitModel contactSubmit);
	}
}
