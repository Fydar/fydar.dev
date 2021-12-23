using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.Site.Areas.Error.Models;

namespace Portfolio.Site.Areas.Error.Controllers
{
	[ApiController]
	[Area("Error")]
	[Route("error/404")]
	[ApiExplorerSettings(GroupName = "Error")]
	public class Error404Controller : Controller
	{
		public Error404Controller()
		{
		}

		/// <summary>
		/// The website page for 404 errors.
		/// </summary>
		/// <returns>A view representing the page.</returns>
		/// <response code="200">The 404 error page.</response>
		[HttpGet]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public IActionResult Index()
		{
			return View("Error404", new ErrorViewModel()
			{

			});
		}
	}
}
