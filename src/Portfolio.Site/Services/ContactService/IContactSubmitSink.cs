using Portfolio.Site.ViewModels;
using System.Threading.Tasks;

namespace Portfolio.Site.Services.ContactService
{
	public interface IContactSubmitSink
	{
		public Task ProcessSubmitAsync(ContactSubmitModel contactSubmit);
	}
}
