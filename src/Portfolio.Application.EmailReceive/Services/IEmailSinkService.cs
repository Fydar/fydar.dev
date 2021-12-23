using Portfolio.Services.EmailTickets.Models;
using System.Threading.Tasks;

namespace Portfolio.Application.EmailReceive.Services
{
	public interface IEmailSinkService
	{
		Task<bool> ForwardEmailAsync(EmailModel email);
	}
}
