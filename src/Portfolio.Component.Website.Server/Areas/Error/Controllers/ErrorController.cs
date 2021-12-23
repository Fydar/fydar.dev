using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Error.Models;

namespace Portfolio.Site.Areas.Error.Controllers
{
	[ApiController]
	[Area("Error")]
	[Route("error/{code:int}")]
	[ApiExplorerSettings(GroupName = "Error")]
	public class ErrorController : Controller
	{
		public ErrorController()
		{
		}

		/// <summary>
		/// The website page for arbitrary errors.
		/// </summary>
		/// <returns>A view representing the page.</returns>
		/// <response code="200">The arbitrary error page.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Index(int code)
		{
			return View("ServerError", new ErrorViewModel()
			{

			});
		}
	}
}
