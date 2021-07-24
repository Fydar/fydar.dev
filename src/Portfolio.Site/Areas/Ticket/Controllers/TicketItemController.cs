using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Services.EmailTickets;
using Portfolio.Site.Areas.Ticket.Models;
using System.Threading.Tasks;

namespace Portfolio.Site.Areas.Ticket.Controllers
{
	[ApiController]
	[Area("Ticket")]
	[Route("/ticket/{ticketId}")]
	[ApiExplorerSettings(GroupName = "Ticket")]
	public class TicketItemController : Controller
	{
		private readonly ILogger<TicketItemController> logger;
		private readonly EmailReaderService emailReader;

		public TicketItemController(
			ILogger<TicketItemController> logger,
			EmailReaderService emailReader)
		{
			this.logger = logger;
			this.emailReader = emailReader;
		}

		[HttpGet]
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
