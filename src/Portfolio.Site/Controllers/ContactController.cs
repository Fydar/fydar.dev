using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Site.Models;
using Portfolio.Site.Services.ContactService;
using Portfolio.Site.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Instance.Controllers
{
	public class ContactController : Controller
	{
		private readonly ILogger<ContactController> logger;
		private readonly IContactSubmitSink[] contactSubmitSinks;

		public ContactController(
			ILogger<ContactController> logger,
			IEnumerable<IContactSubmitSink> contactSubmitSinks)
		{
			this.logger = logger;
			this.contactSubmitSinks = contactSubmitSinks.ToArray();
		}

		[HttpGet("contact")]
		public IActionResult Index()
		{
			return View("Index", new ContactViewModel());
		}

		[HttpPost("contact")]
		public async Task<IActionResult> Submit([FromForm] ContactSubmitRequestModel requestModel)
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
