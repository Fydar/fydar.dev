using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Site.Areas.Contact.Models;
using Portfolio.Site.Services.ContactService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Site.Areas.Contact.Controllers
{
	[ApiController]
	[Area("Contact")]
	[Route("/contact")]
	[ApiExplorerSettings(GroupName = "Contact")]
	public class ContactSubmitController : Controller
	{
		private readonly ILogger<ContactSubmitController> logger;
		private readonly IContactSubmitSink[] contactSubmitSinks;

		public ContactSubmitController(
			ILogger<ContactSubmitController> logger,
			IEnumerable<IContactSubmitSink> contactSubmitSinks)
		{
			this.logger = logger;
			this.contactSubmitSinks = contactSubmitSinks.ToArray();
		}

		/// <summary>
		/// Submits a contact form.
		/// </summary>
		/// <param name="requestModel">A request model representing the contact form.</param>
		/// <returns>An updated page.</returns>
		/// <response code="200">An updated representation of the page.</response>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<IActionResult> Index([FromForm] ContactSubmitRequestModel requestModel)
		{
			if (ModelState.IsValid)
			{
				var contactSubmitModel = new ContactSubmitModel()
				{
					TicketId = requestModel.RequestId,
					FormName = "Contact",
					UserEmail = requestModel.UserEmail,
					UserBody = requestModel.UserBody,
					UserSubject = requestModel.UserSubject,
					SubmitTime = DateTimeOffset.Now
				};

				foreach (var contactSubmitSink in contactSubmitSinks)
				{
					await contactSubmitSink.ProcessSubmitAsync(contactSubmitModel);
				}

				return View("Index", new ContactViewModel()
				{
					UserEmail = "",
					UserSubject = "",
					UserBody = "",
					Sent = true,
					SentToEmail = requestModel.UserEmail
				});
			}

			return View("Index", new ContactViewModel()
			{
				UserEmail = requestModel.UserEmail,
				UserSubject = requestModel.UserSubject,
				UserBody = requestModel.UserBody,
				Sent = false,
			});
		}
	}
}
