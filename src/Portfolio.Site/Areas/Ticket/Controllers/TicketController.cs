using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Services.EmailTickets;
using Portfolio.Site.Areas.Ticket.Models;

namespace Portfolio.Site.Areas.Ticket.Controllers
{
	[ApiController]
	[Area("Ticket")]
	[Route("/ticket")]
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

		[HttpGet]
		public IActionResult Index()
		{
			return View("Index", new TicketIndexViewModel());
		}
	}
}
