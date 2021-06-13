using Portfolio.Instance.Models;
using System.Threading.Tasks;

namespace Portfolio.Instance.Services.ContactService
{
	public interface IContactSubmitSink
	{
		public Task ProcessSubmitAsync(ContactSubmitModel contactSubmit);
	}
}
