using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Site.ViewModels;
using Portfolio.Services.EmailTickets;
using System.Threading.Tasks;

namespace Portfolio.Instance.Controllers
{
	public class TicketController : Controller
	{
		private readonly ILogger<TicketController> logger;
		private readonly EmailReaderService emailReader;

		public TicketController(
			ILogger<TicketController> logger,
			EmailReaderService emailReader)
		{
			this.logger = logger;
			this.emailReader = emailReader;
		}

		[HttpGet("ticket")]
		public IActionResult Index()
		{
			return View("Index", new TicketIndexViewModel());
		}

		[HttpGet("ticket/{ticketId}")]
		public async Task<IActionResult> Ticket(string ticketId)
		{
			var response = await emailReader.ReadEmailAsync(ticketId);

			return View("Ticket", new TicketTicketViewModel()
			{
				TicketId = ticketId,
				From = response.From.ToString(),
				To = response.To.ToString(),
				HtmlBody = response.HtmlBody?.ToString() ?? response.TextBody?.ToString() ?? "",
			});
		}
	}
}
