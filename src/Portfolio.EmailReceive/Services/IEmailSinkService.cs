using Portfolio.Services.EmailTickets.Models;
using System.Threading.Tasks;

namespace Portfolio.EmailReceive.Services
{
	public interface IEmailSinkService
	{
		Task<bool> ForwardEmailAsync(EmailModel email);
	}
}