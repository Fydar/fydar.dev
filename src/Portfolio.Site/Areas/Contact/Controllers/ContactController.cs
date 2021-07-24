using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Contact.Models;

namespace Portfolio.Site.Areas.Contact.Controllers
{
	[ApiController]
	[Area("Contact")]
	[Route("/contact")]
	[ApiExplorerSettings(GroupName = "Contact")]
	public class ContactController : Controller
	{
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Index()
		{
			return View("Index", new ContactViewModel());
		}
	}
}
