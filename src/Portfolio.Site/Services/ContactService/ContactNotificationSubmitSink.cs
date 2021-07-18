using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Logging;
using Portfolio.Site.Areas.Contact.Models;
using Portfolio.Site.Services.ViewToStringRenderer;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Portfolio.Site.Services.ContactService
{
	public class ContactNotificationSubmitSink : IContactSubmitSink
	{
		private readonly ILogger<ContactNotificationSubmitSink> logger;
		private readonly IAmazonSimpleEmailService simpleEmailService;
		private readonly IViewToStringRenderer razorViewToStringRenderer;

		public ContactNotificationSubmitSink(
			ILogger<ContactNotificationSubmitSink> logger,
			IAmazonSimpleEmailService simpleEmailService,
			IViewToStringRenderer razorViewToStringRenderer)
		{
			this.logger = logger;
			this.simpleEmailService = simpleEmailService;
			this.razorViewToStringRenderer = razorViewToStringRenderer;
		}

		public async Task ProcessSubmitAsync(ContactSubmitModel contactSubmit)
		{
			string htmlBody = await razorViewToStringRenderer.RenderViewToStringAsync("Email/NewTicket", new ContactSubmitModel()
			{
				FormName = "Contact",
				TicketId = contactSubmit.TicketId,
				UserEmail = contactSubmit.UserEmail,
				UserBody = contactSubmit.UserBody,
				UserSubject = contactSubmit.UserSubject,
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
					Subject = new Content($"New message from '{contactSubmit.UserEmail}'."),
					Body = new Body()
					{
						Html = new Content(htmlBody)
					}
				},
			};
			await simpleEmailService.SendEmailAsync(request);
		}
	}
}
