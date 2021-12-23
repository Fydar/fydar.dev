﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Component.Website.Server.Areas.Ticket.Models;
using Portfolio.Services.EmailTickets;

namespace Portfolio.Component.Website.Server.Areas.Ticket.Controllers
{
	[ApiController]
	[Area("Ticket")]
	[Route("/ticket")]
	[ApiExplorerSettings(GroupName = "Ticket")]
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

		/// <summary>
		/// The website page for tickets.
		/// </summary>
		/// <returns>A view representing the page.</returns>
		/// <response code="200">The tickets page.</response>
		[HttpGet]
		public IActionResult Index()
		{
			return View("Index", new TicketIndexViewModel());
		}
	}
}