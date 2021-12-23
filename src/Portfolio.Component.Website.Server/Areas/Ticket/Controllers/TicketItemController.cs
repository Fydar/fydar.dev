using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Component.Website.Server.Areas.Ticket.Models;
using Portfolio.Services.EmailTickets;
using System.Threading.Tasks;

namespace Portfolio.Component.Website.Server.Areas.Ticket.Controllers
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

		/// <summary>
		/// The website page for a ticket.
		/// </summary>
		/// <returns>A view representing the page.</returns>
		/// <response code="200">A ticket page.</response>
		/// <response code="404">When no ticket the <paramref name="ticketId"/> could be found.</response>
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
