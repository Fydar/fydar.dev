using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portfolio.Instance.Models;
using Portfolio.Instance.Services.ViewRenderer;
using Portfolio.Instance.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Instance.Controllers
{
	public class ContactController : Controller
	{
		private readonly ILogger<ContactController> logger;
		private readonly IAmazonSimpleEmailService simpleEmailService;
		private readonly IViewToStringRenderer razorViewToStringRenderer;

		public ContactController(
			ILogger<ContactController> logger,
			IAmazonSimpleEmailService simpleEmailService,
			IViewToStringRenderer razorViewToStringRenderer = null)
		{
			this.logger = logger;
			this.simpleEmailService = simpleEmailService;
			this.razorViewToStringRenderer = razorViewToStringRenderer;
		}

		[Route("[controller]")]
		public IActionResult Index()
		{
			return View("Index", new ContactViewModel());
		}

		[HttpPost("[controller]/[action]")]
		public async Task<IActionResult> Submit([FromForm] ContactSubmitRequestModel requestModel)
		{
			string htmlBody = await razorViewToStringRenderer.RenderViewToStringAsync("Email/ContactEmail", new ContactEmailViewModel()
			{
				FormName = "Contact",
				UserEmail = requestModel.UserEmail,
				UserBody = requestModel.UserBody,
				UserSubject = requestModel.UserSubject,
				SubmitTime = DateTimeOffset.Now
			});

			var request = new SendEmailRequest()
			{
				Source = "Anthony Marmont <contact@anthonymarmont.com>",
				Destination = new Destination()
				{
					ToAddresses = new List<string>()
					{
						"dev.anthonymarmont@gmail.com"
					}
				},
				Message = new Message()
				{
					Subject = new Content($"Contact: {requestModel.UserSubject}"),
					Body = new Body()
					{
						Html = new Content(htmlBody)
					}
				},				
			};

			await simpleEmailService.SendEmailAsync(request);

			return RedirectToAction(nameof(Index));
		}
	}
}
